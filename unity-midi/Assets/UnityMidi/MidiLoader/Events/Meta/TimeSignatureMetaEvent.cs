using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public class TimeSignatureMetaEvent : MetaEvent {
		
		public ushort nominator;
		public ushort denominator;
		public ushort midiClocksPerDottedQuarter;
		public ushort thirtySecondNotesInQuaterNote;

		static byte[] identifyingMask = new byte[] {
			0x58, 0x04
		};

		protected override byte[] identifyingBytesMask {
			get {
				return identifyingMask;
			}
		}

		protected override MetaEvent Construct( Stream stream ) {
			TimeSignatureMetaEvent result = new TimeSignatureMetaEvent();
			
			byte[] dataBytes = ParseUtils.SafelyReadBytes( stream, 4 );
			
			result.nominator = (ushort)( dataBytes[ 0 ] );
			result.denominator = (ushort)( dataBytes[ 1 ] );
			result.midiClocksPerDottedQuarter = (ushort)( dataBytes[ 2 ] );
			result.thirtySecondNotesInQuaterNote = (ushort)( dataBytes[ 3 ] );

			return result;
		}
		
		public override string ToString() {
			return base.ToString() + string.Format( ", n/d: {0}/{1} clocksPerQuater: {2} thirtySecondInQuater: {3}",
			                                       nominator, denominator,
			                                       midiClocksPerDottedQuarter, thirtySecondNotesInQuaterNote);
		}
		
	}
	
}