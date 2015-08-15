using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public class SequencerSpecificMetaEvent : MetaEvent {
		
		public byte[] data;

		static byte[] identifyingMask = new byte[] {
			0x7f
		};

		protected override byte[] identifyingBytesMask {
			get {
				return identifyingMask;
			}
		}

		protected override MetaEvent Construct( Stream stream ) {
			SequencerSpecificMetaEvent result = new SequencerSpecificMetaEvent();

			ulong dataLength = VariableLengthQuantity.FromStream( stream );
			result.data = ParseUtils.SafelyReadBytes( stream, (int)dataLength );

			return result;
		}
		
	}
	
}