using UnityEngine;
using System.Collections;

public class BlankSlotManager : GUIManager {

	public override void Draw()
	{
	}

	public override void ShowRightClickWindow(Vector3 pos, Item Item, int SlotID, Inventory inventory)
	{
	}

	public override void MakeGUIWindow (Inventory inventory, string InventoryName)
	{
		for(int a = 0; a < inventory.SlotAmount; a++ )
		{
			GameObject slot = NGUITools.AddChild(this.gameObject, BlankSlotPrefab);
			UISprite sprite = slot.GetComponent<UISprite>();
			sprite.depth = 1;
		}

		UIGrid grid = GetComponent<UIGrid>();

		grid.Reposition();
	}
}
