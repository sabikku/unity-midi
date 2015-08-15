using UnityEngine;
using System.Collections;
using MidiFakeSequencer.Player;

public class SimpleBeatListener : MonoBehaviour, ITrackListener {

	public string explicitlyGaveTrackName;

	public string trackName {
		get {
			return explicitlyGaveTrackName;
		}
	}

	public float secondsBeforeNote {
		get {
			/* In rhythm games it's useful to know than the note is going to be played in the near future.
			 * In this example we just want to know the exact moment the note is being played. */
			return 0;
		}
	}

	public void OnNote( float currentTime ) {
		PlaySimpleBumpAnimation();
	}

	public void OnUpdate( float currentTime ) {
		/* In rhythm games, sometimes it's useful to not depend on the default Unity MonoBehaviour.Update(),
		 * but to be synchronized with played song - especially if it's glitching and pausing a lot on less efficient device.
		 * Use this function to be sure your time deltas goes on exactly as played music. */
	}

	void PlaySimpleBumpAnimation() {
		StopCoroutine( SimpleBumpAnimationEnumerator() );
		StartCoroutine( SimpleBumpAnimationEnumerator() );
	}

	IEnumerator SimpleBumpAnimationEnumerator() {
		float scale = 1.1f;
		do {
			transform.localScale = Vector3.one * scale;
			scale -= 0.01f;
			yield return new WaitForEndOfFrame();
		} while ( transform.localScale.magnitude > Vector3.one.magnitude );
	}
}
