using UnityEngine;
using System.Collections;

public class RadialMenu : MonoBehaviour, IGvrGazeResponder {

	public RadialButton ButtonPrefab;
	public RadialButton Selected;

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
			button.Title = options [i].title;
			button.Circle.color = options [i].color;
			button.Menu = this;
			button.OnClick = options [i].onClick;
		}
	}

	public void OnGazeEnter() {
	}

	public void OnGazeExit() {
	}

	public void OnGazeTrigger() {
	}

	void Update () {
		//if (Input.GetMouseButtonUp (0)) {
		//	if (Selected != null) {
		//	
		//	}
		//
		//	Destroy (gameObject);
		//}
	}
}
