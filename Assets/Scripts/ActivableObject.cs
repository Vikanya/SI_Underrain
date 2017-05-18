using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableObject : MonoBehaviour {


	public DoorColor doorColor;
	public bool state;

	public GameObject blueDoorPrefab;
	public GameObject redDoorPrefab;
	public GameObject yellowDoorPrefab;

	GameObject tmpObj;
	DoorActivation doorActiv;

	void Start(){
		switch (doorColor) {
		case DoorColor.Blue:
			tmpObj = Instantiate (blueDoorPrefab, transform.position, transform.rotation, transform);
			break;
		case DoorColor.Red:
			tmpObj = Instantiate (redDoorPrefab, transform.position, transform.rotation, transform);
			break;
		case DoorColor.Yellow:
			tmpObj = Instantiate (yellowDoorPrefab, transform.position, transform.rotation, transform);
			break;
		default:
			tmpObj = Instantiate (blueDoorPrefab, transform.position, transform.rotation, transform);
			break;
		}
		doorActiv = tmpObj.GetComponent<DoorActivation> ();
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
