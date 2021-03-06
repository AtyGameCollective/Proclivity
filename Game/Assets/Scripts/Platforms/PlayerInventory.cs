﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInventory : MonoBehaviour {
	List<int> itemsCollected= new List<int>();
	int itemsNeeded;
	[SerializeField]
	Collider2D doorCollider;
	void Awake()
	{
		itemsNeeded = ApplicationModel.CurrentLevel+2;
	}

    void Update()
    {
		if (Input.GetButtonDown("Cancel"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Splash");
        }
    }

	public int ItemsCollectedCount{ get { return itemsCollected.Count; } }
	public void ItemCollected(CollectableItem item)
	{
		itemsCollected.Add(item.ItemOrder);
		OpenDoor ();
	}

	void OpenDoor()
	{
		if (ItemsCollectedCount == itemsNeeded || ItemsCollectedCount >= 4) {
			doorCollider.enabled = true;
		}
	}
}
