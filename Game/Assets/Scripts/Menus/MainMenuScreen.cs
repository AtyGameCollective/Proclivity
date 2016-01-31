using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour {

	public void StartGame()
	{
		ApplicationModel.CurrentLevel = 0;
		SceneManager.LoadScene ("House");
	}
}
