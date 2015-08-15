
using System;

namespace MidiLoader.Exceptions {
	
	public class UnrecognizedMidiEventException : MidiParseException {
		
		public UnrecognizedMidiEventException( byte statusByte )
		: base( "Unrecognized midi event! Status byte: " + statusByte ) {
			
		}
		
	}
	
}