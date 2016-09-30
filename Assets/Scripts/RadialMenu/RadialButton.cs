using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class RadialButton : MonoBehaviour, IGvrGazeResponder {

	public Image Circle;
	public Image Icon;
	public string Title;
	public RadialMenu Menu;
	public Text Label;
	public UnityEvent OnClick;

	private Color defaultColor;

	public void OnGazeEnter() {
	}

	public void OnGazeExit() {
	}

	public void OnGazeTrigger() {
	}

	public void OnPointerEnter (PointerEventData eventData) {
		defaultColor = Circle.color;

		Circle.color = Color.white;

		Menu.Selected = this;
	}

	public void OnPointerExit (PointerEventData eventData) {
		Circle.color = defaultColor;

		Menu.Selected = null;
	}

	void Start () {
	}
}
