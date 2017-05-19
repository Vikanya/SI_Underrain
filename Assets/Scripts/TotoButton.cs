using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotoButton : MonoBehaviour {

	public Animator anim;
	public List<GameObject> accessories = new List<GameObject>();
	private int index;



	public void ChangeAccessory()
	{
		Debug.Log ("coolbutton");
		anim.SetTrigger ("Interaction");

		accessories [index].SetActive (false);
		index++;
		if (index > accessories.Count-1) {
			index = 0;
		}
		accessories [index].SetActive (true);
	}
}
