using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour {

	public GameObject triggeredObject;

	bool objectActive = true;
	public LayerMask robotMask;
	//public float lightRadius = 5f;

	public Transform lightZoneOrigin;


	public void Shot(){
		Debug.Log ("I AM DESTRYED " + name);
		triggeredObject.SetActive (false);
		objectActive = false;
	} 

	void Update(){
		if (!objectActive)
			return;
	
		Collider[] itemsFound = Physics.OverlapSphere (lightZoneOrigin.position, lightZoneOrigin.lossyScale.x);

	}

	void OnDrawGizmos(){
		Gizmos.color = Color.green;

		Gizmos.DrawWireSphere (lightZoneOrigin.position, lightZoneOrigin.lossyScale.x);
	
	}
}
