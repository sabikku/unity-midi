using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.SystemCommon {
	
	public class SongSelectMidiEvent : SystemCommonMidiEvent {
		
		public ushort songNo;

		protected override byte statusByteMask {
			get {
				return 0xf3;
			}
		}
		
		protected override SystemCommonMidiEvent Construct( Stream stream ) {
			SongSelectMidiEvent result = new SongSelectMidiEvent();
			
			byte dataByte = ParseUtils.SafelyReadByte( stream ); 
			result.songNo = (ushort)( dataByte & 0x7f );
			
			return result;
		}
		
	}
	
}