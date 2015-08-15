using System;
using System.IO;

namespace MidiLoader.Events.ChannelVoice {
	
	public class PolyphonicPressureMidiEvent : ChannelVoiceMidiEvent {
		
		public ushort channel = 0;
		public ushort note = 0;
		public ushort pressureValue = 0;
		
		protected override byte firstFourBitsMask {
			get {
				return 0xa0;
			}
		}
		
		protected override ushort dataBytesCount {
			get {
				return 2;
			}
		}

		protected override ChannelVoiceMidiEvent Construct( byte statusByte, byte[] dataBytes ) {
			PolyphonicPressureMidiEvent result = new PolyphonicPressureMidiEvent();

			result.channel  = (ushort)( statusByte & 0x0f );
			result.note     = (ushort)( dataBytes[0] & 0x7f );
			result.pressureValue    = (ushort)( dataBytes[1] & 0x7f );

			return result;
		}
		
	}
	
}