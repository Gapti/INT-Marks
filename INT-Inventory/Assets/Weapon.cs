using UnityEngine;
using System.Collections;


[System.Serializable]
public class Weapon : Item {

	public float MinDamage;
	public float MaxDamage;
	public float MinRange;
	public float MaxRange;
	public float CriticalStrike;
	public float BatteryRecharge;
	public float Exhaustion;
	//public AttackModifer;

	public override Item Clone ()
	{
		Weapon item = new Weapon();
		base.CloneBase(item);
		
		//copy all vars before return

		item.MinDamage = MinDamage;
		item.MaxDamage = MaxDamage;
		item.MinRange = MinRange;
		item.MaxRange = MaxRange;
		item.CriticalStrike = CriticalStrike;
		item.BatteryRecharge = BatteryRecharge;
		item.Exhaustion = Exhaustion;

		return item;
	}
	
}
