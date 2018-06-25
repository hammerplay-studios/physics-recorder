using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hammerplay.Utils.PhysicsRecorder {
	public class PhysicsRecorder : MonoBehaviour {

		[SerializeField]
		private RecorderStorage storage;

		[SerializeField]
		private int frameRate = 30;

		[SerializeField]
		private bool recordAtStart = false;

		private Rigidbody[] rigidbodies;

		private bool isRecording;

		private int frames;

		public int Frames {
			get { return frames; }
		}

		private void Start() {
			Application.targetFrameRate = frameRate;

			rigidbodies = GetComponentsInChildren<Rigidbody>();

			for (int i = 0; i < rigidbodies.Length; i++) {
				storage.AddRigidbody(rigidbodies[i]);
			}

			if (recordAtStart)
				StartRecording();
		}

		private void Update() {
			if (!isRecording)
				return;

			for (int i = 0; i < rigidbodies.Length; i++) {
				storage.RecordData(i, rigidbodies[i]);
			}

			frames++;
		}

		/// <summary>
		/// Starts recording. Make sure the storage asset file is empty
		/// </summary>
		public void StartRecording () {
			//storage.ClearData();
			isRecording = true;
		}

		/// <summary>
		/// Stops recording.
		/// </summary>
		public void StopRecording () {
			isRecording = false;
		}
	}
}
