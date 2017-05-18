﻿using System.Collections.Generic;
using UnityEngine;

public class RobotPlayer : MonoBehaviour {

	public Transform waypointParent;

	public List<Transform> waypointsObjects = new List<Transform>();
	List<Vector3> waypoints = new List<Vector3>();
	int waypointIndex;

	List<string> skillList = new List<string>();
	bool ready = true;

	Vector3 moveDir;
	float moveDist;
	float moveAmount;
	public float speed = 15f;
	public float attackRange = 7f;
	public float camouflageTime = 2f;
	public float distractionRange = 10f;
	public float interactRange = 2f;
	float hiddenTimer;
	bool camoOn;
	float totalMoved;

	GameObject currTarget;
	Renderer rend;

	public LayerMask layerShootable;
	public LayerMask layerEnemy;
	public LayerMask layerObstacle;
	public LayerMask layerInteractable;

	public GameObject prefabMine;

	#region temporaire
	public Material camoMat;
	Material baseMat;
	#endregion

	void Start(){
        GenerateRail();
		rend = GetComponent<Renderer> ();
		baseMat = rend.material;
	}

	public void AssignSkill(string skill){
		skillList.Add (skill);
	}

	public string RemoveSkill(){
		if (skillList.Count <= 0)
			return "no";
		string skillRemoved = skillList [skillList.Count - 1];
		skillList.RemoveAt (skillList.Count - 1);
		return skillRemoved;
	}

    void GenerateRail() {
        waypoints.Add(transform.position);
        foreach (Transform child in waypointsObjects) {
            waypoints.Add(child.position);
        }

        LineRenderer rail = waypointParent.GetComponent<LineRenderer>();

        rail.positionCount = waypoints.Count;
        for (int i = 0; i < rail.positionCount; i++) {
            rail.SetPosition(i, waypoints[i]);
        }
        waypointParent.SetParent(null, true);
    }

	void Update(){
		// MOVEMENT
		Move ();
		if (camoOn)
			Hide ();
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
	void Hide(){
		hiddenTimer -= Time.deltaTime;
		if (hiddenTimer <= 0) {
			rend.material = baseMat;
			camoOn = false;
			gameObject.layer = LayerMask.NameToLayer ("Robot");
		}
	}
	void Distract(){
	}
	public void NextSkill(){
		if (skillList.Count == 0){
			Debug.Log ("list = 0");
			return;
		}
		if (!ready && !skillList [0].Equals ("Hide") && !skillList [0].Equals ("Distract") && !skillList [0].Equals ("Mine")) { 
				return;
		}
		if (ExecuteSkill (skillList [0])){
			skillList.RemoveAt (0);
			GetComponentInChildren<RobotSkillDisplay> ().DisplayNextSkill (skillList.Count <= 0);

		}
	}
	bool ExecuteSkill(string skill){
		ready = false;
		switch (skill) {
		case "Attack":
			TriggerAttack ();
			break;
		case "Move":
			if (CheckMove ()) {
				TriggerMove ();
			} else {
				return false;
			}
			break;
		case "Interact":
			TriggerInteract ();
			break;
		case "Hide":
			TriggerHide ();
			break;
		case "Distract":
			TriggerDistract ();
			break;
		case "Mine":
			TriggerMine();
			break;
		case "Back":
			if (CheckBack()){
				TriggerBack();
			}else {
				return false;
			}
			break;
		default:
			ready = true;
			break;
		}
		return true;
	}


	bool CheckMove(){
		if (waypointIndex >= waypoints.Count-1)
			return true;
		
		Vector3 checkDir = (waypoints [waypointIndex + 1] - waypoints [waypointIndex]);
		float checkDist = checkDir.magnitude;
		checkDir = checkDir.normalized;
		return !Physics.Raycast (transform.position, checkDir, checkDist, layerObstacle);
	}
	bool CheckBack(){
		if (waypointIndex <= 0)
			return false;
		
		Vector3 checkDir = (waypoints [waypointIndex - 1] - waypoints [waypointIndex]);
		float checkDist = checkDir.magnitude;
		checkDir = checkDir.normalized;
		return !Physics.Raycast (transform.position, checkDir, checkDist, layerObstacle);
	}
	void TriggerAttack(){
		Attack ();
	}

	void TriggerMove(){
		if (waypointIndex >= waypoints.Count - 1)
			return;
		totalMoved = 0;
		moveDir = (waypoints [waypointIndex + 1] - waypoints [waypointIndex]);
		moveDist = moveDir.magnitude;
		moveDir = moveDir.normalized;
		waypointIndex++;
	}
	void TriggerInteract(){
		currTarget = null;
		Collider[] interactablesInRange = Physics.OverlapSphere (transform.position, interactRange, layerInteractable);
		float minSqrDist = float.MaxValue;

		for (int i = 0; i < interactablesInRange.Length; i++) {
			if (minSqrDist > (interactablesInRange [i].transform.position - transform.position).sqrMagnitude){
				minSqrDist = (interactablesInRange [i].transform.position - transform.position).sqrMagnitude;
				currTarget = interactablesInRange [i].gameObject;
			}
		}
		if (currTarget) {
			currTarget.GetComponent<Terminal> ().Activate ();
		}
	}
	void TriggerHide(){
		rend.material = camoMat;
		camoOn = true;
		hiddenTimer = camouflageTime;
		gameObject.layer = LayerMask.NameToLayer ("Enemy");
	}
	void TriggerDistract(){
		Collider[] enemiesInRange = Physics.OverlapSphere (transform.position, attackRange, layerEnemy);
		for (int a = 0; a < enemiesInRange.Length; a++) {
			enemiesInRange [a].GetComponent<EnemyBehaviour> ().GetDistracted (transform.position);
		}
	}
	void TriggerMine(){
		Instantiate (prefabMine, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
	}
	void TriggerBack(){
		if (waypointIndex <= 0)
			return;

		Debug.Log (waypointIndex);
		totalMoved = 0;
		moveDir = (waypoints [waypointIndex - 1] - waypoints [waypointIndex]);
		moveDist = moveDir.magnitude;
		moveDir = moveDir.normalized;
		waypointIndex--;
	}

	public void HideInDarkness(bool hidden){
		
		gameObject.layer = LayerMask.NameToLayer (hidden ? "Enemy" : "Robot");

		if(hidden){
			Debug.Log ("CANT SEE ME");
		} else {
			Debug.Log("OH NOES YOU SEEZ");
		}
	}
}
