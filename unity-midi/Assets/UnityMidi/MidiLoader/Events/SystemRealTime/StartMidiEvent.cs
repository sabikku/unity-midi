using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.SystemRealTime {
	
	public class StartMidiEvent : SystemRealTimeMidiEvent<StartMidiEvent> {
		
		protected override byte statusByteMask {
			get {
				return 0xfa;
			}
		}
		
	}
	
}