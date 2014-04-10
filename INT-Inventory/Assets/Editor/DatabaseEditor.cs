using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

[CustomEditor(typeof(ItemDataBase))]
public class DatabaseEditor : Editor {

	public ItemType CurrentVisableCatagoery;

	public int index = 0;
	public int ItemIndex = 0;
	public bool IsFoldOutOpen;

	public override void OnInspectorGUI()
	{

		var path = Application.dataPath + "/StreamingAssets";
		var control = (ItemDataBase)target;

		CurrentVisableCatagoery =(ItemType) EditorGUILayout.EnumPopup("Item Catagoery", CurrentVisableCatagoery);

		switch (CurrentVisableCatagoery) {
		case ItemType.Armor:
			DrawArmorList(control);
			break;
		case ItemType.Consumable:
			DrawConsumableList(control);
			break;
		case ItemType.Enhancer:
			DrawEnhancerList(control);
			break;
		case ItemType.Generator:
			DrawGeneratorList(control);
			break;
		case ItemType.Misc:
			DrawMiscList(control);
			break;
		case ItemType.Quest:
			DrawQuestList(control);
			break;
		case ItemType.Weapon:
			DrawWeaponList(control);
			break;

		default:
						break;
		}

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Save to xml");

		if(GUILayout.Button("Save"))
		{
			control.Save(Path.Combine(path, "weapons.xml"), ItemType.Weapon);
			control.Save(Path.Combine(path, "armor.xml"), ItemType.Armor);
			control.Save(Path.Combine(path, "misc.xml"), ItemType.Misc);
			control.Save (Path.Combine(path, "consumables"), ItemType.Consumable);
			control.Save(Path.Combine(path, "Quests"), ItemType.Quest);
			control.Save(Path.Combine(path, "Generators"), ItemType.Generator);
			control.Save(Path.Combine(path, "enhancers"), ItemType.Enhancer);
		}

		if(GUILayout.Button("Load"))
		{
			control.LoadXML(Path.Combine(path, "weapons.xml"), ItemType.Weapon);
			control.LoadXML(Path.Combine(path, "armor.xml"), ItemType.Armor);
			control.LoadXML(Path.Combine(path, "misc.xml"), ItemType.Misc);
			control.LoadXML(Path.Combine(path, "consumables"), ItemType.Consumable);
			control.LoadXML(Path.Combine(path, "Quests"), ItemType.Quest);
			control.LoadXML(Path.Combine(path, "Generators"), ItemType.Generator);
			control.LoadXML(Path.Combine(path, "enhancers"), ItemType.Enhancer);
		}
		
	}

	public Rect DrawSprite (Texture2D tex, Rect sprite, Material mat, bool addPadding, int maxSize)
	{
		float paddingX = addPadding ? 4f / tex.width : 0f;
		float paddingY = addPadding ? 4f / tex.height : 0f;
		float ratio = (sprite.height + paddingY) / (sprite.width + paddingX);
		
		ratio *= (float)tex.height / tex.width;
		
		// Draw the checkered background
		Color c = GUI.color;
		Rect rect = GUILayoutUtility.GetRect(0f, 0f);
		rect.width = Screen.width - rect.xMin;
		rect.height = rect.width * ratio;
		GUILayout.Space(Mathf.Min(rect.height, maxSize));
		GUI.color = c;
		
		if (maxSize > 0)
		{
			float dim = maxSize / Mathf.Max(rect.width, rect.height);
			rect.width *= dim;
			rect.height *= dim;
		}
		
		// We only want to draw into this rectangle
		if (Event.current.type == EventType.Repaint)
		{
			if (mat == null)
			{
				GUI.DrawTextureWithTexCoords(rect, tex, sprite);
			}
			else
			{
				// NOTE: DrawPreviewTexture doesn't seem to support BeginGroup-based clipping
				// when a custom material is specified. It seems to be a bug in Unity.
				// Passing 'null' for the material or omitting the parameter clips as expected.
				UnityEditor.EditorGUI.DrawPreviewTexture(sprite, tex, mat);
				//UnityEditor.EditorGUI.DrawPreviewTexture(drawRect, tex);
				//GUI.DrawTexture(drawRect, tex);
			}
			rect = new Rect(sprite.x + rect.x, sprite.y + rect.y, sprite.width, sprite.height);
		}
		return rect;
	}


	void CommonItemProps(Item item)
	{
		item.ItemName = EditorGUILayout.TextField("Item Name", item.ItemName);
		item.Stackable = EditorGUILayout.Toggle("Stackable ?", item.Stackable);
		//item.StackAmount = EditorGUILayout.IntField("Stack Amount", item.StackAmount);
		item.MaxStack = EditorGUILayout.IntField("Max Stack", item.MaxStack);
		item.ItemGameObject = EditorGUILayout.TextField("GameObject for Item", item.ItemGameObject);
		item.Atlas = EditorGUILayout.TextField("Sprite Atlas", item.Atlas);
		item.Description = EditorGUILayout.TextField("Item Description", item.Description);

		UIAtlas atlasObject = Resources.Load <UIAtlas>(item.Atlas);

		if(atlasObject != null)
		{
			string[] sprites = atlasObject.GetListOfSprites().ToArray();

			index = System.Array.IndexOf<string>(sprites, item.ItemSprite);
			index = EditorGUILayout.Popup(index, sprites);


			UISpriteData sprite = (index > 0) ? atlasObject.GetSprite(sprites[index]) : null;


			if(sprite != null)
			{
				item.ItemSprite = sprite.name;

				Rect spriteRect = new Rect(sprite.x, sprite.y,sprite.width, sprite.height);

				spriteRect = NGUIMath.ConvertToTexCoords(spriteRect, atlasObject.spriteMaterial.mainTexture.width, atlasObject.spriteMaterial.mainTexture.height);
				DrawSprite((Texture2D)atlasObject.spriteMaterial.mainTexture, spriteRect, null,false,100);
			}

		}
	}

	void DrawWeaponList(ItemDataBase control)
	{
		ItemIndex = EditorGUILayout.IntSlider("Item Index", ItemIndex, 0, control.WeaponList.Count - 1);

		EditorGUILayout.BeginHorizontal();

		if(GUILayout.Button("Add Item"))
		{
			var newWeapon = new Weapon();
			newWeapon.Type = ItemType.Weapon;
			control.WeaponList.Add(newWeapon);
			ItemIndex = control.WeaponList.Count - 1;
		}

		if(GUILayout.Button("Remove Item"))
		{
			control.WeaponList.RemoveAt(ItemIndex);
			ItemIndex = control.WeaponList.Count - 1;
		}

		EditorGUILayout.EndHorizontal();

		EditorGUILayout.Space();

		CommonItemProps(control.WeaponList[ItemIndex]);

		control.WeaponList[ItemIndex].Damage = EditorGUILayout.FloatField("Damage", control.WeaponList[ItemIndex].Damage);
		control.WeaponList[ItemIndex].MinRange = EditorGUILayout.FloatField("MinRange", control.WeaponList[ItemIndex].MinRange);
		control.WeaponList[ItemIndex].MaxRange = EditorGUILayout.FloatField("MaxRange", control.WeaponList[ItemIndex].MaxRange);
		control.WeaponList[ItemIndex].CriticalStrike = EditorGUILayout.FloatField("CriticalStrike", control.WeaponList[ItemIndex].CriticalStrike);
		control.WeaponList[ItemIndex].BatteryRecharge = EditorGUILayout.FloatField("BatteryRecharge", control.WeaponList[ItemIndex].BatteryRecharge);
		control.WeaponList[ItemIndex].Exhaustion = EditorGUILayout.FloatField("Exhaustion", control.WeaponList[ItemIndex].Exhaustion);
	
	}

	void DrawArmorList(ItemDataBase control)
	{
		ItemIndex = EditorGUILayout.IntSlider("Item Index", ItemIndex, 0, control.ArmorList.Count - 1);
		
		EditorGUILayout.BeginHorizontal();
		
		if(GUILayout.Button("Add Item"))
		{
			var newArmor = new Armor();
			newArmor.Type = ItemType.Armor;
			control.ArmorList.Add(newArmor);
			ItemIndex = control.ArmorList.Count - 1;
		}
		
		if(GUILayout.Button("Remove Item"))
		{
			control.ArmorList.RemoveAt(ItemIndex);
			ItemIndex = control.ArmorList.Count - 1;
		}
		
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.Space();
		
		CommonItemProps(control.ArmorList[ItemIndex]);

		control.ArmorList[ItemIndex].Fits = (ArmorType)EditorGUILayout.EnumPopup("Fits ", control.ArmorList[ItemIndex].Fits);
		control.ArmorList[ItemIndex].ArmorValue = EditorGUILayout.FloatField("Armor Value", control.ArmorList[ItemIndex].ArmorValue);
		control.ArmorList[ItemIndex].DefenseValue = EditorGUILayout.FloatField("Defense Value", control.ArmorList[ItemIndex].DefenseValue);

	}

	void DrawMiscList(ItemDataBase control)
	{
		ItemIndex = EditorGUILayout.IntSlider("Item Index", ItemIndex, 0, control.MiscList.Count - 1);
		
		EditorGUILayout.BeginHorizontal();
		
		if(GUILayout.Button("Add Item"))
		{
			var newMisc = new Misc();
			newMisc.Type = ItemType.Misc;
			control.MiscList.Add(newMisc);
			ItemIndex = control.MiscList.Count - 1;
		}
		
		if(GUILayout.Button("Remove Item"))
		{
			control.MiscList.RemoveAt(ItemIndex);
			ItemIndex = control.MiscList.Count - 1;
		}
		
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
		
		CommonItemProps(control.MiscList[ItemIndex]);
	}

	void DrawConsumableList(ItemDataBase control)
	{
		ItemIndex = EditorGUILayout.IntSlider("Item Index", ItemIndex, 0, control.ConsumableList.Count - 1);
		
		EditorGUILayout.BeginHorizontal();
		
		if(GUILayout.Button("Add Item"))
		{
			var newConsume = new Consumable();
			newConsume.Type = ItemType.Consumable;
			control.ConsumableList.Add(newConsume);
			ItemIndex = control.ConsumableList.Count - 1;
		}
		
		if(GUILayout.Button("Remove Item"))
		{
			control.ConsumableList.RemoveAt(ItemIndex);
			ItemIndex = control.ConsumableList.Count - 1;
		}
		
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
		
		CommonItemProps(control.ConsumableList[ItemIndex]);
	}

	void DrawQuestList(ItemDataBase control)
	{
		ItemIndex = EditorGUILayout.IntSlider("Item Index", ItemIndex, 0, control.QuestList.Count - 1);
		
		EditorGUILayout.BeginHorizontal();
		
		if(GUILayout.Button("Add Item"))
		{
			var newQuest = new Quest();
			newQuest.Type = ItemType.Quest;
			control.QuestList.Add(newQuest);
			ItemIndex = control.QuestList.Count - 1;
		}
		
		if(GUILayout.Button("Remove Item"))
		{
			control.QuestList.RemoveAt(ItemIndex);
			ItemIndex = control.QuestList.Count - 1;
		}
		
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
		
		CommonItemProps(control.QuestList[ItemIndex]);
	}

	void DrawGeneratorList(ItemDataBase control)
	{
		ItemIndex = EditorGUILayout.IntSlider("Item Index", ItemIndex, 0, control.GeneratorList.Count - 1);
		
		EditorGUILayout.BeginHorizontal();
		
		if(GUILayout.Button("Add Item"))
		{
			var newGen = new Generator();
			newGen.Type = ItemType.Generator;
			control.GeneratorList.Add(newGen);
			ItemIndex = control.GeneratorList.Count - 1;
		}
		
		if(GUILayout.Button("Remove Item"))
		{
			control.GeneratorList.RemoveAt(ItemIndex);
			ItemIndex = control.GeneratorList.Count - 1;
		}
		
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
		
		CommonItemProps(control.GeneratorList[ItemIndex]);
	}

	void DrawEnhancerList(ItemDataBase control)
	{
		ItemIndex = EditorGUILayout.IntSlider("Item Index", ItemIndex, 0, control.EnhancerList.Count - 1);
		
		EditorGUILayout.BeginHorizontal();
		
		if(GUILayout.Button("Add Item"))
		{
			var newEnhancer = new Enhancer();
			newEnhancer.Type = ItemType.Enhancer;
			control.EnhancerList.Add(newEnhancer);
			ItemIndex = control.EnhancerList.Count - 1;
		}
		
		if(GUILayout.Button("Remove Item"))
		{
			control.EnhancerList.RemoveAt(ItemIndex);
			ItemIndex = control.EnhancerList.Count - 1;
		}
		
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
		
		CommonItemProps(control.EnhancerList[ItemIndex]);
	}

}
