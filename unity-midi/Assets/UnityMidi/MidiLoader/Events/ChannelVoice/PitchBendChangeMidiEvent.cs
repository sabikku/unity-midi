using System;
using System.IO;

namespace MidiLoader.Events.ChannelVoice {
	
	public class PitchBendChangeMidiEvent : ChannelVoiceMidiEvent {
		
		public ushort channel = 0;
		public uint pitchChange = 0;
		
		protected override byte firstFourBitsMask {
			get {
				return 0xe0;
			}
		}
		
		protected override ushort dataBytesCount {
			get {
				return 2;
			}
		}

		protected override ChannelVoiceMidiEvent Construct( byte statusByte, byte[] dataBytes ) {
			PitchBendChangeMidiEvent result = new PitchBendChangeMidiEvent();

			result.channel   = (ushort)( statusByte & 0x0f );
			
			byte firstSevenBits = (byte)( dataBytes[0] & 0x7f );
			byte secondSevenBits = (byte)( dataBytes[1] & 0x7f );

			result.pitchChange = (uint)( firstSevenBits << 7 );
			result.pitchChange += (uint)( secondSevenBits ); 

			return result;
		}

		public override string ToString() {
			return base.ToString() + ", channel: " + channel + ", pitchChange: " + pitchChange;
		}
		
	}
	
}