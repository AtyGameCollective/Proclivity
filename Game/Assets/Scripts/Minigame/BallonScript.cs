using UnityEngine;
using System.Collections;

public class BallonScript : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer badBallon;

    [SerializeField]
    private SpriteRenderer goodBallon;

    [SerializeField]
    private float ocilation = 0.2f;

    private bool isShowing = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void ShowBallon(bool isOK)
    {
        if (isOK)
        {
            goodBallon.gameObject.SetActive(true);
        }
        else
        {
            badBallon.gameObject.SetActive(true);
        }

        isShowing = true;
    }
}
