using UnityEngine;

namespace MidiFakeSequencer {

	[System.Serializable]
	public class SequenceAsset : ScriptableObject {

		public Sequencer.FakeTrack[] tracks;

	}

}