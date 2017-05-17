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
		//Debug.Log ("ui pressed : " + skill);
		currentSkill = skill;
	}

	void Awake(){
		instance = this;
	}

	void Update(){
		/*if (Input.GetButtonDown("Fire1")){
			Debug.Log ("Fired");
			//Debug.DrawRay (rayToMousePoint2D.origin, rayToMousePoint2D.direction*500, Color.black, 1f);

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100f)){
				Debug.Log ("on ui " + hit.transform.name);
				currentSkill = hit.transform.tag;


			} else {
				currentSkill = null;
			}
		}*/

		if (Input.GetButtonUp ("Fire1")) {
			if (currentSkill != null){
				//Debug.Log ("fired released");
				rayToMousePoint = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast(rayToMousePoint, out hit, float.MaxValue, layerRobot)){
					//Debug.Log ("on robot " + hit.transform.name);
					hit.transform.GetComponent<RobotPlayer> ().AssignSkill (currentSkill);
					GameManager.instance.ConsumeCard (currentSkill);
				}
				currentSkill = null;
			}
		}
	}
}
