using UnityEngine;
using System.Collections;

public class ParallaxLooper : MonoBehaviour {
	[SerializeField]
	SpriteRenderer [] sRenderers;

	Collider2D boundsColl;

	float speed = -0.5f;
	float sin = 0;
	// Use this for initialization
	void Start () {
		boundsColl = GetComponent<Collider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 delta = new Vector3 ( speed * Time.deltaTime, 0);
		int count = sRenderers.Length;
		for (int i = 0; i < sRenderers.Length; ) {
			sRenderers [i].transform.position += delta;
			if (sRenderers [i].bounds.max.x < boundsColl.bounds.min.x) {
				var temp = sRenderers[0];
				temp.transform.position = new Vector3(sRenderers [sRenderers.Length - 1].bounds.max.x + temp.bounds.extents.x, temp.transform.position.y,temp.transform.position.z);
				for (int j = 0; j < sRenderers.Length-1; j++) {
					sRenderers [j] = sRenderers [j + 1];
				}
				sRenderers [sRenderers.Length-1] = temp;
			} else {
				i++;
			}
		}
	}
}
