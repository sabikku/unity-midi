using System;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public class MidiPortMetaEvent : MetaEvent {
		
		public ushort midiPort;

		static byte[] identifyingMask = new byte[] {
			0x21, 0x01
		};

		protected override byte[] identifyingBytesMask {
			get {
				return identifyingMask;
			}
		}

		protected override MetaEvent Construct( Stream stream ) {
			MidiPortMetaEvent result = new MidiPortMetaEvent();
			
			byte dataByte = ParseUtils.SafelyReadByte( stream );
			result.midiPort = (ushort)( dataByte );

			return result;
		}
		
	}
	
}