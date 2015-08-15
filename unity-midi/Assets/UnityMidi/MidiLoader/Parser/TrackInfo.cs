using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using MidiLoader.Events;
using MidiUtils;

namespace MidiLoader.Parser {

	public class TrackInfo {
		public int index;
		public string name;
		public MidiEvent[] events;

		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.AppendLine( "Track " + index + ":" );
			for ( int i = 0; i < events.Length; i++ ) {
				sb.AppendLine( events[i].ToString() );
			}
			return sb.ToString();
		}
	}

}