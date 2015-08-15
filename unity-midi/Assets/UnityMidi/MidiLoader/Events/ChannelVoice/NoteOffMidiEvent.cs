using System;
using System.IO;

namespace MidiLoader.Events.ChannelVoice {
	
	public class NoteOffMidiEvent : ChannelVoiceMidiEvent {
		
		public ushort channel = 0;
		public ushort note = 0;
		public ushort velocity = 0;
		
		protected override byte firstFourBitsMask {
			get {
				return 0x80;
			}
		}
		
		protected override ushort dataBytesCount {
			get {
				return 2;
			}
		}

		protected override ChannelVoiceMidiEvent Construct( byte statusByte, byte[] dataBytes ) {
			NoteOffMidiEvent result = new NoteOffMidiEvent();

			result.channel  = (ushort)( statusByte & 0x0f );
			result.note     = (ushort)( dataBytes[0] & 0x7f );
			result.velocity = (ushort)( dataBytes[1] & 0x7f );

			return result;
		}

		public override string ToString() {
			return base.ToString() + " channel: " + channel + " note: " + note + " velocity: " + velocity;
		}
		
	}
	
}