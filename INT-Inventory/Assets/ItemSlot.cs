using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemSlot :  UIDragDropItem {

	public Inventory inventory;
	public Item item;
	public UISprite itemSprite;
	public UILabel itemStackCountLabel;
	public int SlotID;

	private UILabel _stackLabel;

	public Item SetItem {
		get 
		{
			return item;
		}
		set 
		{
			if(value != null)
			{
				enabled = true;
				item = value;
				itemSprite.spriteName = item.ItemSprite;
			}
			else
			{
				enabled = false;
				item = null;
				itemSprite.spriteName = "Invisable";
			}

				SetStackLabel();
		}
	}

	void Awake()
	{
		itemSprite = GetComponent<UISprite>();
		_stackLabel = this.GetComponentInChildren<UILabel>();
	}

	void SetStackLabel()
	{

		if(item == null) 
		{
			_stackLabel.text = "";
			return;
		}


		if(inventory.InventoryList[SlotID].Stackable)
		{
			_stackLabel.text = inventory.InventoryList[SlotID].StackAmount.ToString();
		}
		else
		{
			_stackLabel.text = "";
		}

	}
	

	void OnTooltip (bool isOver)
	{
		if(item != null && isOver)
		{
			string toolTipText;

			switch(item.Type)
			{
			case ItemType.Weapon:
				toolTipText = item.ItemName +" \n " + "[4EFF00]" + item.Description;
				UITooltip.ShowText(toolTipText);
				break;
			case ItemType.Armor:
				toolTipText = item.ItemName +" \n " + "[4EFF00]" + item.Description;
				UITooltip.ShowText(toolTipText);
				break;
			case ItemType.Misc:
				toolTipText = item.ItemName +" \n " + "[4EFF00]" + item.Description;
				UITooltip.ShowText(toolTipText);
				break;
			case ItemType.Consumable:
				toolTipText = item.ItemName +" \n " + "[4EFF00]" + item.Description;
				UITooltip.ShowText(toolTipText);
				break;
			case ItemType.Generator:
				toolTipText = item.ItemName +" \n " + "[4EFF00]" + item.Description;
				UITooltip.ShowText(toolTipText);
				break;
			case ItemType.Enhancer:
				toolTipText = item.ItemName +" \n " + "[4EFF00]" + item.Description;
				UITooltip.ShowText(toolTipText);
				break;
			case ItemType.Quest:
				toolTipText = item.ItemName +" \n " + "[4EFF00]" + item.Description;
				UITooltip.ShowText(toolTipText);
				break;
			}
		}
		else
		{
			UITooltip.ShowText(null);
		}
	}

	void OnPress(bool isPressed)
	{
		if(isPressed)
		{
			itemSprite.depth = 3;
			NGUITools.Broadcast("DestroyRightClickWindow");
		
		}
	}

	protected override void OnDragDropRelease (GameObject surface)
	{
		if(surface != null)
		{
			ItemSlot itemSlot = surface.GetComponent<ItemSlot>();

			if(itemSlot != null)
			{
				if(itemSlot.inventory.Additem(item, itemSlot.SlotID))
				{
					inventory.RemoveItem(SlotID);
					item = null;
				}

				itemSlot.inventory.ReDrawGUI();
			}
		}
		else
		{
			inventory._guiManager.DropItem(item, SlotID);
		}

		base.OnDragDropRelease(surface);
		itemSprite.depth = 2;

		inventory.ReDrawGUI();
	}
	

	void OnClick()
	{
		int mouseClick = UICamera.currentTouchID;
		
		//check for right click
		if(mouseClick == -2 && item != null)
		{
			inventory._guiManager.ShowRightClickWindow(this.transform.position, item, SlotID, inventory);
		}
	}

	void OnDoubleClick () 
	{
		if(!inventory.PlayerInventory)
		{
			if(GameManager.Instance.PlayerInventory.Additem(item))
			{
				inventory.RemoveItem(SlotID);
				item = null;

				inventory.ReDrawGUI();
				GameManager.Instance.PlayerInventory.ReDrawGUI();
			}
		}
	}

	
}
