
//#define SEQUENCE_NOTES

using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using MidiLoader;
using MidiLoader.Parser;
using MidiLoader.Events;
using MidiLoader.Events.ChannelVoice;
using MidiLoader.Events.Meta;
using MidiFakeSequencer.Exceptions;
using MidiUtils;

namespace MidiFakeSequencer {

	public class Sequencer {
		
		[System.Serializable]
		public struct NoteOn {
			public float realtime;
			#if SEQUENCE_NOTES
			public ushort note;
			#endif
		}
		
		[System.Serializable]
		public class FakeTrack {
			public string name;
			public NoteOn[] noteOns;
		}

		public FakeTrack[] fakeTracks {
			get;
			private set;
		}

		internal ushort deltasPerQuaterNote = 480;
		internal uint microsecondsPerQuaterNote = 500000;
		internal ulong lastTempoChangeSumOfDeltas = 0;
		internal float lastTempoChangeRealtime = 0;

		TrackSequence[] trackSequences;

		public Sequencer( Loader midiLoader ) {
			TimeSpanLogger.Begin();

			PrepareTicksPerQuaterNote( midiLoader.headerInfo );
			InitializeTrackSequences( midiLoader.trackInfos );

			while ( !AllTracksHasEnded() ) {
				for ( int i = 0; i < trackSequences.Length; i++ ) {
					trackSequences[ i ].Process();
				}
			}

			ConstructFakeTracks();
			
			TimeSpanLogger.EndAndLog( "Sequencer done" );
		}

		void PrepareTicksPerQuaterNote( HeaderInfo header ) {
			DeltasDivision deltas = header.deltasDivision;
			if ( deltas is TicksPerQuaterNoteDeltasDivision ) {
				deltasPerQuaterNote = ( deltas as TicksPerQuaterNoteDeltasDivision ).ticksPerQuaterNote;
			} else {
				throw new FakeSequencerException( "Unsupported deltasDivision format: " + deltas.GetType() );
			}
		}

		void InitializeTrackSequences( TrackInfo[] tracks ) {
			trackSequences = new TrackSequence[ tracks.Length ];
			for ( int i = 0; i < trackSequences.Length; i++ ) {
				trackSequences[ i ] = new TrackSequence( this, tracks[ i ] );
			}
		}

		void ConstructFakeTracks() {
			List<FakeTrack> fakeTracksList = new List<FakeTrack>();
			for ( int i = 0; i < trackSequences.Length; i++ ) {
				fakeTracksList.Add( trackSequences[ i ].ToFakeTrack() );
			}

			fakeTracksList.RemoveAll( fakeTrack => ( fakeTrack.noteOns.Length == 0 ) );

			fakeTracks = fakeTracksList.ToArray();
		}
		
		bool AllTracksHasEnded() {
			for ( int i = 0; i < trackSequences.Length; i++ ) {
				if ( !trackSequences[ i ].HasEnded() ) {
					return false;
				}
			}
			return true;
		}

	}

}