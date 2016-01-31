using UnityEngine;

using System.Collections;

public class CollectableItem : MonoBehaviour {
	Collider2D coll2D;
	bool active;
	SpriteRenderer sRenderer;

	[SerializeField]
	int itemOrder = 0;

	public int ItemOrder {
		get {
			return itemOrder;
		}
		set {
			itemOrder = value;
		}
	}

	int playerItemIndex = -1;

	void OnEnable()
	{
		sRenderer = GetComponent<SpriteRenderer> ();
		coll2D = GetComponent<Collider2D> ();
		active = true;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player") {
			playerItemIndex = col.GetComponent<PlayerInventory>().ItemsCollectedCount;
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Player") {
			if (Input.GetButtonDown ("Fire1")) {
				if (active && playerItemIndex == itemOrder) {
					active = false;
				} else {
					return;
				}
				coll2D.enabled = false;
				col.GetComponent<PlayerInventory> ().ItemCollected (this);
				StartCoroutine (CollectItemAnimation ());
			}
		}

	}

	IEnumerator CollectItemAnimation()
	{
		yield return null;
		Debug.Log (gameObject.name + " Collected");
		float time = 0;
		float totalTime = 1f;
		Vector3 startScale = transform.localScale;
		Vector3 endScale = startScale * 3f;
		Color startColor = sRenderer.color;
		Color endColor = Color.clear;


		while (time < totalTime) {
			time += Time.deltaTime;
			yield return null;
			sRenderer.color = Color.Lerp(startColor, endColor,time/totalTime);
			transform.localScale = Vector3.Lerp (startScale, endScale, time / totalTime);

		}

		sRenderer.color =  endColor;
		transform.localScale = endScale;
		gameObject.SetActive (false);
	}

}
