using System;
using System.Text;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public class CopyrightNoticeMetaEvent : TextBasedMetaEvent {
		
		public string copyright;
		
		static byte[] identifyingMask = new byte[] {
			0x02
		};
		
		protected override byte[] identifyingBytesMask {
			get {
				return identifyingMask;
			}
		}
		
		protected override TextBasedMetaEvent Construct( string parsedText ) {
			CopyrightNoticeMetaEvent result = new CopyrightNoticeMetaEvent();
			result.copyright = parsedText;
			return result;
		}
		
		public override string ToString() {
			return base.ToString() + ", copyright: " + copyright;
		}
		
	}
	
}