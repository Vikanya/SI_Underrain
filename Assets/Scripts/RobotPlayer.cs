using System.Collections.Generic;
using UnityEngine;

public class RobotPlayer : MonoBehaviour {

	public Transform waypointParent;

	List<Vector3> waypoints = new List<Vector3>();
	int waypointIndex;

	List<string> skillList = new List<string>();
	bool ready = true;

	Vector3 moveDir;
	float moveDist;
	float moveAmount;
	public float speed = 15f;
	float totalMoved;
	void Start(){
		waypoints.Add (transform.position);
		foreach (Transform child in waypointParent) {
			waypoints.Add (child.position);
		}
		waypointParent.gameObject.SetActive (false);
	}

	public void AssignSkill(string skill){
		skillList.Add (skill);
	}


	void Update(){
		// MOVEMENT
		Move ();

	}
	void Move(){
	//	if (waypointIndex != 0 && waypointIndex < waypoints.Count) {
			//transform.position = Vector3.Lerp (waypoints [waypointIndex - 1], waypoints [waypointIndex], 1 / (moveDist*1000f));

			if (totalMoved - moveDist < 0) {

				moveAmount = Time.deltaTime * speed;
				totalMoved += moveAmount;
				transform.Translate (moveDir * moveAmount, Space.World);
			} else {
				ready = true;

				
			}
	
		//}


	}

	public void NextSkill(){
		if (!ready || skillList.Count == 0)
			return;
		ExecuteSkill (skillList [0]);
		skillList.RemoveAt (0);

	}
	void ExecuteSkill(string skill){
		ready = false;
		switch (skill) {
		case "Attack":
			TriggerAttack ();
			break;
		case "Move":
			TriggerMove ();
			break;
		case "Interact":
			TriggerInteract ();
			break;
		default:
			ready = true;
			break;
		}
	}

	void TriggerAttack(){

	}

	void TriggerMove(){
		if (waypoints.Count <= waypointIndex-1)
			return;

		totalMoved = 0;
		moveDir = (waypoints [waypointIndex + 1] - waypoints [waypointIndex]);
		moveDist = moveDir.magnitude;
		moveDir = moveDir.normalized;
		waypointIndex++;
	}
	void TriggerInteract(){

	}
}
