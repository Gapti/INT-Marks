using UnityEngine;
using System.Collections;

public class INTEvents : MonoBehaviour {

	public delegate void OnGiveHealth(float health);
	public static event OnGiveHealth GiveHealth;

	public delegate void OnTakeHealth(float health);
	public static event OnTakeHealth TakeHealth;

	public delegate void OnTakeEnergy(float energy);
	public static event OnTakeEnergy TakeEnergy;

	public delegate void OnGiveEnergy(float energy);
	public static event OnGiveEnergy GiveEnergy;
	

	public static void GivePlayerEnergy(float energy)
	{
		if(GiveEnergy != null)
			GiveEnergy(energy);
	}

	public static void TakePlayerEnergy(float energy)
	{
		if(TakeEnergy != null)
			TakeEnergy(energy);
	}

	public static void TakePlayerHealth(float health)
	{
		if(TakeHealth != null)
			TakeHealth(health);
	}

	public static void GivePlayerHealth(float health)
	{
		if(GiveHealth != null)
			GiveHealth(health);
	}
}
