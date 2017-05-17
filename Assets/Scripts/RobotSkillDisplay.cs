using UnityEngine;

public class RobotSkillDisplay : MonoBehaviour {

	public float tinySpace = .1f;

	GameObject[] quads;

	int activeDisplays;
	float quadWidth;

	void Start(){

		int quadAmount = transform.childCount;
		quads = new GameObject[quadAmount];
		for (int i = 0; i < quadAmount; i++) {
			quads [i] = transform.GetChild (i).gameObject;
			quads [i].SetActive (false);
		}

		quadWidth = quads.Length != 0 ? quads [0].transform.lossyScale.x : 0;

	}


	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			ManageDisplays (1);
		}
		if (Input.GetKeyDown (KeyCode.N)) {
			ManageDisplays (-1);
		}
	}


	public void ManageDisplays(int amountAdded){



		if (amountAdded > 0) {
			if (activeDisplays + amountAdded > quads.Length) {
				return;
			}

			quads [activeDisplays].gameObject.SetActive (true);
			activeDisplays += amountAdded;
			activeDisplays = Mathf.Clamp (activeDisplays, 0, quads.Length);
		} else {
			if (activeDisplays + amountAdded < 0) {
				return;
			}
			activeDisplays += amountAdded;
			quads [activeDisplays].gameObject.SetActive (false);

		}

		float totalWidth = activeDisplays * quadWidth + tinySpace * (activeDisplays - 1);
		totalWidth = Mathf.Clamp (totalWidth, 0, totalWidth);

		for (int i = 0; i < activeDisplays; i++) {
			//quads [i].transform.position = new Vector3 (transform.position.x - (totalWidth * .5f) + i * (quadWidth + tinySpace), transform.position.y, transform.position.z);
			//quads[i].transform.position = new Vector3(transform.position.x - (tinySpace + quadWidth) * .5f +i * quadWidth * .5f + tinySpace * (i-1), transform.position.y, transform.position.z);
			quads[i].transform.position = new Vector3(transform.position.x - totalWidth * .5f + quadWidth * .5f + (i-1) * (quadWidth * .5f + tinySpace * .5f), transform.position.y, transform.position.z);	
		}
	}
}
