using UnityEngine;
using System.Collections;

public class RadialMenuSpawner : MonoBehaviour {

	public RadialMenu MenuPrefab;

	private RadialMenu menu;

	public void SpawnMenu (RadialMenuOption[] options, bool useToggleBehaviour) {
		menu = (RadialMenu) Instantiate (MenuPrefab);
		menu.transform.SetParent (transform);
		menu.transform.position = transform.position;
		menu.transform.localScale = Vector3.one;
		menu.UseToggleBehaviour = useToggleBehaviour;

		menu.SpawnButtons (options);
	}

	public void CloseMenu () {
		menu.Close();
	}
}
