using UnityEngine;
using System.Collections;

public class RotateTowardsPlayer : MonoBehaviour {

    public Transform tPlayer;

	// Update is called once per frame
	void Update () {
        transform.LookAt(tPlayer);

        Vector3 vRotation = transform.localRotation.eulerAngles;
        vRotation.x = 0.0f;
        vRotation.z = 0.0f;
        vRotation.y += 180.0f;

        transform.localRotation = Quaternion.Euler(vRotation);

    }
}
