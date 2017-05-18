using UnityEngine;

public class QuadBillboard : MonoBehaviour {


	void OnEnable(){
		transform.rotation = Camera.main.transform.rotation;
	}

	void Update ()
	{
		transform.rotation = Camera.main.transform.rotation;

	}
}
