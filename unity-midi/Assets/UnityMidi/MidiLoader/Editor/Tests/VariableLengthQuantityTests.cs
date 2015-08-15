using UnityEngine;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using MidiLoader.Parser;

namespace MidiLoader.Tests {
	
	[Category("MidiLoader")]
	[TestFixture]
	public class VariableLengthQuantityTests {

		[ExpectedException( typeof( MidiLoader.Exceptions.MidiEndOfFileException ) )]
		[Test]
		public void EndOfFileException() {
			ParseVariableLengthQuantity( new byte[] { } );
		}
		
		#pragma warning disable 414
		Dictionary<ulong, byte[]> midiSpecificationCases = new Dictionary<ulong, byte[]>() {
			{ 0x00000000, new byte[] { 0x00, 0x01, 0x01, 0x01, 0x01 } },
			{ 0x0000007f, new byte[] { 0x7f, 0x01, 0x01, 0x01, 0x01 } },
			{ 0x00000080, new byte[] { 0x81, 0x00, 0x01, 0x01, 0x01 } },
			{ 0x00002000, new byte[] { 0xc0, 0x00, 0x01, 0x01, 0x01 } },
			{ 0x00003fff, new byte[] { 0xff, 0x7f, 0x01, 0x01, 0x01 } },
			{ 0x00004000, new byte[] { 0x81, 0x80, 0x00, 0x01, 0x01 } },
			{ 0x001fffff, new byte[] { 0xff, 0xff, 0x7f, 0x01, 0x01 } },
			{ 0x00200000, new byte[] { 0x81, 0x80, 0x80, 0x00, 0x01 } },
			{ 0x08000000, new byte[] { 0xc0, 0x80, 0x80, 0x00, 0x01 } },
			{ 0x0fffffff, new byte[] { 0xff, 0xff, 0xff, 0x7f, 0x01 } }
		};

		[Test]
		public void ExamplesFromMidiSpecification(
				[ValueSource( "midiSpecificationCases" )] KeyValuePair<ulong, byte[]> kvp
			) {
			
			ulong result = ParseVariableLengthQuantity( kvp.Value );
			Assert.That( result == kvp.Key );
		}


		ulong ParseVariableLengthQuantity( byte[] bytes ) {
			MemoryStream fakeStream = new MemoryStream( bytes );
			return VariableLengthQuantity.FromStream( fakeStream );
		}

	}

}