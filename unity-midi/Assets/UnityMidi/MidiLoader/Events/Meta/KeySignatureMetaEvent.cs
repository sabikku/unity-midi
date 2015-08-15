using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public class KeySignatureMetaEvent : MetaEvent {
		
		public ushort sharpFlats;
		public bool isMajorKey;

		static byte[] identifyingMask = new byte[] {
			0x59, 0x02
		};

		protected override byte[] identifyingBytesMask {
			get {
				return identifyingMask;
			}
		}

		protected override MetaEvent Construct( Stream stream ) {
			KeySignatureMetaEvent result = new KeySignatureMetaEvent();
			
			byte[] dataBytes = ParseUtils.SafelyReadBytes( stream, 2 );
			
			result.sharpFlats = (ushort)( dataBytes[ 0 ] );
			ushort mayorSindicator = (ushort)( dataBytes[ 1 ] );
			result.isMajorKey = mayorSindicator == 0;

			return result;
		}
		
	}
	
}