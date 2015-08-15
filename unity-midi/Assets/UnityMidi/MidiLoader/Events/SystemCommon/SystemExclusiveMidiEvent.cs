using System;
using System.Collections.Generic;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.SystemCommon {
	
	public class SystemExclusiveMidiEvent : SystemCommonMidiEvent {

		public byte[] data;

		protected override byte statusByteMask {
			get {
				return 0xf0;
			}
		}

		const byte endOfExclusiveByte = (byte)( 0xf7 );

		protected override SystemCommonMidiEvent Construct( Stream stream ) {
			SystemExclusiveMidiEvent result = new SystemExclusiveMidiEvent();

			List<byte> bytes = new List<byte>();
			do {
				bytes.Add( ParseUtils.SafelyReadByte( stream ) );
			} while ( bytes[bytes.Count - 1] != endOfExclusiveByte );
			bytes.RemoveAt( bytes.Count - 1 );

			result.data = bytes.ToArray();
			return result;
		}
		
	}
	
}