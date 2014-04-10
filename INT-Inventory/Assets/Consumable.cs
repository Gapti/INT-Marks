using UnityEngine;
using System.Collections;

[System.Serializable]
public class Consumable : Item {

	public override Item Clone ()
	{
		Consumable item = new Consumable();
		base.CloneBase(item);
		
		//copy all vars before return
		return item;
	}
}
