using System.Collections.Generic;
using UnityEngine;

namespace MidiFakeSequencer.Player {
	
	public interface ITrackListener {
		
		string trackName {
			get;
		}
		
		float secondsBeforeNote {
			get;
		}
		
		void OnNote( float currentTime );
		
		void OnUpdate( float currentTime );
	}
	
}