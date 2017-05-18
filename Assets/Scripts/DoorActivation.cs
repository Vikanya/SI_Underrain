using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivation : MonoBehaviour {


	public GameObject Lasers;

	public void SetState(bool state){
		Lasers.SetActive (state);
	}

}
