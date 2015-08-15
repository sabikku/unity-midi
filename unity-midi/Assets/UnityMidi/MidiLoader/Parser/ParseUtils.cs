using System.Text;
using System;
using System.IO;
using MidiLoader.Exceptions;
using System.Collections.Generic;

namespace MidiLoader.Parser {

	public static class ParseUtils {

		public static bool ByteArrayValuesEqual( byte[] a1, byte[] a2 ) {
			if ( a1.Length != a2.Length ) {
				return false;
			}
			
			for ( int i = 0; i < a1.Length; i++ ) {
				if ( a1[i] != a2[i] ) {
					return false;
				}
			}
			
			return true;
		}

		public static bool ByteArrayValuesEqual( byte[] a1, byte[] a2, int checkLength ) {
			if ( a1.Length < checkLength || a2.Length < checkLength ) {
				return false;
			}
			
			for ( int i = 0; i < checkLength; i++ ) {
				if ( a1[i] != a2[i] ) {
					return false;
				}
			}
			
			return true;
		}

		public static void EnsureCorrectBytesOrderForBitConverter( byte[] byteArray ) {
			if ( BitConverter.IsLittleEndian ) {
				Array.Reverse( byteArray );
			}
		}
		
		public static byte SafelyReadByte( Stream stream ) {
			int readByte = stream.ReadByte();
			if ( readByte == -1 ) {
				throw new MidiEndOfFileException();
			}
			return (byte)readByte;
		}
		
		public static byte[] SafelyReadBytes( Stream stream, int bytesCount ) {
			byte[] result = new byte[ bytesCount ];
			int bytesRead = stream.Read( result, 0, bytesCount );
			if ( bytesRead != bytesCount ) {
				throw new MidiEndOfFileException();
			}
			return result;
		}
		
		public static byte[] ASCIIToBytes(string str)
		{
			byte[] bytes = System.Text.Encoding.ASCII.GetBytes( str );
			return bytes;
		}

	}

}