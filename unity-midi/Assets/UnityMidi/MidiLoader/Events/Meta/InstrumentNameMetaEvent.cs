using System;
using System.Text;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public class InstrumentNameMetaEvent : TextBasedMetaEvent {
		
		public string instrumentName;

		static byte[] identifyingMask = new byte[] {
			0x04
		};

		protected override byte[] identifyingBytesMask {
			get {
				return identifyingMask;
			}
		}
		
		protected override TextBasedMetaEvent Construct( string parsedText ) {
			InstrumentNameMetaEvent result = new InstrumentNameMetaEvent();
			result.instrumentName = parsedText;
			return result;
		}
		
		public override string ToString() {
			return base.ToString() + ", name: " + instrumentName;
		}
		
	}
	
}