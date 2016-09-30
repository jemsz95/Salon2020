using UnityEngine;
using UnityEngine.Events;
using System.Collections;
	
[System.Serializable]
public struct RadialMenuOption {
	public Sprite icon;
	public Color color;
	public string title;
	public UnityEvent onClick;
}

public class MenuOpener : MonoBehaviour, IGvrGazeResponder {

	public RadialMenuOption[] options;

	public void OnGazeEnter() {
	}

	public void OnGazeExit() {
	}

	public void OnGazeTrigger() {
		RadialMenuSpawner.Instance.SpawnMenu (options);
	}
}
