using System;
using System.Text;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public class TrackOrSequenceNameMetaEvent : TextBasedMetaEvent {
		
		public string trackOrSequenceName;
		
		static byte[] identifyingMask = new byte[] {
			0x03
		};
		
		protected override byte[] identifyingBytesMask {
			get {
				return identifyingMask;
			}
		}
		
		protected override TextBasedMetaEvent Construct( string parsedText ) {
			TrackOrSequenceNameMetaEvent result = new TrackOrSequenceNameMetaEvent();
			result.trackOrSequenceName = parsedText;
			return result;
		}

		public override string ToString() {
			return base.ToString() + ", name: " + trackOrSequenceName;
		}
		
	}
	
}