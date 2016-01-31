using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextShowner : MonoBehaviour {
	[SerializeField]
	Text text;
	[SerializeField]
	Animator animator;
	// Use this for initialization
	void Start () {
		string message = "";
		switch (ApplicationModel.CurrentLevel) {
		case 0: 
			message = "One drink to wake up.";
			break;
		case 1: 
			message = "Maybe something can increment my drink.";
			break;
		case 2: 
			message = "I should try anything new...";
			break;
		default:
			message = "I'm tired of this routine.";
			break;
		}
		StartCoroutine(ShowMessage(message));
	}
	
	IEnumerator ShowMessage(string message)
	{
		text.text = message;

		yield return new WaitForSeconds (3);
		animator.SetBool ("Show", true);

		yield return new WaitForSeconds (5);

		animator.SetBool ("Show", false);
	}
}
