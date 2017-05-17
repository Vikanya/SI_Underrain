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
	public float attackRange = 10f;
	float totalMoved;

	GameObject currTarget;

	public LayerMask layerShootable;

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
			if (totalMoved - moveDist < 0) {

				moveAmount = Time.deltaTime * speed;
				totalMoved += moveAmount;
				transform.Translate (moveDir * moveAmount, Space.World);
			} else {
				ready = true;
			}
	}
	void Attack(){
		currTarget = null;
		Collider[] shootablesInRange = Physics.OverlapSphere (transform.position, attackRange, layerShootable);
		float minSqrDist = float.MaxValue;

		for (int i = 0; i < shootablesInRange.Length; i++) {
			if (minSqrDist > (shootablesInRange [i].transform.position - transform.position).sqrMagnitude){
				minSqrDist = (shootablesInRange [i].transform.position - transform.position).sqrMagnitude;
				currTarget = shootablesInRange [i].gameObject;
			}
		}
		if (currTarget) {
			try {
				currTarget.GetComponent<EnemyBehaviour> ().Shot ();
			} catch (System.Exception ex) {
				currTarget.GetComponent<Shootable> ().Shot ();
			}
		}

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
		Attack ();
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
