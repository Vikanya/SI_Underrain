using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivation : MonoBehaviour {


	public List<GameObject> Lasers = new List<GameObject> ();

	public void SetState(bool state){
		foreach (GameObject item in Lasers) {
			item.SetActive (state);
		}
	}

}
