using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour {
	List<int> itemsCollected= new List<int>();
	int itemsNeeded;
	[SerializeField]
	Collider2D doorCollider;
	void Awake()
	{
		itemsNeeded = ApplicationModel.CurrentLevel+2;
	}

	public int ItemsCollectedCount{ get { return itemsCollected.Count; } }
	public void ItemCollected(CollectableItem item)
	{
		itemsCollected.Add(item.ItemOrder);
		OpenDoor ();
	}

	void OpenDoor()
	{
		if (ItemsCollectedCount == itemsNeeded) {
			doorCollider.enabled = true;
		}
	}
}
