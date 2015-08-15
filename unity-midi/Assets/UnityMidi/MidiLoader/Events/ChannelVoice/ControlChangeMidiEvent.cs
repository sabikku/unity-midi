using System;
using System.IO;

namespace MidiLoader.Events.ChannelVoice {
	
	public class ControlChangeMidiEvent : ChannelVoiceMidiEvent {
		
		public ushort channel = 0;
		public ushort controllerNo = 0;
		public ushort value = 0;
		
		protected override byte firstFourBitsMask {
			get {
				return 0xb0;
			}
		}
		
		protected override ushort dataBytesCount {
			get {
				return 2;
			}
		}

		protected override ChannelVoiceMidiEvent Construct( byte statusByte, byte[] dataBytes ) {
			ControlChangeMidiEvent result = new ControlChangeMidiEvent();

			result.channel      = (ushort)( statusByte & 0x0f );
			result.controllerNo = (ushort)( dataBytes[0] & 0x7f );
			result.value        = (ushort)( dataBytes[1] & 0x7f );

			return result;
		}

		public override string ToString() {
			return base.ToString() + ", channel: " + channel + " controllerNo: " + controllerNo + " value: " + value;
		}
		
	}
	
}