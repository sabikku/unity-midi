using System;
using System.Text;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public class MarkerMetaEvent : TextBasedMetaEvent {
		
		public string marker;
		
		static byte[] identifyingMask = new byte[] {
			0x06
		};
		
		protected override byte[] identifyingBytesMask {
			get {
				return identifyingMask;
			}
		}
		
		protected override TextBasedMetaEvent Construct( string parsedText ) {
			MarkerMetaEvent result = new MarkerMetaEvent();
			result.marker = parsedText;
			return result;
		}
		
		public override string ToString() {
			return base.ToString() + ", marker: " + marker;
		}
		
	}
	
}