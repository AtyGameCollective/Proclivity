using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class ColliderToggle : MonoBehaviour {
	[SerializeField]
	Collider2D collider;
	[SerializeField]
	SpriteRenderer[] colliderSprites;
	[SerializeField]
	bool upIsStraight = true;
	float alphaOn = 1f;
	float alphaOff = 0.1f;
	[SerializeField]
	bool active;

	public bool Active {
		get {
			return active;
		}
		set {
			if (active != value) {
				collider.enabled = value;
				StartCoroutine (ChangeAlpha ());
			}
			active = value;
		}
	}

	void OnEnable()
	{
		collider.enabled = active;
		StartCoroutine (ChangeAlpha ());
	}


	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Player" && col.GetType () == typeof(CircleCollider2D)) {
			if (upIsStraight) {
				if (CrossPlatformInputManager.GetAxis("Vertical") < -0.5f) {
					Active = false;
				} else {
					Active = true;
				}
			} else {
				if (CrossPlatformInputManager.GetAxis ("Vertical") > 0.5f) {
					Active = true;
				} else {
					Active = false;
				}
			}
		}
	}


	IEnumerator ChangeAlpha()
	{
		float time = 0;
		float totalTime = 0.25f;

		Color color = colliderSprites [0].color;
		Color finalColor = new Color (color.r, color.g, color.b, upIsStraight && active ||  !upIsStraight && !active ? alphaOn : alphaOff);
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
