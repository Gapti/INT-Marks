using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;



public class ItemDataBase : MonoBehaviour {

	public List<Weapon> WeaponList;
	public List<Armor> ArmorList;
	public List<Misc> MiscList;
	public List<Consumable> ConsumableList;
	public List<Quest> QuestList;
	public List<Generator> GeneratorList;
	public List<Enhancer> EnhancerList;
	

	public Item Get(ItemType itemType, int index)
	{
		switch(itemType)
		{
		case ItemType.Weapon:
			return WeaponList[index].Clone();
		case ItemType.Armor:
			return ArmorList[index].Clone();;
		case ItemType.Misc:
			return MiscList[index].Clone();
		case ItemType.Consumable:
			return ConsumableList[index].Clone();
		case ItemType.Quest:
			return QuestList[index].Clone();
		case ItemType.Enhancer:
			return EnhancerList[index].Clone();
		case ItemType.Generator:
			return GeneratorList[index].Clone ();
		default:
			return null;
		}

	}
	
	public void Save(string path, ItemType itemType)
	{

		TextWriter WriteFileStream = new StreamWriter(path);
		XmlSerializer serializer;

		switch(itemType)
		{
		case ItemType.Armor:

			serializer = new XmlSerializer(typeof(List<Armor>));
			serializer.Serialize(WriteFileStream,ArmorList);
			WriteFileStream.Close ();

			break;
		case ItemType.Weapon:

			serializer = new XmlSerializer(typeof(List<Weapon>));
			serializer.Serialize(WriteFileStream,WeaponList);
			WriteFileStream.Close ();

			break;

		case ItemType.Misc:

			serializer = new XmlSerializer(typeof(List<Misc>));
			serializer.Serialize(WriteFileStream, MiscList);
			WriteFileStream.Close();

			break;

		case ItemType.Consumable:
			serializer = new XmlSerializer(typeof(List<Consumable>));
			serializer.Serialize(WriteFileStream, ConsumableList);
			WriteFileStream.Close();
			
			break;
		case ItemType.Quest:
			serializer = new XmlSerializer(typeof(List<Quest>));
			serializer.Serialize(WriteFileStream, QuestList);
			WriteFileStream.Close();
			
			break;
		case ItemType.Enhancer:
			serializer = new XmlSerializer(typeof(List<Enhancer>));
			serializer.Serialize(WriteFileStream, EnhancerList);
			WriteFileStream.Close();
			
			break;

		case ItemType.Generator:
			serializer = new XmlSerializer(typeof(List<Generator>));
			serializer.Serialize(WriteFileStream, GeneratorList);
			WriteFileStream.Close();
		
		break;
		}
	}

	public void LoadXML(string path, ItemType itemType)
	{
		XmlSerializer serializer;

		switch(itemType)
		{
		case ItemType.Weapon:
			
			serializer = new XmlSerializer(typeof(List<Weapon>));
			using(var stream = new FileStream(path, FileMode.Open))
			{
				WeaponList = serializer.Deserialize(stream) as List<Weapon>;
			}
			break;
		case ItemType.Armor:
			
			serializer = new XmlSerializer(typeof(List<Armor>));
			using(var stream = new FileStream(path, FileMode.Open))
			{
				ArmorList = serializer.Deserialize(stream) as List<Armor>;
			}
			break;
		case ItemType.Misc:
			
			serializer = new XmlSerializer(typeof(List<Misc>));
			using(var stream = new FileStream(path, FileMode.Open))
			{
				MiscList = serializer.Deserialize(stream) as List<Misc>;
			}
			break;
		case ItemType.Consumable:
			
			serializer = new XmlSerializer(typeof(List<Consumable>));
			using(var stream = new FileStream(path, FileMode.Open))
			{
				ConsumableList = serializer.Deserialize(stream) as List<Consumable>;
			}
			break;
		case ItemType.Quest:

			serializer = new XmlSerializer(typeof(List<Quest>));
			using(var stream = new FileStream(path, FileMode.Open))
			{
				QuestList = serializer.Deserialize(stream) as List<Quest>;
			}
			break;
		case ItemType.Enhancer:
				
			serializer = new XmlSerializer(typeof(List<Enhancer>));
			using(var stream = new FileStream(path, FileMode.Open))
			{
				EnhancerList = serializer.Deserialize(stream) as List<Enhancer>;
			}
			break;
			case ItemType.Generator:
				
				serializer = new XmlSerializer(typeof(List<Generator>));
				using(var stream = new FileStream(path, FileMode.Open))
				{
					GeneratorList = serializer.Deserialize(stream) as List<Generator>;
				}
				break;
		}
	}

	public int NumberOfItems(ItemType itemType)
	{
		switch(itemType)
		{
		case ItemType.Weapon:
			return WeaponList.Count;
		case ItemType.Armor:
			return ArmorList.Count;
		case ItemType.Misc:
			return MiscList.Count;
		case ItemType.Consumable:
			return ConsumableList.Count;
		case ItemType.Quest:
			return QuestList.Count;
		case ItemType.Enhancer:
			return EnhancerList.Count;
		case ItemType.Generator:
			return GeneratorList.Count;
		default:
			return 0;
		}
	}
}
