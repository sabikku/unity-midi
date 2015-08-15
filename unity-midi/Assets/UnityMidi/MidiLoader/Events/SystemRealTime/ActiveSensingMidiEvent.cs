using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.SystemRealTime {
	
	public class ActiveSensingMidiEvent : SystemRealTimeMidiEvent<ActiveSensingMidiEvent> {
		
		protected override byte statusByteMask {
			get {
				return 0xfe;
			}
		}
		
	}
	
}