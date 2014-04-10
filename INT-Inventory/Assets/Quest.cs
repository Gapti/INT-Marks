using UnityEngine;
using System.Collections;

[System.Serializable]
public class Quest : Item {

	public override Item Clone ()
	{
		Quest item = new Quest();
		base.CloneBase(item);

		//copy all vars before return
		return item;
	}
}
