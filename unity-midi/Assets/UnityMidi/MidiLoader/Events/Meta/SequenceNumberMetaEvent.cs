using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public class SequenceNumberMetaMidiEvent : MetaEvent {

		public ushort sequenceNo;

		static byte[] identifyingMask = new byte[] {
			0x00, 0x02
		};

		protected override byte[] identifyingBytesMask {
			get {
				return identifyingMask;
			}
		}

		protected override MetaEvent Construct( Stream stream ) {
			SequenceNumberMetaMidiEvent result = new SequenceNumberMetaMidiEvent();

			byte dataByte = ParseUtils.SafelyReadByte( stream );
			result.sequenceNo = (ushort)dataByte;

			return result;
		}
		
		public override string ToString() {
			return base.ToString() + ", sequenceNo: " + sequenceNo;
		}
		
	}
	
}