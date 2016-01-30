using UnityEngine;
using System.Collections;

public class ColliderToggle : MonoBehaviour {
	[SerializeField]
	Collider2D collider;
	[SerializeField]
	SpriteRenderer[] colliderSprites;
	[SerializeField]
	bool upIsOn = true;
	float alphaOn = 1f;
	float alphaOff = 0.4f;
	bool active;


	void OnEnable()
	{
		active = collider.enabled;

	}


	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Player" && col.GetType () == typeof(CircleCollider2D)) {
			if (Input.GetAxis ("Vertical") > 0.5f) {
				if (active == upIsOn)
					return;
				active = upIsOn;
			} else if (Input.GetAxis ("Vertical") < -0.5f) {
				if (active == !upIsOn)
					return;
				active = !upIsOn;
			} else {
				return;
			}

			collider.enabled = active;
			StartCoroutine (ChangeAlpha ());
//			for (int i = 0; i < colliderSprites.Length; i++) {
//				colliderSprites [i].color = new Color (1, 1, 1, active ? alphaOn : alphaOff);
//			}
		}

	}


	IEnumerator ChangeAlpha()
	{
		float time = 0;
		float totalTime = 0.25f;

		Color color = colliderSprites [0].color;
		Color finalColor = new Color (color.r, color.g, color.b, active ? alphaOn : alphaOff);
		while (time < totalTime) {
			for (int i = 0; i < colliderSprites.Length; i++) {
				colliderSprites [i].color = Color.Lerp(color, finalColor, time/totalTime);
			}
			yield return new WaitForFixedUpdate ();
			time += Time.deltaTime;
		}
		for (int i = 0; i < colliderSprites.Length; i++) {
			colliderSprites [i].color = finalColor;
		}
	}
}
