using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace MidiLoader.Parser {

	public static class VariableLengthQuantity {
		
		const int MAX_VLQ_BYTES = 8;

		public static ulong FromStream( Stream stream ) {

			ulong result = 0;

			int index = 0;
			do {
				byte singleByte = ParseUtils.SafelyReadByte( stream );

				if ( singleByte > 0 || index > 0 ) {
					result = Insert7BitsOnEnd( result, singleByte );
				}

				index++;
				if ( index == MAX_VLQ_BYTES || IsThisTheLastByte( singleByte ) ) {
					break;
				}

			} while ( stream.Position < stream.Length );

			return result;

		}

		static ulong Insert7BitsOnEnd( ulong integer, short insertion ) {
			integer <<= 7;
			integer |= ( (ulong)insertion & 0x7ful );
			return integer;
		}

		static bool IsThisTheLastByte( short bytee ) {
			return ( ( bytee & 0x80 ) != 0x80 );
		}
	}

}

