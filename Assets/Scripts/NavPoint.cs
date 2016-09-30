using UnityEngine;
using System.Collections;

public class NavPoint : MonoBehaviour, IGvrGazeResponder {

	public static PlayerController Player;

	public void OnGazeEnter() {
	}

	public void OnGazeExit() {
	}

	public void OnGazeTrigger() {
		Player.Move(transform.position);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
