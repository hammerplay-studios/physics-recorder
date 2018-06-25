using UnityEngine;
using UnityEditor;

namespace Hammerplay.Utils.PhysicsRecorder {

	[CustomEditor(typeof(PhysicsRecorder))]
	public class PhysicsRecorderEditor : Editor {

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			PhysicsRecorder physicsRecorder = (PhysicsRecorder)target;

			EditorGUILayout.LabelField("Frames: ", physicsRecorder.Frames.ToString());

			if (GUILayout.Button ("Record")) {
				physicsRecorder.StartRecording();
			}

			if (GUILayout.Button("Stop")) {
				physicsRecorder.StartRecording();
			}

			EditorUtility.SetDirty(target);
		}

	}
}
