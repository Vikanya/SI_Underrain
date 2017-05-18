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
	}

	public void Trigger(){
		state = !state;
		doorActiv.SetState (state);
	}

	public enum DoorColor
	{
		Blue,
		Red,
		Yellow
	}
}
