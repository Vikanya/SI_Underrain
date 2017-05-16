using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	public Transform waypointParent;
	List<Vector3> waypoints = new List<Vector3>();
	List<float> waypointsTimer = new List<float>();
	int waypointIndex;
	public bool looping;

	float moveAmount;
	public float speed = 15f;
	float totalMoved;
	float timerWait;
	Vector3 moveDir;
	float moveDist;

	void Start () {
		foreach (Transform child in waypointParent) {
			waypoints.Add (new Vector3(child.position.x, 0, child.position.z));
			waypointsTimer.Add (child.position.y);
		}
		waypointParent.gameObject.SetActive (false);
	}
	
	void Update () {
		Move ();
	}

	void Move(){
		if (totalMoved - moveDist < 0) {

			moveAmount = Time.deltaTime * speed;
			totalMoved += moveAmount;
			transform.Translate (moveDir * moveAmount, Space.World);
		} else {
			timerWait -= Time.deltaTime;
			if (timerWait <= 0){
				NextPoint ();
			}
		}
	}
	void NextPoint(){
		Debug.Log (waypoints.Count + " " + waypointIndex);
		if (waypoints.Count <= waypointIndex+1){
			Debug.Log ("slt");
			if (looping){
				moveDir = (waypoints [0] - waypoints [waypointIndex]);
				timerWait = waypointsTimer [0];
				waypointIndex = -1;
			} else {
				moveDir = (waypoints [waypointIndex -1] - waypoints [waypointIndex]);
				timerWait = waypointsTimer [waypointIndex -1];
				waypoints.Reverse ();
				waypointsTimer.Reverse ();
				waypointIndex = 0;
			}
		} else {
			moveDir = (waypoints [waypointIndex + 1] - waypoints [waypointIndex]);
			timerWait = waypointsTimer [waypointIndex + 1];
			
		}


		moveDist = moveDir.magnitude;
		moveDir = moveDir.normalized;
		totalMoved = 0;
		waypointIndex++;
	}
}

