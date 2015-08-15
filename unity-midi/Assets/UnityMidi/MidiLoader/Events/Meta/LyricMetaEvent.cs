using System;
using System.Text;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public class LyricMetaEvent : TextBasedMetaEvent {
		
		public string lyric;
		
		static byte[] identifyingMask = new byte[] {
			0x05
		};
		
		protected override byte[] identifyingBytesMask {
			get {
				return identifyingMask;
			}
		}
		
		protected override TextBasedMetaEvent Construct( string parsedText ) {
			LyricMetaEvent result = new LyricMetaEvent();
			result.lyric = parsedText;
			return result;
		}
		
		public override string ToString() {
			return base.ToString() + ", lyric: " + lyric;
		}
		
	}
	
}