using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ItemGUIName
{
	Blank,
	AxeIcon,
	KnifeIcon,
	HealthPotionIconSmall
}

public enum ItemType
{
	Weapon,
	Armor,
	Misc,
	Consumable,
	Quest,
	Generator,
	Enhancer
}

[System.Serializable]
public class Item {
	public string ItemName;
	public  bool Stackable;
	public int MaxStack;
	public ItemType Type;
	public string ItemGameObject;
	public string Atlas;
	public string ItemSprite;
	public string Description;

	[System.NonSerialized]
	public int StackAmount = 1;

	protected Item CloneBase(Item item)
	{
		item.ItemName = ItemName;
		item.Stackable = Stackable;
		item.MaxStack = MaxStack;
		item.Type = Type;
		item.ItemGameObject = ItemGameObject;
		item.Atlas = Atlas;
		item.ItemSprite = ItemSprite;
		item.Description = Description;

		return item;
	}

	public virtual Item Clone()
	{
		return new Item();
	}
}

[System.Serializable]
public class ItemInInventory {
	public ItemType itemType;
	public int Index;
}
