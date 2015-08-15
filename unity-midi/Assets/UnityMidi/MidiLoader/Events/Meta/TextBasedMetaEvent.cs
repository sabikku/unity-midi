using System;
using System.Text;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public abstract class TextBasedMetaEvent : MetaEvent {

		protected override MetaEvent Construct( Stream stream ) {

			ulong textLength = VariableLengthQuantity.FromStream( stream );
			byte[] textBytes = ParseUtils.SafelyReadBytes( stream, (int)textLength );

			return Construct( Encoding.ASCII.GetString( textBytes ) );

		}

		protected abstract TextBasedMetaEvent Construct( string parsedText );
		
	}
	
}