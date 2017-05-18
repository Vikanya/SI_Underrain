using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionFeedback : MonoBehaviour {

	public Image image;

	public void SetFillAmount(float t)
	{
		image.fillAmount = t;
	}
}
