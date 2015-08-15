using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using RangeAttribute = NUnit.Framework.RangeAttribute;
using MidiLoader.Parser;

namespace MidiLoader.Tests {
	
	[Category("MidiLoader")]
	[TestFixture]
	public class HeaderParserTests {
		
		[ExpectedException( typeof( MidiLoader.Exceptions.MidiEndOfFileException ) )]
		[Test]
		public void EndOfFileException() {
			byte[] bytes = new byte[] { };
			AssertHeader( bytes, HeaderFormat.SINGLE_TRACK, 1, 1 );
		}
		
		[Test]
		public void SimpliestHeader() {
			List<byte> bytes = new List<byte>();
			bytes.AddRange( ParseUtils.ASCIIToBytes( "MThd" ) );
			bytes.AddRange( new byte[] { 0x00, 0x00, 0x00, 0x06 } );
			bytes.AddRange( new byte[] { 0x00, 0x00} );
			bytes.AddRange( new byte[] { 0x00, 0x01} );
			bytes.AddRange( new byte[] { 0x00, 0x01} );
			
			AssertHeader( bytes.ToArray(), HeaderFormat.SINGLE_TRACK, 1, 1 );
		}
		
		[Test]
		public void VariableFormats() {
			foreach ( HeaderFormat format in Enum.GetValues( typeof( HeaderFormat ) ) ) {
				List<byte> bytes = new List<byte>();
				bytes.AddRange( ParseUtils.ASCIIToBytes( "MThd" ) );
				bytes.AddRange( new byte[] { 0x00, 0x00, 0x00, 0x06 } );
				bytes.AddRange( new byte[] { 0x00, (byte)format} );
				bytes.AddRange( new byte[] { 0x00, 0x01} );
				bytes.AddRange( new byte[] { 0x00, 0x01} );
				AssertHeader( bytes.ToArray(), format, 1, 1 );
			}
		}
		
		[Test]
		public void VariableTracksCount(
				[Values( 0x00, 0x9a, 0xff )] byte firstByte,
				[Values( 0x00, 0x9a, 0xff )] byte secondByte
			) {
			List<byte> bytes = new List<byte>();
			bytes.AddRange( ParseUtils.ASCIIToBytes( "MThd" ) );
			bytes.AddRange( new byte[] { 0x00, 0x00, 0x00, 0x06 } );
			bytes.AddRange( new byte[] { 0x00, 0x00} );
			byte[] tracksCountBytes = new byte[] { firstByte, secondByte};
			bytes.AddRange( tracksCountBytes );
			bytes.AddRange( new byte[] { 0x00, 0x01} );

			ParseUtils.EnsureCorrectBytesOrderForBitConverter( tracksCountBytes );
			ushort tracksCount = BitConverter.ToUInt16( tracksCountBytes, 0 );
			AssertHeader( bytes.ToArray(), HeaderFormat.SINGLE_TRACK, tracksCount, 1 );
		}
		
		[Test]
		public void VariableTicksPerQuaterNote(
				[Values( 0x00, 0x9a, 0xff )] byte firstByte,
				[Values( 0x00, 0x9a, 0xff )] byte secondByte
			) {
			List<byte> bytes = new List<byte>();
			bytes.AddRange( ParseUtils.ASCIIToBytes( "MThd" ) );
			bytes.AddRange( new byte[] { 0x00, 0x00, 0x00, 0x06 } );
			bytes.AddRange( new byte[] { 0x00, 0x00} );
			bytes.AddRange( new byte[] { 0x00, 0x01} );
			byte[] ticksBytes = new byte[] { firstByte, secondByte};
			bytes.AddRange( ticksBytes );
			
			ParseUtils.EnsureCorrectBytesOrderForBitConverter( ticksBytes );
			ushort ticks = BitConverter.ToUInt16( ticksBytes, 0 );
			AssertHeader( bytes.ToArray(), HeaderFormat.SINGLE_TRACK, 1, ticks );
		}
		
		void AssertHeader( byte[] bytes, HeaderFormat format, ushort tracksCount, ushort ticksPerQuarterNote ) {
			MemoryStream fakeStream = new MemoryStream( bytes );
			HeaderInfo result = HeaderParser.Parse( fakeStream );
			
			Assert.That( result.format == format );
			Assert.That( result.tracksCount == tracksCount );
			//Assert.That( result.deltasDivision == ticksPerQuarterNote );
		}
		
	}
	
}