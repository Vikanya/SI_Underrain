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
	float visionLength = 5f;
	float visionDotProduct = .3f;
	public float detectionDelay = 2f;
	public bool staticEnemy;
	float detectionTimer;
	float totalMoved;
	float timerWait;
	Vector3 moveDir;
	float moveDist;

	bool distracted;
	public float distractionTime = 3f;
	float distractionTimer;
	Quaternion beforeDistractionRotation;

	Vector3 tmpV3;
	bool gameOver;
	public bool target;

	public LayerMask layerVision;
	RaycastHit hit;

	RobotPlayer[] robots = new RobotPlayer[3];

	private FieldOfView fov;
	public DetectionFeedback detectionFeedback;

	//ANim
	Animator anim;

	void Start () {
		if (!staticEnemy) {
			foreach (Transform child in waypointParent) {
				waypoints.Add (new Vector3 (child.position.x, 0, child.position.z));
				waypointsTimer.Add (child.localPosition.y);
			}
			waypointParent.gameObject.SetActive (false);
		}
		robots = FindObjectsOfType<RobotPlayer> ();

		fov = GetComponent<FieldOfView> ();
		visionLength = fov.viewRadius;
		visionDotProduct = Mathf.Cos (fov.viewAngle * .5f * Mathf.Deg2Rad );

		anim = GetComponentInChildren<Animator> ();
	}
	
	void Update () {
		if (!isPlayerDetected) {
			if (!distracted) {
				Move ();
			} else {
				BeDistracted ();
			}
			PlayerWatch ();
		} else {
			DetectedPlayer ();
		}
	}

	void Move(){

		if (staticEnemy)
		{
			anim.SetFloat ("speed", 0);
			return;
		}

		if (totalMoved - moveDist < 0) {
			anim.SetFloat ("speed",1);
			moveAmount = Time.deltaTime * speed;
			totalMoved += moveAmount;
			transform.Translate (moveDir * moveAmount, Space.World);
		} else {
			timerWait -= Time.deltaTime;
			anim.SetFloat ("speed", 0);
			if (timerWait <= 0){
				NextPoint ();
			}
		}
	}
	void PlayerWatch(){
		if (GameManager.instance.actionPhase) {
			for (int i = 0; i < robots.Length; i++) {
				if (SinglePlayerWatch (robots [i])) {
					//Debug.Log ("DETRCTERD");
					isPlayerDetected = true;
					detectionFeedback.gameObject.SetActive (true);
					playerDetected = robots [i];
					detectionTimer = detectionDelay;

					anim.SetTrigger ("aims");

				}
			}
		}
	}
	bool SinglePlayerWatch(RobotPlayer robot){
		tmpV3 = robot.transform.position - transform.position;
		if (tmpV3.sqrMagnitude < visionLength*visionLength){
			if (Vector3.Dot(transform.forward, tmpV3.normalized) > visionDotProduct){
				if (Physics.Raycast(transform.position, tmpV3, out hit, tmpV3.magnitude, layerVision)){
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
			anim.SetBool ("firing", true);
			gameOver = true;
			playerDetected.Die ();
			Invoke ("DelayedGameOver", 2);

		}
		transform.LookAt (playerDetected.transform);
		if (!SinglePlayerWatch(playerDetected)){
			isPlayerDetected = false;
			detectionFeedback.gameObject.SetActive (false);

			anim.SetTrigger ("drop");
		}

		detectionFeedback.SetFillAmount (1 - (detectionTimer / detectionDelay));

	}
	void BeDistracted(){
		distractionTimer -= Time.deltaTime;
		if (distractionTimer <= 0){
			distracted = false;
			anim.SetTrigger ("drop");
			transform.rotation = beforeDistractionRotation;
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

	public void GetDistracted(Vector3 distractionOrigin){
		distracted = true;
		beforeDistractionRotation = transform.rotation;
		transform.LookAt (distractionOrigin);
		anim.SetTrigger ("aims");
		distractionTimer = distractionTime;
		Debug.Log ("i ma distractend");
	}

	public void Shot(){
		//Debug.Log ("I AM DEAD");

		anim.SetTrigger ("dies");
		gameObject.layer = LayerMask.NameToLayer ("Default");
		detectionFeedback.gameObject.SetActive (false);
		fov.enabled = false;
		fov.viewMeshFilter.gameObject.SetActive(false);
		this.enabled = false;
		if (target)
			Invoke ("DelayedVictory", 2);
		//gameObject.SetActive (false);
	}


	public void DelayedVictory()
	{
		GameManager.instance.Victory ();
	}

	public void DelayedGameOver()
	{
		GameManager.instance.GameOver ();
	}
}

