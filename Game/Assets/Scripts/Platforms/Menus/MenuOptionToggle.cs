using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;


public class MenuOptionToggle : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IPointerClickHandler {

	Toggle toggle;

	[SerializeField]
	UnityEvent onClickEvent;

	// Use this for initialization
	void Start () {
		toggle = GetComponent<Toggle> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Select ()
	{
		toggle.Select ();
	}

	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		Select ();
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
		ActivateOption ();
	}

	#endregion

	public void ActivateOption ()
	{
		onClickEvent.Invoke ();
	}
}
