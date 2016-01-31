using UnityEngine;
using System.Collections;


public class AlphaAnimation : MonoBehaviour {
	SpriteRenderer sRenderer;

	Color minColor = new Color(1,1,1,0.5f);
	Color maxColor = Color.white;
	float totalTime = 3.5f;
	bool update;
	// Use this for initialization
	void Awake() {
		sRenderer = GetComponent<SpriteRenderer> ();
	}


	void OnEnable() {

		StartCoroutine (Animation ());
	}

	void OnDisable()
	{
		update = false;
	}
	


	IEnumerator Animation()
	{
		update = true;
		bool goToMinimum = true;
		while (update) {
			Color target = goToMinimum ? minColor : maxColor;
			Color startColor = sRenderer.color;
			float time = 0;
			while (time < totalTime) {
				sRenderer.color = Color.Lerp (startColor, target, time / totalTime);
				time += Time.deltaTime;
				yield return null;
			}
			goToMinimum = !goToMinimum;
		}
		yield return null;
	}
}
