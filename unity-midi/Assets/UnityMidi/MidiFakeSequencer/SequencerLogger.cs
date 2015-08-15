using System.IO;
using System.Text;
using MidiUtils;

namespace MidiFakeSequencer {

	public static class SequencerLogger {

		public static void LogTo( Sequencer sequencer, string path ) {
			TimeSpanLogger.Begin();
			StringBuilder sb = new StringBuilder();

			for ( int i = 0; i < sequencer.fakeTracks.Length; i++ ) {
				sb.AppendLine( "Track " + sequencer.fakeTracks[ i ].name + ":" );
				for ( int j = 0; j < sequencer.fakeTracks[ i ].noteOns.Length; j++ ) {
					Sequencer.NoteOn on = sequencer.fakeTracks[ i ].noteOns[ j ];
					sb.AppendLine( " realtime: " + on.realtime.ToString( "#00000.000" )
					              #if SEQUENCE_NOTES
					              + " " + on.note
					              #endif
					              );
				}
			}

			#if !UNITY_WEBPLAYER
			StreamWriter writer = File.CreateText( path );
			string log = sb.ToString();
			foreach ( char c in log ) {
				writer.Write( c );
			}
			writer.Close();
			#endif
			
			TimeSpanLogger.EndAndLog( "Logged sequencer result to file" );
		}

	}

}
