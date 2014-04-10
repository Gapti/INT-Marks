using UnityEngine;
using System.Collections;

public enum ArmorType
{
	Helm,
	Chest,
	Legs,
	Hands,
	Feet
}

[System.Serializable]
public class Armor : Item {

	public ArmorType Fits;

	public float ArmorValue;
	public float DefenseValue;

	public override Item Clone ()
	{
		Armor item = new Armor();
		base.CloneBase(item);

		//copy all vars before return
		item.Fits = Fits;
		item.ArmorValue = ArmorValue;
		item.DefenseValue = DefenseValue;

		return item;
	}

	/// <summary>
	/// these 2 need recoding
	/// </summary>
	//public int Resistance;
	//public int Requirements;
}
