using System.Collections.Generic;
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
	float hiddenTimer;
	bool camoOn;
	float totalMoved;

	GameObject currTarget;
	Renderer rend;

	public LayerMask layerShootable;
	public LayerMask layerEnemy;

	#region temporaire
	public Material camoMat;
	Material baseMat;
	#endregion

	void Start(){
		waypoints.Add (transform.position);
		foreach (Transform child in waypointsObjects) {
			waypoints.Add (child.position);
		}
		waypointParent.SetParent(null, true);
		rend = GetComponent<Renderer> ();
		baseMat = rend.material;
	}

	public void AssignSkill(string skill){
		skillList.Add (skill);
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
		if (!skillList [0].Equals ("Hide") && !skillList [0].Equals ("Distract")) {
			if (!ready || skillList.Count == 0) 
				return;
		}
		if (ExecuteSkill (skillList [0])){
			skillList.RemoveAt (0);
		}
	}
	bool ExecuteSkill(string skill){
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
		case "Hide":
			TriggerHide ();
			break;
		case "Distract":
			TriggerDistract ();
			break;
		default:
			ready = true;
			break;
		}
		return true;
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
		Debug.Log ("distractiong");
	}
}
