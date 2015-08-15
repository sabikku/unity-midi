using System;
using System.IO;
using System.Collections.Generic;
using MidiLoader.Exceptions;

namespace MidiLoader.Parser {

	public enum HeaderFormat {
		SINGLE_TRACK = 0,
		MULTIPLE_TRACKS = 1,
		MULTIPLE_SONGS = 2
	}

	public class DeltasDivision { };

	public class TicksPerQuaterNoteDeltasDivision : DeltasDivision {
		public ushort ticksPerQuaterNote;

		public override string ToString() {
			return string.Format( "ticksPerQuaterNote: {0}", ticksPerQuaterNote );
		}
	}

	public class SMPTESignatureDeltasDivision : DeltasDivision {
		public ushort fps;
		public ushort unitsPerFrame;
		
		public override string ToString() {
			return string.Format( "SMPT fps: {0}, unitsPerFrame: {1}", fps, unitsPerFrame );
		}
	}

	public class HeaderInfo {
		public HeaderFormat format;
		public ushort tracksCount;
		public DeltasDivision deltasDivision;

		public override string ToString() {
			return string.Format( "Header:\n - format: {0} \n - tracksCount: {1} \n - deltasDivision: {2}",
			                     format, tracksCount, deltasDivision );
		}
	}

}