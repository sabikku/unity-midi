using System.Collections.Generic;
using MidiLoader.Parser;
using MidiLoader.Events;
using MidiLoader.Events.ChannelVoice;
using MidiLoader.Events.Meta;

namespace MidiFakeSequencer {

	internal class TrackSequence {

		Sequencer sequencer;
		TrackInfo info;
		
		string name;

		uint nextEventIndex;
		ulong remainingDeltas;
		ulong sumsOfDeltas;
		bool trackEnded;
		
		List<Sequencer.NoteOn> noteOns;

		public TrackSequence( Sequencer fakeSequencer, TrackInfo trackInfo ) {
			sequencer = fakeSequencer;
			info = trackInfo;

			nextEventIndex = 0;
			trackEnded = false;
			remainingDeltas = info.events[ 0 ].deltas;
			sumsOfDeltas = remainingDeltas;
			noteOns = new List<Sequencer.NoteOn>();
		}

		public void Process() {
			if ( !HasEnded() ) {
				while ( !HasEnded() && remainingDeltas == 0 ) {
					// simultanously processed events with zero deltas
					ProcessCurrentEvent();
				}
				
				remainingDeltas--;
			}
		}

		void ProcessCurrentEvent() {
			uint currentEventIndex = nextEventIndex;
			float currentRealtime = CalculateRealtimeForEvent( currentEventIndex );
			
			MidiEvent midiEvent = info.events[ currentEventIndex ];
			ProcessEventEffect( midiEvent, currentRealtime );
			
			PrepareForNextEvent( currentEventIndex );
		}
		
		float CalculateRealtimeForEvent( uint currentEventIndex ) {
			ulong deltasSpan = ( sumsOfDeltas - sequencer.lastTempoChangeSumOfDeltas );
			float realtimeInMicroseconds = ( deltasSpan / (float)sequencer.deltasPerQuaterNote ) * sequencer.microsecondsPerQuaterNote;
			
			return sequencer.lastTempoChangeRealtime + ( realtimeInMicroseconds / 1000000f );
		}

		float lastNoteOnRealtime = -1;
		void ProcessEventEffect( MidiEvent midiEvent, float currentRealtime ) {
			if ( midiEvent is NoteOnMidiEvent ) {
				if ( ( midiEvent as NoteOnMidiEvent ).velocity > 0 ) {
					if ( currentRealtime > lastNoteOnRealtime ) { // we don't want to sequence two sounds simultanously
						lastNoteOnRealtime = currentRealtime;
						noteOns.Add( new Sequencer.NoteOn() {
							realtime = currentRealtime,
							#if SEQUENCE_NOTES
							note = ( midiEvent as NoteOnMidiEvent ).note,
							#endif
						} );
					}
				}
				
			} else if ( midiEvent is TrackOrSequenceNameMetaEvent ) {
				name = ( midiEvent as TrackOrSequenceNameMetaEvent ).trackOrSequenceName;
				
			} else if ( midiEvent is SetTempoMetaEvent ) {
				sequencer.microsecondsPerQuaterNote = ( midiEvent as SetTempoMetaEvent ).microsecondsPerQuaterNote;
				sequencer.lastTempoChangeSumOfDeltas = sumsOfDeltas;
				sequencer.lastTempoChangeRealtime = currentRealtime;
			}
		}
		
		void PrepareForNextEvent( uint currentEventIndex ) {
			nextEventIndex++;
			if ( nextEventIndex >= info.events.Length ) {
				trackEnded = true;
			} else {
				remainingDeltas = info.events[ nextEventIndex ].deltas;
				sumsOfDeltas += remainingDeltas;
			}
		}

		public Sequencer.FakeTrack ToFakeTrack() {
			Sequencer.FakeTrack result = new Sequencer.FakeTrack();
			result.name = name;
			result.noteOns = noteOns.ToArray();
			return result;
		}

		public bool HasEnded() {
			return trackEnded;
		}

	}

}
