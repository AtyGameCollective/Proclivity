using UnityEngine;
using System.Collections;

public class TurnOffCollidersSprites : MonoBehaviour {
	void Awake()
	{
		var renderers = GetComponentsInChildren<SpriteRenderer> ();
		for (int i = 0; i < renderers.Length; i++) {
			renderers [i].enabled = false;
		}
	}
}
