using UnityEngine;
using System.Collections;

public class NGUIManager : GUIManager {

	public ItemSlot[] Slots;
	public UILabel Title;
	public GameObject RightClickPrefab;
	private GameObject _currentRightClickWindowOpen = null;
	private Item _item;
	private int _slotID;

	public GameObject currentRightClickWindowOpen {
		get {
			return _currentRightClickWindowOpen;
		}
		set {
			_currentRightClickWindowOpen = value;
		}
	}

	public override void Draw ()
	{
		FillData();
	}

	void FillData ()
	{
		for(int a = 0; a < Slots.Length; a++)
		{
			Slots[a].inventory = _inventory;
			Slots[a].SetItem = _inventory.InventoryList[a];
		}
	}

	public override void MakeGUIWindow (Inventory inventory, string inventoryName)
	{
		_inventory = inventory;
		Slots = new ItemSlot[inventory.InventoryList.Length];

		Title.text = inventoryName;

		//make the slots
		for(int a = 0; a < inventory.InventoryList.Length; a++)
		{
			GameObject slot = NGUITools.AddChild(this.gameObject, ItemSlotPrefab);
			slot.transform.name = a.ToString("000");
			Slots[a] = slot.GetComponent<ItemSlot>();
			Slots[a].SlotID = a;
			UISprite sprite = slot.GetComponent<UISprite>();
			sprite.depth = 2;
		}

		UIGrid grid = GetComponent<UIGrid>();
		grid.Reposition();

		FillData();
	}

	public override void ShowRightClickWindow(Vector3 pos, Item Item, int SlotID, Inventory inventory)
	{
		///make sure no windows are open
		NGUITools.Broadcast("DestroyRightClickWindow");
		DestroyRightClickWindow();

		_item = Item;
		_slotID = SlotID;
		_inventory = inventory;
		currentRightClickWindowOpen = NGUITools.AddChild(_inventory.UIRoot, RightClickPrefab);
		_currentRightClickWindowOpen.transform.position = pos;
		RightClickOptionManager rightClickWindow = _currentRightClickWindowOpen.GetComponent<RightClickOptionManager>();
		rightClickWindow.NguiManager = this;
		rightClickWindow.inventory = inventory;
		rightClickWindow.item = Item;
	}


	public void DestroyRightClickWindow()
	{
		if(_currentRightClickWindowOpen != null)
		{
			NGUITools.Destroy(_currentRightClickWindowOpen);
		}

		_currentRightClickWindowOpen = null;
		_item = null;
		_slotID = 0;
	}

	public void DropItem()
	{
		DropItem(_item,_slotID);
		DestroyRightClickWindow();
	}

	public void UseItem()
	{
		UseItem (_item, _slotID);
		DestroyRightClickWindow();
	}

	public void PickUpItem()
	{
		PickUpItem (_item, _slotID);
	}

	public void CloseInventoryWindow()
	{
		_inventory.ToggleInventory();
	}
	
}
