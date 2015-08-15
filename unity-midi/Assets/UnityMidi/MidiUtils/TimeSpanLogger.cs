using System;
using System.Collections.Generic;
using UnityEngine;

namespace MidiUtils {

	public static class TimeSpanLogger {
		
		static Stack<DateTime> befores = new Stack<DateTime>();
		
		public static void Begin() {
			befores.Push( DateTime.Now );
		}
		
		public static void EndAndLog( string whatsDone ) {
			TimeSpan span = DateTime.Now - befores.Pop();
			Debug.Log( whatsDone + " in " + span.Seconds + " seconds and " + span.Milliseconds + " miliseconds." );
		}

	}

}