using UnityEngine;
using UnityEngine.UI;

public class SkillAssignment : MonoBehaviour {

	public static SkillAssignment instance;

	Ray2D rayToMousePoint2D;
	Ray rayToMousePoint;
	RaycastHit hit;
	public LayerMask layerUI;
	public LayerMask layerRobot;

	string currentSkill;


	public void SetCurrentSkill(string skill){
		currentSkill = skill;
	}

	void Awake(){
		instance = this;
	}

	void Update(){

		if (Input.GetButtonUp ("Fire1")) {
			if (currentSkill != null){
				rayToMousePoint = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast(rayToMousePoint, out hit, float.MaxValue, layerRobot)){
					if (GameManager.instance.ConsumeCard (currentSkill)) {
						hit.transform.GetComponentInChildren<RobotSkillDisplay> ().ManageDisplays (1, currentSkill);
						hit.transform.GetComponent<RobotPlayer> ().AssignSkill (currentSkill);

					}
				}
				currentSkill = null;
			}
		}

		if (Input.GetButtonDown ("Fire2")) {
			rayToMousePoint = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (rayToMousePoint, out hit, float.MaxValue, layerRobot)) {
				string skillToRemove = hit.transform.GetComponent<RobotPlayer> ().RemoveSkill ();
				if (!skillToRemove.Equals ("no")) {
					hit.transform.GetComponentInChildren<RobotSkillDisplay> ().ManageDisplays (-1, currentSkill);
					GameManager.instance.AddCard (skillToRemove);
				}
			}
		
		}
	}
}
