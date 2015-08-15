using System;
using System.Text;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public class ProgramNameMetaEvent : TextBasedMetaEvent {
		
		public string programName;

		static byte[] identifyingMask = new byte[] {
			0x08
		};

		protected override byte[] identifyingBytesMask {
			get {
				return identifyingMask;
			}
		}
		
		protected override TextBasedMetaEvent Construct( string parsedText ) {
			ProgramNameMetaEvent result = new ProgramNameMetaEvent();
			result.programName = parsedText;
			return result;
		}
		
		public override string ToString() {
			return base.ToString() + ", name: " + programName;
		}
		
	}
	
}