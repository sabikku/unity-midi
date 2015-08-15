using System.Collections.Generic;
using UnityEngine;
using MidiUtils;

namespace MidiFakeSequencer.Player {


	public class SequencePlayer {

		SequenceAsset asset;

		List<ITrackListener> trackListeners = new List<ITrackListener>();
		
		PlayedTrack[] playedTracks;

		bool started = false;

		public SequencePlayer( SequenceAsset asset ) {
			this.asset = asset;
		}

		public void Start() {
			List<PlayedTrack> tracksToPlay = new List<PlayedTrack>();

			for ( int i = 0; i < trackListeners.Count; i++ ) {
				ITrackListener listener = trackListeners[ i ];
				Sequencer.FakeTrack track = FindTrackByName( listener.trackName );
				if ( track != null ) {
					tracksToPlay.Add( new PlayedTrack( track, listener ) );
				}
			}

			playedTracks = tracksToPlay.ToArray();
			started = true;
		}

		public void Stop() {
			playedTracks = null;
			started = false;
		}

		public void Update( float currentTime ) {
			foreach( PlayedTrack playedTrack in playedTracks ) {
				playedTrack.Update( currentTime );
			}
		}

		public void AddListener( ITrackListener listener ) {
			AssertPlayerNotStarted();
			trackListeners.Add( listener );
		}

		public void RemoveListener( ITrackListener listener ) {
			AssertPlayerNotStarted();
			trackListeners.Remove( listener );
		}

		public void ClearListeners() {
			AssertPlayerNotStarted();
			trackListeners.Clear();
		}
		
		public uint GetSumOfNoteOnsTillTime( float duration ) {
			uint sum = 0;
			List<Sequencer.FakeTrack> playedFakeTracks = new List<Sequencer.FakeTrack>();
			foreach( PlayedTrack playedTrack in playedTracks ) {
				if ( !playedFakeTracks.Contains( playedTrack.track ) ) {
					playedFakeTracks.Add( playedTrack.track );
					foreach ( Sequencer.NoteOn noteOn in playedTrack.track.noteOns ) {
						if ( noteOn.realtime < duration ) {
							sum++;
						} else {
							break;
						}
					}
				}
			}
			playedFakeTracks.Clear();
			return sum;
		}

		void AssertPlayerNotStarted() {
			if ( started ) {
				throw new Exceptions.FakeSequencerException( "Can't manage SequencePlayer listeners after player started!" );
			}
		}

		Sequencer.FakeTrack FindTrackByName( string name ) {

			for ( int i = 0; i < asset.tracks.Length; i++ ) {
				if ( asset.tracks[ i ].name.Equals( name ) ) {
					return asset.tracks[ i ];
				}
			}

			Debug.LogWarning( "Couldn't find track named: " + name );;
			return null;
		}
	}

}