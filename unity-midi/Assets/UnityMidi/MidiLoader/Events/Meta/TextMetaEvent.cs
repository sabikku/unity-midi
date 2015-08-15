using System;
using System.Text;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public class TextMetaEvent : TextBasedMetaEvent {
		
		public string text;

		static byte[] identifyingMask = new byte[] {
			0x01
		};

		protected override byte[] identifyingBytesMask {
			get {
				return identifyingMask;
			}
		}
		
		protected override TextBasedMetaEvent Construct( string parsedText ) {
			TextMetaEvent result = new TextMetaEvent();
			result.text = parsedText;
			return result;
		}
		
		public override string ToString() {
			return base.ToString() + ", text: " + text;
		}
		
	}
	
}