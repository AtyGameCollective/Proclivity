using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player") {
			SceneManager.LoadScene ("Minigame");
		}
	}
}
