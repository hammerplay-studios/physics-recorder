using UnityEngine;
using UnityEditor;

namespace Hammerplay.Utils.PhysicsRecorder {

	[CustomEditor(typeof(PhysicsPlayback))]
	public class PhysicsPlaybackEditor : Editor {

		public override void OnInspectorGUI() {
			DrawDefaultInspector();

			PhysicsPlayback physicsPlayback = (PhysicsPlayback)target;

			EditorGUILayout.LabelField("Frames: ", physicsPlayback.Frames.ToString());

			if (GUILayout.Button ("Play")) {
				physicsPlayback.Play();
			}

			if (GUILayout.Button ("Stop")) {
				physicsPlayback.Stop();
			}

			EditorUtility.SetDirty(target);
		}
	}
}
