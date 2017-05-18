using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTargets : MonoBehaviour {

	public GameObject[] targets;
	private Color[] baseColors = new Color[3];

	void Awake()
	{
		for(int i = 0; i < baseColors.Length;i++)
		{
			baseColors [i] = targets [i].GetComponent<SpriteRenderer> ().color;
		}	
	}

	public void ShowTarget(int i, bool b)
	{
		targets [i].GetComponent<SpriteRenderer> ().color = baseColors [i];
		targets [i].SetActive (b);
	}

	public void IndicateTarget(int i)
	{
		targets [i].SetActive (true);
		targets [i].GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.4f);
	}

	void FixedUpdate()
	{
		for(int i = 0; i<targets.Length;i++)
		{
			targets [i].SetActive (false);
		}
	}
}
