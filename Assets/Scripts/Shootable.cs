using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour {

	public GameObject triggeredObject;

	bool objectActive = true;
	public LayerMask robotMask;
	//public float lightRadius = 5f;

	public Transform lightZoneOrigin;

	public AudioClip lightsOffSFX;

	List<RobotPlayer> currentRobots = new List<RobotPlayer>();
	List<RobotPlayer> previousRobots = new List<RobotPlayer>();

	public void Shot(){
		Debug.Log ("I AM DESTRYED " + name);
		AudioSource.PlayClipAtPoint (lightsOffSFX, transform.position, 1.5f);
		triggeredObject.SetActive (false);
		objectActive = false;
	} 

	void Update(){
		if (objectActive)
			return;


		Collider[] itemsFound = Physics.OverlapSphere (lightZoneOrigin.position, lightZoneOrigin.lossyScale.x, robotMask);
		currentRobots.Clear ();

		if (itemsFound.Length == 0 && previousRobots.Count == 0)
			return;

		for (int i = 0; i < itemsFound.Length; i++) {
			RobotPlayer myRobot = itemsFound[i].GetComponent<RobotPlayer>();
			myRobot.HideInDarkness (true);
			currentRobots.Add (myRobot);
			previousRobots.Remove (myRobot);
		}

		for (int i = 0; i < previousRobots.Count; i++) {
			previousRobots [i].GetComponent<RobotPlayer> ().HideInDarkness (false);
		}

		previousRobots.Clear ();

		for (int i = 0; i < currentRobots.Count; i++) {
			previousRobots.Add (currentRobots [i]);
		}

	}

	void OnDrawGizmos(){
		Gizmos.color = Color.blue;

		Gizmos.DrawWireSphere (lightZoneOrigin.position, lightZoneOrigin.lossyScale.x);
	
	}
}
