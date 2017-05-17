using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	public Transform waypointParent;
	List<Vector3> waypoints = new List<Vector3>();
	List<float> waypointsTimer = new List<float>();
	int waypointIndex;
	public bool looping;

	bool isPlayerDetected;
	RobotPlayer playerDetected;

	float moveAmount;
	public float speed = 15f;
	public float visionLength = 5f;
	[Tooltip("Si égal à 0, ennemi a un cône de vision à 180°, si égal à 1 vision à 0°")]
	public float visionDotProduct = .3f;
	public float detectionDelay = 2f;
	float detectionTimer;
	float totalMoved;
	float timerWait;
	Vector3 moveDir;
	float moveDist;

	Vector3 tmpV3;
	bool gameOver;

	RaycastHit hit;

	RobotPlayer[] robots = new RobotPlayer[3];

	void Start () {
		foreach (Transform child in waypointParent) {
			waypoints.Add (new Vector3(child.position.x, 0, child.position.z));
			waypointsTimer.Add (child.localPosition.y);
		}
		waypointParent.gameObject.SetActive (false);
		robots = FindObjectsOfType<RobotPlayer> ();
	}
	
	void Update () {
		if (!isPlayerDetected) {
			Move ();
			PlayerWatch ();
		} else {
			DetectedPlayer ();
		}
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
	void PlayerWatch(){
		for (int i = 0; i < robots.Length; i++) {
			if (SinglePlayerWatch (robots [i])) {
				Debug.Log ("DETRCTERD");
				isPlayerDetected = true;
				playerDetected = robots [i];
				detectionTimer = detectionDelay;
			}
		}
	}
	bool SinglePlayerWatch(RobotPlayer robot){
		tmpV3 = robot.transform.position - transform.position;
		if (tmpV3.sqrMagnitude < visionLength*visionLength){
			if (Vector3.Dot(transform.forward, tmpV3.normalized) > visionDotProduct){
				if (Physics.Raycast(transform.position, tmpV3, out hit, tmpV3.magnitude)){
					if (hit.transform.gameObject.Equals (robot.gameObject)) {
						return true;
					}
				}
			}
		}
		return false;
	}
	void DetectedPlayer(){
		if (gameOver)
			return;
		detectionTimer -= Time.deltaTime;
		if (detectionTimer <= 0f){
			gameOver = true;
			GameManager.instance.GameOver();
		}
		transform.LookAt (playerDetected.transform);
		if (!SinglePlayerWatch(playerDetected)){
			isPlayerDetected = false;
		}
	}

	void NextPoint(){
		//Debug.Log (waypoints.Count + " " + waypointIndex);
		if (waypoints.Count <= waypointIndex+1){
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
		transform.LookAt (transform.position + moveDir);
		moveDist = moveDir.magnitude;
		moveDir = moveDir.normalized;
		totalMoved = 0;
		waypointIndex++;
	}

	public void Shot(){
		Debug.Log ("I AM DEAD");
	}
}

