using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour {

	[SerializeField]
	Text splashText;
	[SerializeField]
	Image atyLogo;
	[SerializeField]
	GameObject menuParent;

	void Start()
	{
		StartCoroutine (SplashMenuAnimation());
	}

	IEnumerator SplashMenuAnimation()
	{
		float time = 0;
		float totalTime = 1f;
		Vector3 startScale = transform.localScale;
		Vector3 endScale = startScale * 3f;
		Color startColor = Color.clear;
		Color endColor = Color.white;
		splashText.color = Color.clear;
		atyLogo.color = Color.clear;
		menuParent.SetActive (false);

		while (time < totalTime) {
			time += Time.deltaTime;
			yield return null;
			splashText.color = Color.Lerp(startColor, endColor,time/totalTime);

		}

		yield return new WaitForSeconds (1f);
		time = 0;

		while (time < totalTime) {
			time += Time.deltaTime;
			yield return null;
			atyLogo.color = Color.Lerp(startColor, endColor,time/totalTime);
		}

		yield return new WaitForSeconds (3f);
		time = 0;

		startColor = Color.white;
     endColor = Color.clear;
		while (time < totalTime) {
			time += Time.deltaTime;
			yield return null;
			splashText.color = Color.Lerp(startColor, endColor,time/totalTime);
			atyLogo.color = Color.Lerp(startColor, endColor,time/totalTime);
		}
		menuParent.SetActive (true);
	}


	public void StartGame()
	{
		ApplicationModel.CurrentLevel = 0;
		SceneManager.LoadScene ("House");
	}
}
