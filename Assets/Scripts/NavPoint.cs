using UnityEngine;
using System.Collections;

public class NavPoint : MonoBehaviour, IGvrGazeResponder {

	public static PlayerController Player;

	public void OnGazeEnter() {
		if(GvrController.ClickButton) {
			Player.Move(transform.position);
		}
	}

	public void OnGazeExit() {

	}

	public void OnGazeTrigger() {

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
