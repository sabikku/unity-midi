using System.IO;
using System.Text;
using MidiLoader.Parser;
using MidiUtils;

namespace MidiLoader {
	
	public static class LoaderLogger {
		
		public static void LogTo( Loader loader, string path ) {
			TimeSpanLogger.Begin();
			StringBuilder sb = new StringBuilder();
			
			LogStreamNameAndHeader( loader.headerInfo, path, sb );
			for ( int i = 0; i < loader.trackInfos.Length; i++ ) {
				LogTrack( loader.trackInfos[i], sb );
			}
			
			#if !UNITY_WEBPLAYER
			StreamWriter writer = File.CreateText( path );
			string log = sb.ToString();
			foreach ( char c in log ) {
				writer.Write( c );
			}
			writer.Close();
			#endif
			
			TimeSpanLogger.EndAndLog( "Logged parsing result to file" );
		}
		
		static void LogStreamNameAndHeader( HeaderInfo info, string path, StringBuilder output ) {
			output.AppendLine( "File: " + path + ":" );
			output.AppendLine( info.ToString() );
		}
		
		static void LogTrack( TrackInfo info, StringBuilder output ) {
			output.AppendLine( info.ToString() );
		}
		
	}
	
}
