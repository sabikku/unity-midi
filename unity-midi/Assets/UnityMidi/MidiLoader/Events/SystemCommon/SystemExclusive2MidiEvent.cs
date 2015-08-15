using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.SystemCommon {
	
	public class SystemExclusive2MidiEvent : SystemCommonMidiEvent {

		public byte[] data;
		
		protected override byte statusByteMask {
			get {
				return 0xf7;
			}
		}
		
		protected override SystemCommonMidiEvent Construct( Stream stream ) {
			SystemExclusive2MidiEvent result = new SystemExclusive2MidiEvent();

			ulong exclusiveBytesCount = VariableLengthQuantity.FromStream( stream );
			result.data = ParseUtils.SafelyReadBytes( stream, (int)exclusiveBytesCount );
			
			return result;
		}
		
	}
	
}