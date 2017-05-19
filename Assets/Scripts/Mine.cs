using UnityEngine;

public class Mine : MonoBehaviour {

	public float range = 2f;
	public LayerMask layerEnemy;

	public GameObject rangeObject;

	Collider[] enemiesInRange;

	public AudioClip explosionSFX;

	void Start(){
		rangeObject.transform.localScale = new Vector3 (range, range, range);
	}

	void Update(){

		enemiesInRange = Physics.OverlapSphere (transform.position, range, layerEnemy);
		
		if (enemiesInRange.Length != 0){
			enemiesInRange [0].GetComponent<EnemyBehaviour> ().Shot ();
			AudioSource.PlayClipAtPoint (explosionSFX, transform.position, 1.5f);
			gameObject.SetActive (false);
		}

	}

}
