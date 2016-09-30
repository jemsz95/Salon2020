using UnityEngine;
using UnityEngine.Events;
using System.Collections;
	
[System.Serializable]
public struct RadialMenuOption {
	public Sprite icon;
	public Color color;
	public string title;
	public MenuClickEvent onClick;
}

public class MenuOpener : MonoBehaviour, IGvrGazeResponder {

	public RadialMenuSpawner Spawner;
	public RadialMenuOption[] Options;
	public bool UseToggleBehaviour;

	private bool shown = false;

	public void OnGazeEnter() {
	}

	public void OnGazeExit() {
	}

	public void OnGazeTrigger() {
		if(!shown) {
			Spawner.SpawnMenu(Options, UseToggleBehaviour, gameObject);
		} else {
			Spawner.CloseMenu();
		}
		
		if(UseToggleBehaviour) {
			shown = !shown;
		}	
	}
}
