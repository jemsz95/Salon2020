using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float Speed;
	public float StoppingDistance;

	private Vector3 target;

	public void Move(Vector3 pos) {
		target = pos;
		target.y = transform.position.y;
	}

	// Use this for initialization
	void Start () {
		//Tell the nav system the player to move
		NavPoint.Player = this;	

		target = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position, target) > StoppingDistance) {
			transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
		}
	}
}
