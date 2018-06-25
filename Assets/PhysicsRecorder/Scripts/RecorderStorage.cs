using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace Hammerplay.Utils.PhysicsRecorder {
	[CreateAssetMenuAttribute]
	public class RecorderStorage : ScriptableObject {

		[SerializeField]
		private List<RecordedRigidbody> recordedRigidbodies;

		public void ClearData() {
			recordedRigidbodies.Clear();
		}

		public void AddRigidbody (Rigidbody rigidbody) {
			recordedRigidbodies.Add(new RecordedRigidbody(rigidbody.name));
		}

		public void RecordData (int index, Rigidbody rigidBody) {
			recordedRigidbodies[index].Record(rigidBody);
		}

		public string[] GetRigidbodyNames () {
			string[] rigidbodyNames = new string[recordedRigidbodies.Count];

			for (int i = 0; i < recordedRigidbodies.Count; i++) {
				rigidbodyNames[i] = recordedRigidbodies[i].RigidbodyName;
			}

			return rigidbodyNames;
		}

		
		private Dictionary<string, RecordedRigidbody> transformDictionary;

		private void CopyListToDictionary () {
			transformDictionary = new Dictionary<string, RecordedRigidbody>();

			for (int i = 0; i < recordedRigidbodies.Count; i++) {
				transformDictionary.Add(recordedRigidbodies[i].RigidbodyName, recordedRigidbodies[i]);
			}
		}

		private Dictionary<Transform, RecordedRigidbody> mappedTransformDictionary;

		private void MapTransformDictionary(Transform[] children) {
			List<string> storedRigidbodyNames = transformDictionary.Keys.ToList();

			mappedTransformDictionary = new Dictionary<Transform, RecordedRigidbody>();

			for (int i = 0; i < children.Length; i++) {
				if (storedRigidbodyNames.Contains (children[i].name)) {
					mappedTransformDictionary.Add(children[i], transformDictionary[children[i].name]);
				} /*else {
					Debug.LogError(children[i].name + " is not found in the records");
				}*/
			}
		}

		public Transform[] Prepare (Transform[] children) {
			CopyListToDictionary();
			MapTransformDictionary(children);

			return mappedTransformDictionary.Keys.ToArray();
		}

		public bool GetRecordedData (Transform transform, int frame, out Vector3 position, out Quaternion rotation) {
			position = Vector3.zero;
			rotation = Quaternion.identity;

			if (mappedTransformDictionary.ContainsKey (transform)) {
				mappedTransformDictionary[transform].GetData(frame, out position, out rotation);
				return true;
			}

			return false;
		}

		public int GetFrameCount (Transform transform) {
			if (mappedTransformDictionary.ContainsKey (transform)) {
				return mappedTransformDictionary[transform].GetFrameCount;
			}

			return 0;
		}

	}

	[System.Serializable]
	public class RecordedRigidbody {

		[SerializeField]
		private string rigidbodyName;

		public string RigidbodyName {
			get { return rigidbodyName; }
		}

		[SerializeField]
		private List<Data> data;

		public int GetFrameCount {
			get { return data.Count; }
		}

		public RecordedRigidbody(string rigidbodyName) {
			this.rigidbodyName = rigidbodyName;
			data = new List<Data>();
		}

		public void Record (Rigidbody rigidbody) {
			data.Add(new Data(rigidbody));
		}

		public void GetData (int frame, out Vector3 position, out Quaternion rotation) {
			position = data[frame].Position;
			rotation = data[frame].Rotation;
		}

	}

	[System.Serializable]
	public class Data {


		[SerializeField]
		private Vector3 position;

		public Vector3 Position {
			get { return position; }
		}

		[SerializeField]
		private Quaternion rotation;

		public Quaternion Rotation {
			get { return rotation; }
		}

		public Data (Rigidbody rigidbody) {
			this.position = rigidbody.transform.localPosition;
			this.rotation = rigidbody.transform.localRotation;
		}
	}
}
