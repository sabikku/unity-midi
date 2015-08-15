using System;
using System.IO;

namespace MidiLoader.Events {

	/// <summary> Events undefined by specification. Should be ignored. </summary>
	public class UndefinedMidiEvent : MidiEvent {

		static byte[] undefinedStatusBytes = new byte[] {
			0xf1,
			0xf4,
			0xf5,
			0xf9,
			0xfd
		};

		public override bool MatchesStatusByte( byte statusByte ) {
			return ( Array.IndexOf( undefinedStatusBytes, statusByte ) > 0 );
		}
		
		public override MidiEvent Construct( Stream stream, byte statusByte ) {
			return new UndefinedMidiEvent();
		}
		
	}
	
}