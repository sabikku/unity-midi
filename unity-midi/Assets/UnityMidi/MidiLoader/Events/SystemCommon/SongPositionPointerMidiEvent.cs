using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.SystemCommon {
	
	public class SongPositionPointerMidiEvent : SystemCommonMidiEvent {

		public uint beatsFromStart;

		protected override byte statusByteMask {
			get {
				return 0xf2;
			}
		}
		
		protected override SystemCommonMidiEvent Construct( Stream stream ) {
			SongPositionPointerMidiEvent result = new SongPositionPointerMidiEvent();
			
			byte[] dataBytes = ParseUtils.SafelyReadBytes( stream, 2 );
			
			byte firstSevenBits = (byte)( dataBytes[0] & 0x7f );
			byte secondSevenBits = (byte)( dataBytes[1] & 0x7f );
			
			result.beatsFromStart = (uint)( firstSevenBits << 7 );
			result.beatsFromStart += (uint)( secondSevenBits ); 
			
			return result;
		}
		
	}
	
}