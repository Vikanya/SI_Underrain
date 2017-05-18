using UnityEngine;

public class Mine : MonoBehaviour {

	public float range = 2f;
	public LayerMask layerEnemy;

	Collider[] enemiesInRange;

	void Update(){

		enemiesInRange = Physics.OverlapSphere (transform.position, range, layerEnemy);
		
		if (enemiesInRange.Length != 0){
			enemiesInRange [0].GetComponent<EnemyBehaviour> ().Shot ();
			gameObject.SetActive (false);
		}

	}

}
