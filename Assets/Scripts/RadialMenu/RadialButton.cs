﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class RadialButton : MonoBehaviour, IGvrGazeResponder {

	public Image Circle;
	public Image Icon;
	public RadialMenu Menu;
	public Text Label;
	public UnityEvent<GameObject> OnClick;
	public bool UseToggleBehaviour;

	private Color defaultColor;

	public void OnGazeEnter() {
		defaultColor = Circle.color;

		Circle.color = Color.white;
	}

	public void OnGazeExit() {
		if(Circle != null) {
			Circle.color = defaultColor;
		}
	}

	public void OnGazeTrigger() {
		Menu.Selected = this;
		
		if(!UseToggleBehaviour) {
			Menu.Close();
		}
	}

	void Start () {
	}
}
