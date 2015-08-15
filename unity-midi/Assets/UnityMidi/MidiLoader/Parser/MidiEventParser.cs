using System.IO;
using MidiLoader.Exceptions;
using MidiLoader.Events;
using MidiLoader.Events.ChannelVoice;
using MidiLoader.Events.SystemCommon;
using MidiLoader.Events.SystemRealTime;
using MidiLoader.Events.Meta;

namespace MidiLoader.Parser {

	public class MidiEventParser {

		static MidiEvent[] eventPrototypes = new MidiEvent[] {
			new NoteOffMidiEvent(),
			new NoteOnMidiEvent(),
			new PitchBendChangeMidiEvent(),
			new PolyphonicPressureMidiEvent(),
			new ControlChangeMidiEvent(),
			new ProgramChangeMidiEvent(),
			new ChannelPressureMidiEvent(),
			
			new SystemExclusiveMidiEvent(),
			new SystemExclusive2MidiEvent(),
			new SongPositionPointerMidiEvent(),
			new SongSelectMidiEvent(),
			new TuneRequestMidiEvent(),
			
			new TimingClockMidiEvent(),
			new StartMidiEvent(),
			new ContinueMidiEvent(),
			new StopMidiEvent(),
			new ActiveSensingMidiEvent(),

			new UndefinedMidiEvent()
		};

		static MetaEvent[] metaEventPrototypes = new MetaEvent[] {
			new ChannelPrefixMetaEvent(),
			new CopyrightNoticeMetaEvent(),
			new CuePointMetaEvent(),
			new DeviceNameMetaEvent(),
			new EndOfTrackMetaEvent(),
			new InstrumentNameMetaEvent(),
			new KeySignatureMetaEvent(),
			new LyricMetaEvent(),
			new MarkerMetaEvent(),
			new MidiPortMetaEvent(),
			new ProgramNameMetaEvent(),
			new SequenceNumberMetaMidiEvent(),
			new SequencerSpecificMetaEvent(),
			new SetTempoMetaEvent(),
			new SMPTEOffsetMetaEvent(),
			new TextMetaEvent(),
			new TimeSignatureMetaEvent(),
			new TrackOrSequenceNameMetaEvent()
		};

		byte? lastStatusByte;

		public MidiEvent Parse( Stream stream ) {

			ulong delay = VariableLengthQuantity.FromStream( stream );

			byte possibleStatusByte = ParseUtils.SafelyReadByte( stream );
			byte statusByte = ProcessRunningStatusByte( stream, possibleStatusByte, lastStatusByte );

			MidiEvent midiEvent = ParseEvent( stream, statusByte );

			this.lastStatusByte = statusByte;
			midiEvent.deltas = delay;
			return midiEvent;

		}

		MidiEvent ParseEvent( Stream stream, byte statusByte ) {
			MidiEvent prototype = FindPrototypeBasedOnStatusByte( statusByte );
			if ( prototype == null ) {
				if ( CanBeMetaEvent( statusByte ) ) {
					prototype = FindMetaPrototypeBasedOnStream( stream );
				} else {
					throw new UnrecognizedMidiEventException( statusByte );
				}
			} 

			return prototype.Construct( stream, statusByte );
		}

		byte ProcessRunningStatusByte( Stream stream, byte possibleStatusByte, byte? lastStatusByte ) {
			if ( possibleStatusByte >= 0x80 || !lastStatusByte.HasValue ) {
				return possibleStatusByte;
			} else {
				// "Running status" idea - if no status is delivered, we remember last status byte and deal with current byte as a data byte
				stream.Position--;
				return lastStatusByte.Value;
			}
		}
		
		MidiEvent FindPrototypeBasedOnStatusByte( byte statusByte ) {
			foreach ( MidiEvent prototype in eventPrototypes ) {
				if ( prototype.MatchesStatusByte( statusByte ) ) {
					return prototype;
				}
			}
			return null;
		}

		// almost-hack: we decide based on any meta-event status byte match
		bool CanBeMetaEvent( byte statusByte ) {
			MetaEvent anyMetaEvent = metaEventPrototypes[0];
			return anyMetaEvent.MatchesStatusByte( statusByte );
		}
		
		MetaEvent FindMetaPrototypeBasedOnStream( Stream stream ) {
			byte[] identifyingBytes = ParseUtils.SafelyReadBytes( stream, 2 );
			stream.Position -= 2;
			foreach ( MetaEvent metaPrototype in metaEventPrototypes ) {
				if ( metaPrototype.MatchesIdentifyingBytes( identifyingBytes ) ) {
					return metaPrototype;
				}
			}

			//TODO make it clean
			return new BlankMetaEvent();//throw new UnrecognizedMetaEventException( identifyingBytes );
		}

	}

}