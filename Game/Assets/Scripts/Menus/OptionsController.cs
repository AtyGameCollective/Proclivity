using UnityEngine;
using System.Collections;

public class OptionsController : MonoBehaviour {
	[SerializeField]
	MenuOptionToggle [] options;

	int index = 0;
	float delay  = 1;
	void Update()
	{
		delay -= Time.deltaTime;
		if (delay < 0) {
			float vertical = Input.GetAxis ("Vertical");
			if (vertical != 0) {
				if (vertical < 0) {
					
					index = index + 1;
				} else if (vertical > 0) {
					index = index - 1;
				}
				
				
				if (index < 0) {
					index = options.Length - 1;
				} else if (index >= options.Length) {
					index = 0;
				}
				options [index].Select ();
				delay = 0.4f;
			}

		}
		if (Input.GetButton ("Submit")) {
			options [index].ActivateOption ();
		}
	}

}
