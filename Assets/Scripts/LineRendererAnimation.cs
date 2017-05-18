using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererAnimation : MonoBehaviour {

	public float speed = -0.02f;
	private Material _mat;

	void Start () {
		_mat = GetComponent<Renderer> ().material;	
	}
	
	// Update is called once per frame
	void Update () {
		_mat.mainTextureOffset += new Vector2(speed,0);
		if (_mat.mainTextureOffset.x > 1 || _mat.mainTextureOffset.x < 0) {
			_mat.mainTextureOffset -= new Vector2(1*Mathf.Sign(_mat.mainTextureOffset.x),0);
		}

	}
}
