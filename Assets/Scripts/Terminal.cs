using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour {

	public ActivableObject activableObject;


	public void Activate(){
		activableObject.GetActivated();
	}

}
