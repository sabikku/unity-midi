using UnityEngine;
using System.Collections;

public class ExampleBehaviour : MonoBehaviour {

	public MidiFakeSequencer.SequenceAsset assetFile;
	AudioSource audioSource;
	MidiFakeSequencer.Player.SequencePlayer sequencePlayer;

	void Start() {
		audioSource = GetComponent<AudioSource>();
		CreatePlayerAndListeners();
		PlaySourceAndStartPlayer();
		AssignButtonsListeners();
	}
	
	void CreatePlayerAndListeners() {
		sequencePlayer = new MidiFakeSequencer.Player.SequencePlayer( assetFile );

		/*
		 * We'll create a SimpleBeatListener container for each track stored in asset file.
		 */
		GameObject beatRepresentationPrototype = GameObject.Find( "BeatIndicator" );
		for ( int i = 0; i < assetFile.tracks.Length; i++ ) {
			MidiFakeSequencer.Sequencer.FakeTrack track = assetFile.tracks[ i ];

			/*
			 * UI:
			 */
			GameObject beatRepresentation;
			if ( i == 0 ) {
				beatRepresentation = beatRepresentationPrototype;
			} else {
				beatRepresentation = GameObject.Instantiate( beatRepresentationPrototype );
				beatRepresentation.transform.SetParent( beatRepresentationPrototype.transform.parent, false );
			}
			UnityEngine.UI.Text text = beatRepresentation.transform.FindChild( "Text" ).GetComponent<UnityEngine.UI.Text>();
			text.text = track.name;

			/*
			 * And here's the important code:
			 */
			SimpleBeatListener beatListener = beatRepresentation.AddComponent<SimpleBeatListener>();
			beatListener.explicitlyGaveTrackName = track.name;
			sequencePlayer.AddListener( beatListener );
		}
	}
	
	void PlaySourceAndStartPlayer() {
		audioSource.Play();
		sequencePlayer.Start();
	}
	
	void AssignButtonsListeners() {
		UnityEngine.UI.Button pauseButton = GameObject.Find( "PausePlayButton" ).GetComponent<UnityEngine.UI.Button>();
		UnityEngine.UI.Button restartButton = GameObject.Find( "RestartButton" ).GetComponent<UnityEngine.UI.Button>();
		pauseButton.onClick.AddListener( OnClickPausePlay );
		restartButton.onClick.AddListener( OnClickRestart );
	}

	void OnClickPausePlay() {
		if ( audioSource.isPlaying ) {
			audioSource.Pause();
		} else {
			audioSource.Play();
		}
	}

	void OnClickRestart() {
		audioSource.Stop();
		sequencePlayer.Stop();
		audioSource.Play();
		sequencePlayer.Start();
	}

	void Update() {
		/*
		 * This way it's always synchronized. This triggers OnUpdate and OnNote on assigned listeners.
		 */
		sequencePlayer.Update( audioSource.time );
	}

}
