using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;


	[Header("Deck")]
	public int moveAmount;
	public int attackAmount;
	public int interactAmount;
	public int camoAmount;
	public int distractAmount;
	public int mineAmount;
	public int backAmount;

	[Header("Parameters")]
	public float tinySpace;

	[Header("Put once and dont touch")]
	public RobotPlayer robot1;
	public RobotPlayer robot2;
	public RobotPlayer robot3;

	public RectTransform cardPanel;
	public GameObject[] cards;

	public bool actionPhase;

	float cardWidth;
	int cardNumber;

	void Awake(){
		instance = this;
	}

	void Start ()
	{
		InitializeDeck ();
		/*cardWidth = cards [0].GetComponent<RectTransform> ().rect.width;
		float totalWidth = cardNumber * cardWidth + tinySpace * (cardNumber - 1);
		totalWidth = Mathf.Clamp (totalWidth, 0, totalWidth);
		Vector2 center = cardPanel.rect.center;
		for (int i = 0; i < cardNumber; i++) {
			cards[i].GetComponent<RectTransform>().position = new Vector2(center.x - totalWidth * .5f + cardWidth * .5f + i * cardWidth * .5f + i * tinySpace * .5f+ i * (cardWidth * .5f + tinySpace * .5f), center.y);    

		}*/
	}

	public void ConsumeCard(string cardName){
		switch (cardName) {
		case "Move":
			moveAmount--;
			cards[0].GetComponentInChildren<Text>().text = "x" + moveAmount;
			break;
		case "Attack":
			attackAmount--;
			cards[1].GetComponentInChildren<Text>().text = "x" + attackAmount;
			break;
		case "Interact":
			interactAmount--;
			cards[2].GetComponentInChildren<Text>().text = "x" + interactAmount;
			break;
		case "Hide":
			camoAmount--;
			cards[3].GetComponentInChildren<Text>().text = "x" + camoAmount;
			break;
		case "Distract":
			distractAmount--;
			cards[4].GetComponentInChildren<Text>().text = "x" + distractAmount;
			break;
		case "Mine":
			mineAmount--;
			cards[5].GetComponentInChildren<Text>().text = "x" + mineAmount;
			break;
		case "Back":
			backAmount--;
			cards[6].GetComponentInChildren<Text>().text = "x" + backAmount;
			break;
		default:
			break;
		}
	}

	void Update(){
		if (!actionPhase){
			if (Input.GetButtonDown ("Next")) {
				actionPhase = true;
				cardPanel.gameObject.SetActive (false);
			}
		} else {
			if (Input.GetButtonDown ("Robot1")) {
				robot1.NextSkill ();
			}
			if (Input.GetButtonDown ("Robot2")) {
				robot2.NextSkill ();
			}
			if (Input.GetButtonDown ("Robot3")) {
				robot3.NextSkill ();
			}
		}
	}

	public void GameOver(){
		Debug.Log ("GAMEOVER");
		Time.timeScale = 0f;
	}



	void InitializeDeck(){
		if (moveAmount == 0){
			cards [0].SetActive (false);
		} else {
			cardNumber++;
			cards [0].GetComponentInChildren<Text> ().text = "x" + moveAmount;
		}
		if (attackAmount == 0){
			cards [1].SetActive (false);
		} else {
			cardNumber++;
			cards [1].GetComponentInChildren<Text> ().text = "x" + attackAmount;
		}
		if (interactAmount == 0){
			cards [2].SetActive (false);
		} else {
			cardNumber++;
			cards [2].GetComponentInChildren<Text> ().text = "x" + interactAmount;
		}
		if (camoAmount == 0){
			cards [3].SetActive (false);
		} else {
			cardNumber++;
			cards [3].GetComponentInChildren<Text> ().text = "x" + camoAmount;
		}
		if (distractAmount == 0){
			cards [4].SetActive (false);
		} else {
			cardNumber++;
			cards [4].GetComponentInChildren<Text> ().text = "x" + distractAmount;
		}
		if (mineAmount == 0){
			cards [5].SetActive (false);
		} else {
			cardNumber++;
			cards [5].GetComponentInChildren<Text> ().text = "x" + mineAmount;
		}
		if (backAmount == 0){
			cards [6].SetActive (false);
		} else {
			cardNumber++;
			cards [6].GetComponentInChildren<Text> ().text = "x" + backAmount;
		}
	}

}
