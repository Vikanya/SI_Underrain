using UnityEngine;
using System.Collections.Generic;

public class RobotSkillDisplay : MonoBehaviour {

	public float tinySpace = .1f;

	GameObject[] quads;

	int activeDisplays;
	float quadWidth;

	[Header("All teh sprites")]
	public Sprite moveSprite;
	public Sprite attackSprite;
	public Sprite interactSprite;
	public Sprite camoSprite;
	public Sprite distractSprite;
	public Sprite mineSprite;
	public Sprite backSprite;


	Dictionary<string, Sprite> skillSprite = new Dictionary<string, Sprite>();

	void Start(){

		int quadAmount = transform.childCount;
		quads = new GameObject[quadAmount];
		for (int i = 0; i < quadAmount; i++) {
			quads [i] = transform.GetChild (i).gameObject;
			quads [i].SetActive (false);
		}

		quadWidth = quads.Length != 0 ? quads [0].transform.lossyScale.x : 0;

		skillSprite.Add ("Move", moveSprite);
		skillSprite.Add ("Attack", attackSprite);
		skillSprite.Add ("Interact", interactSprite);
		skillSprite.Add ("Hide", camoSprite);
		skillSprite.Add ("Distract", distractSprite);
		skillSprite.Add ("Mine", mineSprite);
		skillSprite.Add ("Back", backSprite);
	}

	/*
	void Update(){
		
		if (Input.GetKeyDown (KeyCode.Space)) {
			ManageDisplays (1, "Attack");
		}
		if (Input.GetKeyDown (KeyCode.N)) {
			ManageDisplays (-1, "Move");
		}
		
	}
	*/


	public void ManageDisplays(int amountAdded, string skillAdded){

		if (amountAdded > 0) {
			if (activeDisplays + amountAdded > quads.Length) {
				return;
			}

			quads [activeDisplays].gameObject.SetActive (true);
			quads [activeDisplays].GetComponent<SpriteRenderer> ().sprite = skillSprite [skillAdded];
			activeDisplays += amountAdded;
			activeDisplays = Mathf.Clamp (activeDisplays, 0, quads.Length);

		} else {
			if (activeDisplays + amountAdded < 0) {
				return;
			}
			activeDisplays += amountAdded;
			quads [activeDisplays].gameObject.SetActive (false);
			//quads [activeDisplays].GetComponent<SpriteRenderer> ().sprite = null;

		}

		float totalWidth = activeDisplays * quadWidth + tinySpace * (activeDisplays - 1);
		totalWidth = Mathf.Clamp (totalWidth, 0, totalWidth);

		for (int i = 0; i < activeDisplays; i++) {
			quads [i].transform.position = new Vector3 (transform.position.x - totalWidth * .5f + quadWidth * .5f + i * (quadWidth + tinySpace), transform.position.y + 3.5f, transform.position.z);
		}
	}
}
