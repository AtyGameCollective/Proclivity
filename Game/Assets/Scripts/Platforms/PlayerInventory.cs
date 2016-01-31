using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour {
	List<int> itemsCollected= new List<int>();

	public int ItemsCollectedCount{ get { return itemsCollected.Count; } }
	public void ItemCollected(CollectableItem item)
	{
		itemsCollected.Add(item.ItemOrder);
	}
}
