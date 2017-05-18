using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotation : MonoBehaviour {

	public float rotSpeed = 0.1f;


	void Update () {
		transform.RotateAround (transform.position, transform.up, rotSpeed);
	}
}
