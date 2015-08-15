using System.Collections.Generic;
using System.IO;
using System.Text;
using MidiLoader.Exceptions;
using MidiLoader.Parser;
using UnityEngine;
using MidiUtils;

namespace MidiLoader {

	public class Loader {

		public string streamName;
		public HeaderInfo headerInfo;
		public TrackInfo[] trackInfos;

		public Loader( string path ) {

			TimeSpanLogger.Begin();

			FileStream stream = File.OpenRead( path );
			stream.Position = 0;
			streamName = stream.Name;

			try {
				headerInfo = HeaderParser.Parse( stream );

				trackInfos = new TrackInfo[ headerInfo.tracksCount ];

				for ( int i = 0; i < trackInfos.Length; i++ ) {
					trackInfos[ i ] = new TrackParser().Parse( stream, i );
				}

			} catch ( MidiParseException e ) {
				Debug.LogError( e.ToString() );
			}
			
			stream.Close();

			TimeSpanLogger.EndAndLog( "Parsed " + streamName );
		}

	}

}