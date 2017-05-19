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

	public GameObject distractionZone;

	Animator anim;
	public Transform turret;
	bool rotationOverride = false;
	Transform lookAtTarget;


	public AudioClip stealthSFX;
	public AudioClip interactSFX;
	public AudioClip attackSFX;
	public AudioClip distractionSFX;

	#region temporaire
	public Material camoMat;
	Material baseMat;
	#endregion

	void Start(){
        GenerateRail();
		rend = GetComponent<Renderer> ();
		baseMat = rend.material;
		distractionZone.transform.localScale = new Vector3 (distractionRange*2, distractionRange*2, distractionRange*2);
		anim = GetComponentInChildren<Animator> ();
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
        Instantiate(waypointsObjects[0], transform.position, transform.rotation);
        LineRenderer rail = waypointParent.GetComponent<LineRenderer>();

        rail.positionCount = waypoints.Count;
        for (int i = 0; i < rail.positionCount; i++) {
            rail.SetPosition(i, waypoints[i]);
        }
        waypointParent.SetParent(null, true);
    }

	void Update(){
		// MOVEMENT
		if (GameManager.instance.actionPhase){
			//print ("slt");
			CheckIfShootable ();
			Distract ();
		}
		Move ();
		if (camoOn)
			Hide ();
	}

	void LateUpdate()
	{
		if(rotationOverride)
		{
			turret.LookAt (new Vector3(lookAtTarget.position.x,turret.position.y,lookAtTarget.position.z));
			turret.RotateAround (turret.right, -90);
		}
	}
	void Move(){
		if (totalMoved - moveDist < 0) {
			anim.SetBool ("Walk", true);
			moveAmount = Time.deltaTime * speed;
			totalMoved += moveAmount;
            print("en train de bouger");
			transform.Translate (moveDir * moveAmount, Space.World);
		} else {
			anim.SetBool ("Walk", false);
			if (waypointIndex>0 && waypointsObjects [waypointIndex-1].localScale.x == 0) {
				
				TriggerMove ();
			} else{
				ready = true;
			}
		}
	}
	void Attack(){
		currTarget = null;
		Collider[] shootablesInRange = Physics.OverlapSphere (transform.position, attackRange, layerShootable);
		float minSqrDist = float.MaxValue;

		for (int i = 0; i < shootablesInRange.Length; i++) {
			if (minSqrDist > (shootablesInRange [i].transform.position - transform.position).sqrMagnitude 
				&& !Physics.Raycast(transform.position, shootablesInRange [i].transform.position - transform.position, (shootablesInRange [i].transform.position - transform.position).magnitude, layerObstacle)){
				minSqrDist = (shootablesInRange [i].transform.position - transform.position).sqrMagnitude;
				currTarget = shootablesInRange [i].gameObject;
			}
		}
		if (currTarget) {
			anim.SetTrigger ("Fire");
			rotationOverride = true;
			lookAtTarget = currTarget.transform;
			try {
				currTarget.GetComponent<EnemyBehaviour> ().Shot ();
			} catch (System.Exception ex) {
				currTarget.GetComponent<Shootable> ().Shot ();
			}
			AudioSource.PlayClipAtPoint (attackSFX, transform.position, 1.5f);
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
		if(skillList.Count > 0 && skillList[0] == "Distract") {
			distractionZone.SetActive (true);
		}
	}
	public void NextSkill(){
		if (skillList.Count == 0){
			//Debug.Log ("list = 0");
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
		rotationOverride = false;
		turret.rotation = Quaternion.identity;
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
        
		transform.LookAt (waypoints [waypointIndex + 1]);
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
			AudioSource.PlayClipAtPoint (interactSFX, transform.position,1.5f);
		}
		anim.SetTrigger ("Interaction");
	}
	void TriggerHide(){
		rend.material = camoMat;
		camoOn = true;
		hiddenTimer = camouflageTime;
		gameObject.layer = LayerMask.NameToLayer ("Invisible");
		AudioSource.PlayClipAtPoint (stealthSFX, transform.position,1.5f);
	}
	void TriggerDistract(){
		Collider[] enemiesInRange = Physics.OverlapSphere (transform.position, distractionRange, layerEnemy);
		for (int a = 0; a < enemiesInRange.Length; a++) {
			enemiesInRange [a].GetComponent<EnemyBehaviour> ().GetDistracted (transform.position);
		}
		distractionZone.SetActive (false);
		AudioSource.PlayClipAtPoint (distractionSFX, transform.position, 1.5f);
		anim.SetTrigger ("Interaction");
	}
	void TriggerMine(){
		Instantiate (prefabMine, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
		anim.SetTrigger ("Interaction");
	}
	void TriggerBack(){
		if (waypointIndex <= 0)
			return;

		transform.LookAt (waypoints [waypointIndex - 1]);
		totalMoved = 0;
		moveDir = (waypoints [waypointIndex - 1] - waypoints [waypointIndex]);
		moveDist = moveDir.magnitude;
		moveDir = moveDir.normalized;
		waypointIndex--;
	}

	public void HideInDarkness(bool hidden){
		
		gameObject.layer = LayerMask.NameToLayer (hidden ? "Invisible" : "Robot");

		if(hidden){
			Debug.Log ("CANT SEE ME");
		} else {
			Debug.Log("OH NOES YOU SEEZ");
		}
	}


	public void CheckIfShootable()
	{
		currTarget = null;
		Collider[] shootablesInRange = Physics.OverlapSphere (transform.position, attackRange, layerShootable);
		float minSqrDist = float.MaxValue;

		for (int i = 0; i < shootablesInRange.Length; i++) {
			if (minSqrDist > (shootablesInRange [i].transform.position - transform.position).sqrMagnitude 
				&& !Physics.Raycast(transform.position, shootablesInRange [i].transform.position - transform.position, (shootablesInRange [i].transform.position - transform.position).magnitude, layerObstacle)){
				minSqrDist = (shootablesInRange [i].transform.position - transform.position).sqrMagnitude;
				currTarget = shootablesInRange [i].gameObject;
			}
		}
		if (currTarget) {
			if(skillList.Count > 0 && skillList[0] == "Attack") 
			{
				if (name.Contains("A"))
				{
					currTarget.GetComponent<ShootTargets> ().ShowTarget (0, true);
				}
				else if (name.Contains("B"))
				{
					currTarget.GetComponent<ShootTargets> ().ShowTarget (1, true);
				}
				else if (name.Contains("C"))
				{
					currTarget.GetComponent<ShootTargets> ().ShowTarget (2, true);
				}
			}
			else
			{
				if (name.Contains("A"))
				{
					currTarget.GetComponent<ShootTargets> ().IndicateTarget (0);
				}
				else if (name.Contains("B"))
				{
					currTarget.GetComponent<ShootTargets> ().IndicateTarget (1);
				}
				else if (name.Contains("C"))
				{
					currTarget.GetComponent<ShootTargets> ().IndicateTarget (2);
				}
			}
		}
	}

	public void Die()
	{
		anim.SetTrigger ("Death");
	}
}
