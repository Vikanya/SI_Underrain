using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour {

	public GameObject triggeredObject;

	public void Shot(){
		Debug.Log ("I AM DESTRYED " + name);
		triggeredObject.SetActive (false);
	} 
}
