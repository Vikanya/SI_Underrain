using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableObject : MonoBehaviour {


	public DoorColor doorColor;
	public bool state;

	public GameObject blueDoor;
	public GameObject redDoor;
	public GameObject yellowDoor;

	DoorActivation doorActiv;

	Collider coll;

	void Start(){
		switch (doorColor) {
		case DoorColor.Blue:
			blueDoor.SetActive (true);
			doorActiv = blueDoor.GetComponent<DoorActivation> ();
			break;
		case DoorColor.Red:
			redDoor.SetActive(true);
			doorActiv = redDoor.GetComponent<DoorActivation> ();
			break;
		case DoorColor.Yellow:
			yellowDoor.SetActive(true);
			doorActiv = yellowDoor.GetComponent<DoorActivation> ();
			break;
		default:
			blueDoor.SetActive(true);
			doorActiv = blueDoor.GetComponent<DoorActivation> ();
			break;
		}
		doorActiv.SetState (state);
		coll = GetComponent<Collider> ();
		coll.enabled = state;
	}

	public void Trigger(){
		state = !state;
		coll.enabled = state;
		doorActiv.SetState (state);
	}

	void OnValidate(){

		blueDoor.SetActive (false);
		redDoor.SetActive(false);
		yellowDoor.SetActive(false);
		switch (doorColor) {
		case DoorColor.Blue:
			blueDoor.SetActive (true);
			break;
		case DoorColor.Red:
			redDoor.SetActive(true);
			break;
		case DoorColor.Yellow:
			yellowDoor.SetActive(true);
			break;
		default:
			blueDoor.SetActive(true);
			break;
		}
	}

	public enum DoorColor
	{
		Blue,
		Red,
		Yellow
	}
}
