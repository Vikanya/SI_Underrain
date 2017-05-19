using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	public List<GameObject> tutos = new List<GameObject>();
	public List<GameObject> tutotexts = new List<GameObject>();
	[Space]
	public GameObject bgCanvas;

	private int tutoIndex;
	private bool tutoOn;

	void Start(){
		GameManager.instance.cardPanel.gameObject.SetActive (false);
		Time.timeScale = 0;
		tutoIndex = -1;
		tutoOn = true;
	}

	void Update(){
		if(tutoOn)
			Time.timeScale = 0;
		
		if (Input.GetButtonDown("Fire1")){
			NextPanel ();
		}
		if (Input.GetButtonDown("Fire2")){
			PreviousPanel ();
		}
	}

	void NextPanel(){
		if(tutoOn && tutoIndex<tutos.Count)
		{
			if(tutoIndex>=0)
			{
				tutos [tutoIndex].SetActive (false);
				tutotexts [tutoIndex].SetActive (false);
			}


			tutoIndex++;

			if(tutoIndex<tutos.Count)
			{
				tutos [tutoIndex].SetActive (true);
				tutotexts [tutoIndex].SetActive (true);
			}
			else
			{
				EndTuto ();
			}
		}

	}

	void PreviousPanel(){
		if (tutoOn && tutoIndex > 0) {
			tutos [tutoIndex].SetActive (false);
			tutotexts [tutoIndex].SetActive (false);

			tutoIndex--;

			tutos [tutoIndex].SetActive (true);
			tutotexts [tutoIndex].SetActive (true);
		}
	}

	void EndTuto()
	{
		tutoOn = false;
		tutos [tutos.Count - 1].SetActive (true);
		bgCanvas.SetActive (false);
		Time.timeScale = 1;

	}
}
