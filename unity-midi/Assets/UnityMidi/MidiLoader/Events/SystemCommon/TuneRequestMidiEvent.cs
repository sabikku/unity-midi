using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.SystemCommon {
	
	public class TuneRequestMidiEvent : SystemCommonMidiEvent {
		
		protected override byte statusByteMask {
			get {
				return 0xf6;
			}
		}
		
		protected override SystemCommonMidiEvent Construct( Stream stream ) {
			return new TuneRequestMidiEvent();
		}
		
	}
	
}