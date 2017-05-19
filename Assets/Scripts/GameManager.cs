using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    GameObject _endMissionObject;
    EndMission endMission;
    public GameObject endMissionObject {
        get { return _endMissionObject; }
        set {
            _endMissionObject = value;
            endMission = _endMissionObject.GetComponent<EndMission>();
        }
    }

	float cardWidth;
	int cardNumber;


	void Awake(){
		instance = this;
	}

	void Start ()
	{
		InitializeDeck ();
        Time.timeScale = 1;
		/*cardWidth = cards [0].GetComponent<RectTransform> ().rect.width;
		float totalWidth = cardNumber * cardWidth + tinySpace * (cardNumber - 1);
		totalWidth = Mathf.Clamp (totalWidth, 0, totalWidth);
		Vector2 center = cardPanel.rect.center;
		for (int i = 0; i < cardNumber; i++) {
			cards[i].GetComponent<RectTransform>().position = new Vector2(center.x - totalWidth * .5f + cardWidth * .5f + i * cardWidth * .5f + i * tinySpace * .5f+ i * (cardWidth * .5f + tinySpace * .5f), center.y);    

		}*/
	}

	public bool ConsumeCard(string cardName){
		switch (cardName) {
		case "Move":
			if (moveAmount == 0)
				return false;
			moveAmount--;
			cards[0].GetComponentInChildren<TextMeshProUGUI>().text =  moveAmount.ToString();
			break;
		case "Attack":
			if (attackAmount == 0)
				return false;
			attackAmount--;
			cards[1].GetComponentInChildren<TextMeshProUGUI>().text = attackAmount.ToString();
			break;
		case "Interact":
			if (interactAmount == 0)
				return false;
			interactAmount--;
			cards[2].GetComponentInChildren<TextMeshProUGUI>().text =  interactAmount.ToString();
			break;
		case "Hide":
			if (camoAmount == 0)
				return false;
			camoAmount--;
			cards[3].GetComponentInChildren<TextMeshProUGUI>().text =  camoAmount.ToString();
			break;
		case "Distract":
			if (distractAmount == 0)
				return false;
			distractAmount--;
			cards[4].GetComponentInChildren<TextMeshProUGUI>().text = distractAmount.ToString();
			break;
		case "Mine":
			if (mineAmount == 0)
				return false;
			mineAmount--;
			cards[5].GetComponentInChildren<TextMeshProUGUI>().text =  mineAmount.ToString();
			break;
		case "Back":
			if (backAmount == 0)
				return false;
			backAmount--;
			cards[6].GetComponentInChildren<TextMeshProUGUI>().text =  backAmount.ToString();
			break;
		default:
			break;
		}
		return true;
	}

	public bool AddCard(string cardName){
		switch (cardName) {
		case "Move":
			moveAmount++;
			cards[0].GetComponentInChildren<TextMeshProUGUI>().text =  moveAmount.ToString();
			break;
		case "Attack":
			attackAmount++;
			cards[1].GetComponentInChildren<TextMeshProUGUI>().text = attackAmount.ToString();
			break;
		case "Interact":
			interactAmount++;
			cards[2].GetComponentInChildren<TextMeshProUGUI>().text =  interactAmount.ToString();
			break;
		case "Hide":
			camoAmount++;
			cards[3].GetComponentInChildren<TextMeshProUGUI>().text =  camoAmount.ToString();
			break;
		case "Distract":
			distractAmount++;
			cards[4].GetComponentInChildren<TextMeshProUGUI>().text =  distractAmount.ToString();
			break;
		case "Mine":
			mineAmount++;
			cards[5].GetComponentInChildren<TextMeshProUGUI>().text =  mineAmount.ToString();
			break;
		case "Back":
			backAmount++;
			cards[6].GetComponentInChildren<TextMeshProUGUI>().text =  backAmount.ToString();
			break;
		default:
			break;
		}
		return true;
	}


	void Update(){
		if (!actionPhase){
			if (Input.GetButtonDown ("Next") && Time.timeScale>0) {
				actionPhase = true;
				cardPanel.gameObject.SetActive (false);

				robot1.GetComponentInChildren<RobotSkillDisplay> ().HideAllButOne ();
				robot2.GetComponentInChildren<RobotSkillDisplay> ().HideAllButOne ();
				robot3.GetComponentInChildren<RobotSkillDisplay> ().HideAllButOne ();

			}
		} else {
			if (Input.GetKeyDown(KeyCode.Escape)) {
                Pause(!isPaused);
            }
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

    bool isPaused;
    public void Pause(bool value) {
        isPaused = value;
        Time.timeScale = value ? 0f : 1f;
        endMissionObject.SetActive(value);
        endMission.pauseScreen.SetActive(value);
    }
	public void GameOver(){
		Time.timeScale = 0f;
        endMissionObject.SetActive(true);
        endMission.loseScreen.SetActive(true);
	}
	public void Victory(){
		Time.timeScale = 0f;
        endMissionObject.SetActive(true);
        endMission.winScreen.SetActive(true);
    }

	void InitializeDeck(){
		if (moveAmount == 0){
			cards [0].SetActive (false);
		} else {
			cardNumber++;
			cards [0].GetComponentInChildren<TextMeshProUGUI> ().text = moveAmount.ToString();
		}
		if (attackAmount == 0){
			cards [1].SetActive (false);
		} else {
			cardNumber++;
			cards [1].GetComponentInChildren<TextMeshProUGUI> ().text =  attackAmount.ToString();
		}
		if (interactAmount == 0){
			cards [2].SetActive (false);
		} else {
			cardNumber++;
			cards [2].GetComponentInChildren<TextMeshProUGUI> ().text = interactAmount.ToString();
		}
		if (camoAmount == 0){
			cards [3].SetActive (false);
		} else {
			cardNumber++;
			cards [3].GetComponentInChildren<TextMeshProUGUI> ().text =  camoAmount.ToString();
		}
		if (distractAmount == 0){
			cards [4].SetActive (false);
		} else {
			cardNumber++;
			cards [4].GetComponentInChildren<TextMeshProUGUI> ().text =  distractAmount.ToString();
		}
		if (mineAmount == 0){
			cards [5].SetActive (false);
		} else {
			cardNumber++;
			cards [5].GetComponentInChildren<TextMeshProUGUI> ().text =  mineAmount.ToString();
		}
		if (backAmount == 0){
			cards [6].SetActive (false);
		} else {
			cardNumber++;
			cards [6].GetComponentInChildren<TextMeshProUGUI> ().text = backAmount.ToString();
		}
	}

}
