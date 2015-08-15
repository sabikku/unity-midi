using System;
using System.IO;

namespace MidiLoader.Events.SystemRealTime {
	
	public abstract class SystemRealTimeMidiEvent<T> : MidiEvent
			where T : MidiEvent, new() {
		
		public override bool MatchesStatusByte( byte statusByte ) {
			return ( statusByte == statusByteMask );
		}
		
		protected abstract byte statusByteMask {
			get;
		}
		
		public override MidiEvent Construct( Stream stream, byte statusByte ) {
			return new T();
		}

	}
	
}