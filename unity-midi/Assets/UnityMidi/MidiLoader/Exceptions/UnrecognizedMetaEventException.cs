
using System;

namespace MidiLoader.Exceptions {
	
	public class UnrecognizedMetaEventException : MidiParseException {
		
		public UnrecognizedMetaEventException( byte[] dataBytes )
		: base( "Unrecognized meta midi event! Data bytes: " + BitConverter.ToString( dataBytes, 0 ) ) {
			
		}
		
	}
	
}