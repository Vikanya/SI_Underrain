using UnityEngine;

public class Terminal : MonoBehaviour {

	public ActivableObject[] activableObjects;


	public void Activate(){
        foreach(ActivableObject activableObject in activableObjects) {
            activableObject.Trigger();
        }
	}

}
