using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class Inventory : MonoBehaviour {

	public GUIManager _guiManager;
	public int SlotAmount = 16;

	[System.NonSerialized]
	public Item[] InventoryList;

	public GameObject UIRoot;
	public GameObject InventoryGUIPrefab;
	private bool ShowInventory = false;
	private GameObject GUIWindowTemp;
	public string InventoryName;
	public bool PlayerInventory;

	[HideInInspector]
	[SerializeField]
	public List<ItemInInventory> StartUpItems;


	void Start()
	{
		if(PlayerInventory)
			GameManager.Instance.PlayerInventory = this;

		InventoryList = new Item[SlotAmount];

		if(StartUpItems.Count == 0)
			return;

		for (int i = 0; i < StartUpItems.Count; i++)
		{
			var item = GameManager.Instance.itemDatabase.Get(StartUpItems[i].itemType, StartUpItems[i].Index);
			Additem(item);
		}

	}

	public void RemoveItem(int pos)
	{
		InventoryList[pos] = null;

		if(_guiManager != null)
		_guiManager.Draw();
	}

	public bool Additem(Item item)
	{
		for (int i = 0; i < InventoryList.Length; i++) 
		{
			if(InventoryList[i] == null)
			{
				Additem(item, i);
				return true;
			}
		}

		return false;
	}
	

	public bool Additem(Item item, int pos)
	{

		if(InventoryList[pos] == null)
		{
			InventoryList[pos] = item;

			return true;
		}
		//check to see if the item is stackable
		if(item.Stackable)
		{
			if(InventoryList[pos].ItemName == item.ItemName && InventoryList[pos].StackAmount < InventoryList[pos].MaxStack)
			{
				if((InventoryList[pos].StackAmount + item.StackAmount) > InventoryList[pos].MaxStack)
				{
					int tempStackAmount = InventoryList[pos].MaxStack - InventoryList[pos].StackAmount;

					InventoryList[pos].StackAmount += tempStackAmount;
					item.StackAmount -= tempStackAmount;

					return false;
				}

				InventoryList[pos].StackAmount += item.StackAmount;

				return true;
			}
		}

		for (int i = 0; i < InventoryList.Length; i++) 
		{
			if(InventoryList[i] == item)
				return false;
		}

			///check if there is room
		int slotId = CheckForSpace(item);

		if(slotId == -1)
		{
			return false;
		}
		else
		{
			Additem(item, slotId);


			return true;
		}

	}

	public void ReDrawGUI()
	{
		if(_guiManager != null)
			_guiManager.Draw();
	}

	int CheckForSpace (Item item)
	{
		for (int a = 0; a < this.InventoryList.Length; a++) 
		{
			if (InventoryList[a] == null) 
			{

				return a;
			}
		}

		return -1;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.I))
		{
			ToggleInventory();
		}
	}
	
	public void ToggleInventory()
	{
		ShowInventory =! ShowInventory;

		if(ShowInventory)
		{
			MakeGUI();
		}
		else
		{
			NGUITools.Destroy(GUIWindowTemp);
			NGUITools.Broadcast("DestroyRightClickWindow");
		}
	}

	void MakeGUI()
	{
		GUIWindowTemp = NGUITools.AddChild(UIRoot, InventoryGUIPrefab);
		GUIWindowTemp.transform.localPosition = new Vector2(480f,-441f);
		ConnectNGUIManager cmn = (ConnectNGUIManager) GUIWindowTemp.GetComponent<ConnectNGUIManager>();

		GUIManager BlankSlotMan = cmn.BlankSlotManagerConnect;
		BlankSlotMan.MakeGUIWindow(this, InventoryName);

		_guiManager = cmn.NGUIManagerConnect;
		_guiManager.MakeGUIWindow(this, InventoryName);
	}
	
}
