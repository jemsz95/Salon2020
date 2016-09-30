using UnityEngine;
using System.Collections;

public class RadialMenuSpawner : MonoBehaviour {

	public static RadialMenuSpawner Instance = null;
	public RadialMenu MenuPrefab;

	public void SpawnMenu (RadialMenuOption[] options) {
		RadialMenu menu = (RadialMenu) Instantiate (MenuPrefab);
		menu.transform.SetParent (transform);
		menu.transform.position = transform.position;
		menu.transform.localScale = Vector3.one;

		menu.SpawnButtons (options);
	}

	void Awake () {
		if (Instance == null) {
			Instance = this;
		}
	}
}
