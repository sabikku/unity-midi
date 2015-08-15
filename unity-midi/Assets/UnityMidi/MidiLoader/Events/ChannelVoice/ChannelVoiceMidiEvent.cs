using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.ChannelVoice {
	
	public abstract class ChannelVoiceMidiEvent : MidiEvent {

		public override bool MatchesStatusByte( byte statusByte ) {
			byte firstFourBits = (byte)( statusByte & 0xf0 );
			return ( firstFourBits == firstFourBitsMask );
			
		}
		
		protected abstract byte firstFourBitsMask {
			get;
		}
		
		protected abstract ushort dataBytesCount {
			get;
		}

		public override MidiEvent Construct( Stream stream, byte statusByte ) {
			byte[] dataBytes;
			if ( dataBytesCount > 0 ) {
				dataBytes = ParseUtils.SafelyReadBytes( stream, dataBytesCount );
			} else {
				dataBytes = new byte[] {};
			}
			return Construct( statusByte, dataBytes );
		}
		
		protected abstract ChannelVoiceMidiEvent Construct( byte statusByte, byte[] dataBytes );
	}
	
}