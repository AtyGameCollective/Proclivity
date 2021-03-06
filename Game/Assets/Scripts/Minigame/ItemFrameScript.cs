﻿using UnityEngine;
using System.Collections;
using System.ComponentModel;
using System;
using UnityStandardAssets.CrossPlatformInput;

public class ItemFrameScript : MonoBehaviour
{

    //TODO: Mac OS mapping
    public enum buttons
    {
        A,
        B,
        X,
        Y
    }

    private string[] buttonsNames = { "Green", "Red", "Blue", "Yellow" };

    public delegate void onUseEventHandler(ItemFrameScript item, bool idOK);
    public event onUseEventHandler onUse;

    private SpriteRenderer actualBorder;

    public int position { get; set; }

    public float realPosition { get; set; }

    public int destinyPosition { get; set; }

    [SerializeField]
    private Vector3 positon;

    [SerializeField]
    private Item item;

    public Item Item
    {
        get { return item; }
        set { item = value; }
    }

    [SerializeField]
    private bool isActive = false;

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    [SerializeField]
    private buttons button = buttons.A;

    public buttons Button
    {
        get { return button; }
        set { button = value; }
    }

    [SerializeField]
    private Sprite[] Borders;

    [SerializeField]
    private Sprite[] Icons;

    [SerializeField]
    private SpriteRenderer Icon;

    void Awake()
    {
        actualBorder = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        UpdateBorder();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
			if (CrossPlatformInputManager.GetButtonDown(buttonsNames[(int)button]))
            {
                //Debug.Log(buttonsNames[(int)button]);

                if (onUse != null)
                    onUse(this, true);

                return;
            }
            else
            {
                for (int i = 0; i < buttonsNames.Length; i++)
                {
					if (CrossPlatformInputManager.GetButtonDown(buttonsNames[i]))
                    {
                        if (onUse != null)
                            onUse(this, false);

                        return;
                    };
                }
            }
        }
    }

    public void UpdateBorder()
    {
        actualBorder.sprite = Borders[(int)button];
    }

    public void ChangeIcon()
    {
        Icon.sprite = Icons[(int)(UnityEngine.Random.value * Icons.Length)];
    }
    
    public void ChangeButton(buttons newButton)
    {
        this.button = newButton;
        UpdateBorder();
    }

    public void ChangePositon(Vector3 position)
    {
        //TODO: Slide to position

        this.positon = position;

        transform.position = position;
    }
}
