using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MenuOptionToggle : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IPointerClickHandler {

	Toggle toggle;

	[SerializeField]
	EventTrigger onClickEvent;


	// Use this for initialization
	void Start () {
		toggle = GetComponent<Toggle> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		toggle.Select ();
	}

	#endregion

	#region ISelectHandler implementation

	public void OnSelect (BaseEventData eventData)
	{
		toggle.isOn = true;
	}

	#endregion

	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		Debug.Log ("Option Clicked " + name);
		onClickEvent
	}

	#endregion
}
