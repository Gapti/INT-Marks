using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System;

[CustomEditor(typeof(Inventory))]
public class InventoryInspector : Editor {

	public ItemType itemType = ItemType.Weapon;
	public int Index;
	public bool OpenFoldout;
	
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		var control = (Inventory)target;

		OpenFoldout = EditorGUILayout.Foldout(OpenFoldout,"Start Up Items" );

		if(OpenFoldout) 
		{
			for (int i = 0; i < control.StartUpItems.Count; i++) 
			{
				var itemInfo = GameManager.Instance.itemDatabase.Get(control.StartUpItems[i].itemType,control.StartUpItems[i].Index);
				EditorGUILayout.LabelField("Item Name",itemInfo.ItemName);
				control.StartUpItems[i].itemType = (ItemType)EditorGUILayout.EnumPopup("itemType" ,(Enum)control.StartUpItems[i].itemType);
				//EditorGUILayout.IntField("Stack Amount",itemInfo.StackAmount);
				control.StartUpItems[i].Index = EditorGUILayout.IntSlider("Index",control.StartUpItems[i].Index,0,GameManager.Instance.itemDatabase.NumberOfItems(control.StartUpItems[i].itemType) - 1);
			

				if(GUILayout.Button("Remove Item"))
				{
					control.StartUpItems.RemoveAt(i);
				}

				EditorGUILayout.Separator();
			}
		}


		EditorGUILayout.Separator();
		EditorGUILayout.LabelField("Add Your Item below");
		EditorGUILayout.Separator();

		itemType = (ItemType)EditorGUILayout.EnumPopup("itemType" ,(Enum)itemType);

		Index = EditorGUILayout.IntSlider("Index",Index,0,GameManager.Instance.itemDatabase.NumberOfItems(itemType) - 1);

		var MoreItemInfo = GameManager.Instance.itemDatabase.Get(itemType,Index);
		EditorGUILayout.LabelField("Item Name",MoreItemInfo.ItemName);

		if(GUILayout.Button("Add Item"))
		{
			ItemInInventory newItem;
			newItem = new ItemInInventory();
			newItem.Index = Index;
			newItem.itemType = itemType;
			control.StartUpItems.Add(newItem);
		}
		
	}
}
