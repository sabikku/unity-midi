using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public class SetTempoMetaEvent : MetaEvent {

		public uint microsecondsPerQuaterNote;

		static byte[] identifyingMask = new byte[] {
			0x51, 0x03
		};

		protected override byte[] identifyingBytesMask {
			get {
				return identifyingMask;
			}
		}

		protected override MetaEvent Construct( Stream stream ) {
			SetTempoMetaEvent result = new SetTempoMetaEvent();

			byte[] dataBytes = ParseUtils.SafelyReadBytes( stream, 3 );

			byte[] dataBytesIn32Bits = new byte[] {
				0x00,
				dataBytes[0],
				dataBytes[1],
				dataBytes[2]
			};

			ParseUtils.EnsureCorrectBytesOrderForBitConverter( dataBytesIn32Bits );

			result.microsecondsPerQuaterNote = BitConverter.ToUInt32( dataBytesIn32Bits, 0 );

			return result;
		}

		public override string ToString() {
			return base.ToString() + ", microsecondsPerQuaterNote: " + microsecondsPerQuaterNote;
		}

	}
	
}