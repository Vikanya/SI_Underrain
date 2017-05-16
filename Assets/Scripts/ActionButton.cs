using UnityEngine;
using UnityEngine.EventSystems;

public class ActionButton : MonoBehaviour {
	
	public void OnPointerDown (PointerEventData eventData) 
	{
		//Debug.Log ("mouse down");
		SkillAssignment.instance.SetCurrentSkill (name);
	}
}
