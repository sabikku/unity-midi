using System;
using System.IO;

namespace MidiLoader.Events.ChannelVoice {
	
	public class ProgramChangeMidiEvent : ChannelVoiceMidiEvent {
		
		public ushort channel = 0;
		public ushort programNo = 0;
		
		protected override byte firstFourBitsMask {
			get {
				return 0xc0;
			}
		}
		
		protected override ushort dataBytesCount {
			get {
				return 1;
			}
		}

		protected override ChannelVoiceMidiEvent Construct( byte statusByte, byte[] dataBytes ) {
			ProgramChangeMidiEvent result = new ProgramChangeMidiEvent();

			result.channel   = (ushort)( statusByte & 0x0f );
			result.programNo = (ushort)( dataBytes[0] & 0x7f );

			return result;
		}

		public override string ToString() {
			return base.ToString() + ", channel: " + channel + ", programNo: " + programNo;
		}
		
	}
	
}