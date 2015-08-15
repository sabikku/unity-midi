using System;
using System.IO;

namespace MidiLoader.Events.ChannelVoice {
	
	public class ChannelPressureMidiEvent : ChannelVoiceMidiEvent {
		
		public ushort channel = 0;
		public ushort pressureValue = 0;
		
		protected override byte firstFourBitsMask {
			get {
				return 0xd0;
			}
		}
		
		protected override ushort dataBytesCount {
			get {
				return 1;
			}
		}

		protected override ChannelVoiceMidiEvent Construct( byte statusByte, byte[] dataBytes ) {
			ChannelPressureMidiEvent result = new ChannelPressureMidiEvent();

			result.channel   = (ushort)( statusByte & 0x0f );
			result.pressureValue = (ushort)( dataBytes[0] & 0x7f );

			return result;
		}
		
	}
	
}