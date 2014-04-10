using UnityEngine;
using System.Collections;

public class RightClickOptionManager : MonoBehaviour {

	private NGUIManager _nguiManager;
	private Item _item;
	private int _slotID;
	private Inventory _inventory;

	public GameObject EquipButton;
	public GameObject UseButton;
	public GameObject TakeButton;

	public UIGrid grid;

	public NGUIManager NguiManager {
		get 
		{
			return _nguiManager;
		}
		set
		{
			_nguiManager = value;
		}
	}

	public Item item {
		get {
			return _item;
		}
		set {
			_item = value;

			DrawRightClick();
		}
	}

	public Inventory inventory{
		get
		{
			return _inventory;
		}
		set
		{
			_inventory = value;
		}
	}

	void DrawRightClick()
	{
		switch(_item.Type)
		{
		case ItemType.Armor:
			UseButton.SetActive(false);
			EquipButton.SetActive(true);
			break;
		case ItemType.Consumable:
			UseButton.SetActive(true);
			EquipButton.SetActive(false);
			break;
		case ItemType.Enhancer:
			UseButton.SetActive(true);
			EquipButton.SetActive(false);
			break;
		case ItemType.Generator:
			UseButton.SetActive(true);
			EquipButton.SetActive(true);
			break;
		case ItemType.Misc:
			UseButton.SetActive(true);
			EquipButton.SetActive(false);
			break;
		case ItemType.Quest:
			UseButton.SetActive(false);
			EquipButton.SetActive(false);
			break;
		case ItemType.Weapon:
			UseButton.SetActive(false);
			EquipButton.SetActive(true);
			break;
		}

		if(_inventory.PlayerInventory)
		{
			TakeButton.SetActive(false);
		}

		grid.Reposition();
	}


	public void DropItem()
	{
		_nguiManager.DropItem();
	}

	public void Cancel()
	{
		NGUITools.Broadcast("DestroyRightClickWindow");
	}

	public void UseItem()
	{
		_nguiManager.UseItem();
	}

	public void PickUpItem()
	{
		_nguiManager.PickUpItem();
	}

}
