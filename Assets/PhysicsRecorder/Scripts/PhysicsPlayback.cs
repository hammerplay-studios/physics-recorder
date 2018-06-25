using UnityEngine;

namespace Hammerplay.Utils.PhysicsRecorder {
	public class PhysicsPlayback : MonoBehaviour {

		[SerializeField]
		private RecorderStorage storage;

		private Transform[] childrenFoundInStorage;

		private void Start() {
			childrenFoundInStorage = storage.Prepare(GetComponentsInChildren<Transform>());
		}

		private int frames = 0;

		public int Frames {
			get { return frames; }
		}

		private bool hasReachedEnd;

		private bool isPlaying;

		private void Update() {

			if (!isPlaying)
				return;

			if (hasReachedEnd)
				return;

			frames++;

			for (int i = 0; i < childrenFoundInStorage.Length; i++) {

				if (frames < storage.GetFrameCount(childrenFoundInStorage[i])) {
					Vector3 position;
					Quaternion rotation;

					storage.GetRecordedData(childrenFoundInStorage[i], frames, out position, out rotation);

					childrenFoundInStorage[i].localPosition = position;
					childrenFoundInStorage[i].localRotation = rotation;
				} else {
					hasReachedEnd = true;
				}
			}

		}

		public void Play() {
			isPlaying = true;
		}

		public void Stop () {
			isPlaying = false;
		}
	}
}
