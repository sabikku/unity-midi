using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public class BlankMetaEvent : MetaEvent {

		static byte[] identifyingMask = new byte[] { };
		
		protected override byte[] identifyingBytesMask {
			get {
				return identifyingMask;
			}
		}
		
		protected override MetaEvent Construct( Stream stream ) {
			BlankMetaEvent result = new BlankMetaEvent();
			return result;
		}
		
	}
	
}