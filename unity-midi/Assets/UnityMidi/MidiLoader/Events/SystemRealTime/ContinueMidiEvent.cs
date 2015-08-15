using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.SystemRealTime {
	
	public class ContinueMidiEvent : SystemRealTimeMidiEvent<ContinueMidiEvent> {
		
		protected override byte statusByteMask {
			get {
				return 0xfb;
			}
		}
		
	}
	
}