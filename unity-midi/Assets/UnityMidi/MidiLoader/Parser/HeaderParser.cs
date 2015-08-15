using System;
using System.IO;
using System.Collections.Generic;
using MidiLoader.Exceptions;

namespace MidiLoader.Parser {

	public class HeaderParser {

		// "MThd" on MIDI file start
		static byte[] mthdBytes = new byte[] { 0x4d, 0x54, 0x68, 0x64 };

		// Valid header should have exactly 6 bytes
		static byte[] validHeaderSizeBytes = new byte[] { 0x00, 0x00, 0x00, 0x06 };

		// Parser supports only one-track (0) and multi-tracks (1) MIDI files. The multi-song (2) format isn't supported.
		static Dictionary<HeaderFormat, byte[]> formatRepresentations = new Dictionary<HeaderFormat, byte[]>() {
			{ HeaderFormat.SINGLE_TRACK,    new byte[] { 0x00, 0x00 } },
			{ HeaderFormat.MULTIPLE_TRACKS, new byte[] { 0x00, 0x01 } },
			{ HeaderFormat.MULTIPLE_SONGS,  new byte[] { 0x00, 0x02 } }
		};

		public static HeaderInfo Parse( Stream stream ) {
			HeaderInfo headerInfo = new HeaderInfo();

			ParseMthd( stream );
			ParseHeaderSize( stream );

			headerInfo.format = ParseFormat( stream );
			headerInfo.tracksCount = ParseTracksCount( stream );
			headerInfo.deltasDivision = ParseDeltasDivision( stream );

			return headerInfo;
		}

		static void ParseMthd( Stream stream ) {
			byte[] fourBytes = ParseUtils.SafelyReadBytes( stream, 4 );
			
			if ( !ParseUtils.ByteArrayValuesEqual( fourBytes, mthdBytes ) ) {
				throw new MidiParseException( "Invalid MIDI header, it's not a 'Mtdh'! It's "
				                             + BitConverter.ToString( fourBytes ) );
			}
		}

		static void ParseHeaderSize( Stream stream ) {
			byte[] fourBytes = ParseUtils.SafelyReadBytes( stream, 4 );
			
			if ( !ParseUtils.ByteArrayValuesEqual( fourBytes, validHeaderSizeBytes ) ) {
				throw new MidiParseException( "Invalid MIDI header size, it's not 6! It's "
				                             + BitConverter.ToString( fourBytes ) );
			}
		}
		
		static HeaderFormat ParseFormat( Stream stream ) {
			byte[] twoBytes = ParseUtils.SafelyReadBytes( stream, 2 );

			foreach ( KeyValuePair<HeaderFormat, byte[]> kvp in formatRepresentations ) {
				if ( ParseUtils.ByteArrayValuesEqual( twoBytes, kvp.Value ) ) {
					return kvp.Key;
				}
			}

			throw new MidiParseException( "Invalid MIDI format! No format for given bytes "
				                             + BitConverter.ToString( twoBytes ) );
			
		}
		
		static ushort ParseTracksCount( Stream stream ) {
			byte[] twoBytes = ParseUtils.SafelyReadBytes( stream, 2 );
			ParseUtils.EnsureCorrectBytesOrderForBitConverter( twoBytes );
			return BitConverter.ToUInt16( twoBytes, 0 );
		}

		static DeltasDivision ParseDeltasDivision( Stream stream ) {
			DeltasDivision result = new DeltasDivision();
			byte[] twoBytes = ParseUtils.SafelyReadBytes( stream, 2 );

			if ( ( twoBytes[0] & 0x80 ) == 0x00 ) {
				TicksPerQuaterNoteDeltasDivision ticksResult = new TicksPerQuaterNoteDeltasDivision();
				ParseUtils.EnsureCorrectBytesOrderForBitConverter( twoBytes );
				ticksResult.ticksPerQuaterNote = BitConverter.ToUInt16( twoBytes, 0 );
				result = ticksResult;

			} else {
				SMPTESignatureDeltasDivision smpteResult = new SMPTESignatureDeltasDivision();
				twoBytes[0] -= 0x80;
				smpteResult.fps = (ushort)( twoBytes[0] );
				smpteResult.unitsPerFrame = (ushort)( twoBytes[1] );
				result = smpteResult;
			}

			return result;
		}

	}

}