using UnityEngine;
using System.Collections;

[System.Serializable]
public class Misc : Item {

	public override Item Clone ()
	{
		Misc item = new Misc();
		base.CloneBase(item);

		//copy all vars before return
		return item;
	}
}
