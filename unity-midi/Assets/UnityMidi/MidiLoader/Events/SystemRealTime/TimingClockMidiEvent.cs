using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.SystemRealTime {
	
	public class TimingClockMidiEvent : SystemRealTimeMidiEvent<TimingClockMidiEvent> {
		
		protected override byte statusByteMask {
			get {
				return 0xf8;
			}
		}
		
	}
	
}