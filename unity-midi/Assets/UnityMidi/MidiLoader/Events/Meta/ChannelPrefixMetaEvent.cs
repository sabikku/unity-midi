using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public class ChannelPrefixMetaEvent : MetaEvent {
		
		public ushort channel;
		
		static byte[] twoBytesMask = new byte[] {
			0x20, 0x01
		};

		protected override byte[] identifyingBytesMask {
			get {
				return twoBytesMask;
			}
		}

		protected override MetaEvent Construct( Stream stream ) {
			ChannelPrefixMetaEvent result = new ChannelPrefixMetaEvent();

			byte dataByte = ParseUtils.SafelyReadByte( stream );
			result.channel = (ushort)dataByte;

			return result;
		}
		
	}
	
}