using UnityEngine;
using System.Collections;


public abstract class GUIManager : MonoBehaviour {
	

	public abstract void Draw();
	public abstract void MakeGUIWindow(Inventory inventory, string InventoryName);
	public abstract void ShowRightClickWindow(Vector3 pos, Item item, int SlotID, Inventory inventory);

	public GameObject ItemSlotPrefab;
	public GameObject BlankSlotPrefab;

	public Inventory _inventory;

	public void DropItem(Item item, int SlotID)
	{
		if(item.ItemGameObject != null)
		{
			Instantiate(Resources.Load(item.ItemGameObject, typeof (GameObject)), new Vector3(0,0,0), Quaternion.identity);
		}
		
		_inventory.RemoveItem(SlotID);
		
		item = null;
	}

	public void UseItem(Item item, int SlotID)
	{
		//delagate test take player energy on everything
		INTEvents.TakePlayerEnergy(21);

		if(_inventory.InventoryList[SlotID].Stackable)
		{
			if(_inventory.InventoryList[SlotID].StackAmount > 1)
			{
				_inventory.InventoryList[SlotID].StackAmount --;
			}
			else
			{
				_inventory.RemoveItem(SlotID);
			}
		}

		Draw();

	}

	public void PickUpItem(Item item, int SlotID)
	{
		if(!_inventory.PlayerInventory)
		{
			if(GameManager.Instance.PlayerInventory.Additem(item))
			{
				_inventory.RemoveItem(SlotID);
				
				_inventory.ReDrawGUI();
				GameManager.Instance.PlayerInventory.ReDrawGUI();

				NGUITools.Broadcast("DestroyRightClickWindow");
			}
		}
	}


}
