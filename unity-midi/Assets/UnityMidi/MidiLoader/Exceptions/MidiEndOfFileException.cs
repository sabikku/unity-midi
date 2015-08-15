
using System;

namespace MidiLoader.Exceptions {
	
	public class MidiEndOfFileException : MidiParseException {
		
		public MidiEndOfFileException()
			: base( "Unexpected end of MIDI file!" ) {
			
		}
		
	}
	
}