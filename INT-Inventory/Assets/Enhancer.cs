using UnityEngine;
using System.Collections;

[System.Serializable]
public class Enhancer : Item {

	public override Item Clone ()
	{
		Enhancer item = new Enhancer();
		base.CloneBase(item);
		
		//copy all vars before return
		return item;
	}
}
