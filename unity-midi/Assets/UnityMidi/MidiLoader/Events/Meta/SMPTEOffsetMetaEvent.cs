using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public class SMPTEOffsetMetaEvent : MetaEvent {

		public ushort hoursOffset;
		public ushort minutesOffset;
		public ushort secondsOffset;
		public ushort frameOffset;
		public ushort hundtrethOfFrameOffset;

		static byte[] identifyingMask = new byte[] {
			0x54, 0x05
		};

		protected override byte[] identifyingBytesMask {
			get {
				return identifyingMask;
			}
		}

		protected override MetaEvent Construct( Stream stream ) {
			SMPTEOffsetMetaEvent result = new SMPTEOffsetMetaEvent();

			byte[] dataBytes = ParseUtils.SafelyReadBytes( stream, 5 );
			
			result.hoursOffset = (ushort)( dataBytes[ 0 ] );
			result.minutesOffset = (ushort)( dataBytes[ 1 ] );
			result.secondsOffset = (ushort)( dataBytes[ 2 ] );
			result.frameOffset = (ushort)( dataBytes[ 3 ] );
			result.hundtrethOfFrameOffset = (ushort)( dataBytes[ 4 ] );

			return result;
		}
		
		public override string ToString() {
			return base.ToString() + string.Format( ", h: {0} m: {1} s: {2} f {3} ff {4}",
			                                       hoursOffset, minutesOffset, secondsOffset, frameOffset, hundtrethOfFrameOffset);
		}
	}
	
}