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
		if (Input.GetButton ("Fire1")) {
			currentSkill = skill;
			if (Input.GetButton ("Robot1")) {
				if (GameManager.instance.ConsumeCard (currentSkill)) {
					GameManager.instance.robot1.GetComponentInChildren<RobotSkillDisplay> ().ManageDisplays (1, currentSkill);
					GameManager.instance.robot1.GetComponent<RobotPlayer> ().AssignSkill (currentSkill);
				}
			} else if (Input.GetButton ("Robot2")) {
				if (GameManager.instance.ConsumeCard (currentSkill)) {
					GameManager.instance.robot2.GetComponentInChildren<RobotSkillDisplay> ().ManageDisplays (1, currentSkill);
					GameManager.instance.robot2.GetComponent<RobotPlayer> ().AssignSkill (currentSkill);
				}
			} else if (Input.GetButton ("Robot3")) {
				if (GameManager.instance.ConsumeCard (currentSkill)) {
					GameManager.instance.robot3.GetComponentInChildren<RobotSkillDisplay> ().ManageDisplays (1, currentSkill);
					GameManager.instance.robot3.GetComponent<RobotPlayer> ().AssignSkill (currentSkill);
				}
			}
		} else if (Input.GetButton("Fire2")){
			if (Input.GetButton ("Robot1")) {
				string skillToRemove = GameManager.instance.robot1.RemoveSkill ();
				if (!skillToRemove.Equals ("no")) {
					GameManager.instance.robot1.GetComponentInChildren<RobotSkillDisplay> ().ManageDisplays (-1, currentSkill);
					GameManager.instance.AddCard (skillToRemove);
				}
			} else if (Input.GetButton ("Robot2")) {
				string skillToRemove = GameManager.instance.robot2.RemoveSkill ();
				if (!skillToRemove.Equals ("no")) {
					GameManager.instance.robot2.GetComponentInChildren<RobotSkillDisplay> ().ManageDisplays (-1, currentSkill);
					GameManager.instance.AddCard (skillToRemove);
				}
			} else if (Input.GetButton ("Robot3")) {
				string skillToRemove = GameManager.instance.robot3.RemoveSkill ();
				if (!skillToRemove.Equals ("no")) {
					GameManager.instance.robot3.GetComponentInChildren<RobotSkillDisplay> ().ManageDisplays (-1, currentSkill);
					GameManager.instance.AddCard (skillToRemove);
				}
			}
		}
	}
	public void SetCursorTexture(Texture2D tex){
		Cursor.SetCursor(tex,new Vector2(tex.width/2,tex.height/2),CursorMode.Auto);
	}
	void Awake(){
		instance = this;
	}

	void Update(){

		if (Input.GetButtonUp ("Fire1")) {
			if (currentSkill != null){
				Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);
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
		if (Input.GetButtonUp ("Fire2")) {
			Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);
			currentSkill = null;
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
