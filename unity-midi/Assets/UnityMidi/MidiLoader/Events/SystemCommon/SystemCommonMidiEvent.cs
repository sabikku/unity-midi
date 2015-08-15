using System;
using System.IO;

namespace MidiLoader.Events.SystemCommon {
	
	public abstract class SystemCommonMidiEvent : MidiEvent {
		
		public override bool MatchesStatusByte( byte statusByte ) {
			return ( statusByte == statusByteMask );
		}
		
		protected abstract byte statusByteMask {
			get;
		}

		public override MidiEvent Construct( Stream stream, byte statusByte ) {
			return Construct( stream );
		}

		protected abstract SystemCommonMidiEvent Construct( Stream stream );

	}
	
}