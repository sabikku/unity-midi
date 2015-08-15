using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public abstract class MetaEvent : MidiEvent {
		
		public override bool MatchesStatusByte( byte statusByte ) {
			return ( statusByte == 0xff );
		}
		
		public bool MatchesIdentifyingBytes( byte[] identifyingBytes ) {
			return ParseUtils.ByteArrayValuesEqual( identifyingBytes, identifyingBytesMask, identifyingBytesMask.Length );
		}

		protected abstract byte[] identifyingBytesMask {
			get;
		}

		public override MidiEvent Construct( Stream stream, byte statusByte ) {
			OmitIdentifyingBytes( stream );
			return Construct( stream );
		}

		protected abstract MetaEvent Construct( Stream stream );

		void OmitIdentifyingBytes( Stream stream ) {
			ParseUtils.SafelyReadBytes( stream, identifyingBytesMask.Length );
		}
	}
	
}