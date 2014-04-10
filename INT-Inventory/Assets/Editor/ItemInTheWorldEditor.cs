using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

[CustomEditor(typeof(ItemInTheWorld))]
public class ItemInTheWorldEditor : Editor  
{

	public override void OnInspectorGUI()
	{
		var control = (ItemInTheWorld)target;
		var Database = GameManager.Instance.itemDatabase;
		control.itemType = (ItemType)EditorGUILayout.EnumPopup("itemType" ,(Enum)control.itemType);
		
		control.ItemIndex = EditorGUILayout.IntSlider("Index",control.ItemIndex,0,Database.NumberOfItems(control.itemType) - 1);
		
		var MoreItemInfo = Database.Get(control.itemType,control.ItemIndex);
		EditorGUILayout.LabelField("Item Name",MoreItemInfo.ItemName);
	}
}
