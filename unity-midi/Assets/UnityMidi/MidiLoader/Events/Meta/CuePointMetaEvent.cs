using System;
using System.Text;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public class CuePointMetaEvent : TextBasedMetaEvent {
		
		public string cue;
		
		static byte[] identifyingMask = new byte[] {
			0x07
		};
		
		protected override byte[] identifyingBytesMask {
			get {
				return identifyingMask;
			}
		}
		
		protected override TextBasedMetaEvent Construct( string parsedText ) {
			CuePointMetaEvent result = new CuePointMetaEvent();
			result.cue = parsedText;
			return result;
		}
		
		public override string ToString() {
			return base.ToString() + ", cue: " + cue;
		}
		
	}
	
}