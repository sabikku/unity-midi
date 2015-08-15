using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using MidiLoader.Exceptions;
using MidiLoader.Events;
using MidiLoader.Events.Meta;

namespace MidiLoader.Parser {
	
	public class TrackParser {

		public TrackInfo trackInfo {
			get;
			private set;
		}

		// "MTrk" on track start
		static byte[] mtrkBytes = new byte[] { 0x4d, 0x54, 0x72, 0x6b };

		public TrackInfo Parse( FileStream stream, int trackIndex ) {
			TrackInfo result = new TrackInfo();
			result.index = trackIndex; 

			ParseMtrk( stream );
			OmitIgnoredTrackSize( stream );

			MidiEventParser eventParser = new MidiEventParser();
			List<MidiEvent> events = new List<MidiEvent>();
			MidiEvent midiEvent = null;
			while ( !( midiEvent is EndOfTrackMetaEvent ) && stream.Position < stream.Length-1 ) {
				midiEvent = eventParser.Parse( stream );
				events.Add( midiEvent );
			}
			result.events = events.ToArray();

			return result;
		}
		
		void ParseMtrk( FileStream stream ) {
			byte[] fourBytes = ParseUtils.SafelyReadBytes( stream, 4 );
			
			if ( !ParseUtils.ByteArrayValuesEqual( fourBytes, mtrkBytes ) ) {
				throw new MidiParseException( "Invalid track " + GetTrackName() + " header, it's not a 'Mtrk'! It's "
				                             + BitConverter.ToString( fourBytes ) );
			}
		}

		void OmitIgnoredTrackSize( FileStream stream ) {
			ParseUtils.SafelyReadBytes( stream, 4 );
		}

		string GetTrackName() {
			if ( !string.IsNullOrEmpty( trackInfo.name ) ) {
				return trackInfo.name;
			}
			return trackInfo.index.ToString();
		}

	}

}