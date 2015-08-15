using System;
using System.Text;
using System.IO;
using MidiLoader.Parser;

namespace MidiLoader.Events.Meta {
	
	public class DeviceNameMetaEvent : TextBasedMetaEvent {
		
		public string deviceName;

		static byte[] identifyingMask = new byte[] {
			0x09
		};

		protected override byte[] identifyingBytesMask {
			get {
				return identifyingMask;
			}
		}
		
		protected override TextBasedMetaEvent Construct( string parsedText ) {
			DeviceNameMetaEvent result = new DeviceNameMetaEvent();
			result.deviceName = parsedText;
			return result;
		}
		
		public override string ToString() {
			return base.ToString() + ", name: " + deviceName;
		}
		
	}
	
}