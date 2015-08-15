using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public class EndOfTrackMetaEvent : MetaEvent {

		static byte[] identifyingMask = new byte[] {
			0x2f, 0x00
		};

		protected override byte[] identifyingBytesMask {
			get {
				return identifyingMask;
			}
		}

		protected override MetaEvent Construct( Stream stream ) {
			return new EndOfTrackMetaEvent();
		}
		
	}
	
}