using System;
using System.IO;

namespace MidiLoader.Events {

	public abstract class MidiEvent {

		public ulong deltas;

		public abstract bool MatchesStatusByte( byte statusByte );
		
		public abstract MidiEvent Construct( Stream stream, byte statusByte );

		public override string ToString()
		{
			return " - Deltas: " + deltas.ToString().PadLeft( 5 ) + ", " + GetType().Name;
		}

	}

}