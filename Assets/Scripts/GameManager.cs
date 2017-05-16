using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public RobotPlayer robot1;
	public RobotPlayer robot2;
	public RobotPlayer robot3;

	void Awake(){
		instance = this;
	}

	void Update(){
		if (Input.GetButtonDown("Robot1")){
			robot1.NextSkill();
		}
		if (Input.GetButtonDown("Robot2")){
			robot2.NextSkill();
		}
		if (Input.GetButtonDown("Robot3")){
			robot3.NextSkill();
		}


	}

}
