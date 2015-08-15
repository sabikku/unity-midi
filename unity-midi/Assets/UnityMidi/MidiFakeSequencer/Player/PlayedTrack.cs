using System.Collections.Generic;
using UnityEngine;

namespace MidiFakeSequencer.Player {

	internal class PlayedTrack {
		
		public Sequencer.FakeTrack track {
			get;
			private set;
		}

		ITrackListener listener;

		int noteIndexToPlay = 0;

		public PlayedTrack( Sequencer.FakeTrack track, ITrackListener listener ) {
			this.track = track;
			this.listener = listener;
		}

		public void Update( float currentTime ) {
			while( noteIndexToPlay < track.noteOns.Length &&
			      ( currentTime + listener.secondsBeforeNote ) > track.noteOns[ noteIndexToPlay ].realtime ) {
				noteIndexToPlay++;
				listener.OnNote( currentTime );
			}

			listener.OnUpdate( currentTime );
		}
	}
	
}