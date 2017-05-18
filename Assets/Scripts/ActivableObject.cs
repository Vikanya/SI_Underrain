using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableObject : MonoBehaviour {

	public Vector3 destination;
	Vector3 origin;

	void Start(){
		origin = transform.position;
	}

	public void GetActivated(){
		if (transform.position == origin){
			transform.position = destination;
		} else {
			transform.position = origin;
		}
	}
}
