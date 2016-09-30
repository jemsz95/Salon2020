using UnityEngine;
using System.Collections;

public class RadialMenu : MonoBehaviour {

	public RadialButton ButtonPrefab;
	public RadialButton Selected;
	public bool UseToggleBehaviour;

	// Use this for initialization
	public void SpawnButtons (RadialMenuOption[] options) {
		for (int i = 0; i < options.Length; i++) {
			RadialButton button = (RadialButton) Instantiate (ButtonPrefab);
			button.transform.SetParent (transform);
			button.transform.localScale = new Vector3(0.2f, 0.2f, 0f);

			float theta = (2 * Mathf.PI / options.Length) * i;
			float x = Mathf.Sin (theta);
			float y = Mathf.Cos (theta);

			Vector3 position = new Vector3 (x, y, 0f) * 3;
			button.transform.localPosition = position;

			button.Icon.sprite = options [i].icon;
			button.Label.text = options [i].title;
			button.Circle.color = options [i].color;
			button.Menu = this;
			button.OnClick = options [i].onClick;
			button.UseToggleBehaviour = UseToggleBehaviour;
		}
	}

	public void Close () {
		if(Selected != null) {
			Selected.OnClick.Invoke();
		}
	
		Destroy (gameObject);
	}
}
