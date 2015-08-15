using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.SystemRealTime {
	
	public class StopMidiEvent : SystemRealTimeMidiEvent<StopMidiEvent> {
		
		protected override byte statusByteMask {
			get {
				return 0xfc;
			}
		}
		
	}
	
}