using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public UISlider HealthBarGUI;
	public UISlider EnergyBarGUI;

	private float _healthAmount = 40;
	private float _energyAmount = 100;

	void OnEnable()
	{
		INTEvents.GiveEnergy += AddEnergy;
		INTEvents.TakeEnergy += TakeEnergy;
		INTEvents.GiveHealth += AddHealth;
		INTEvents.TakeHealth += TakeHealth;
	}

	void OnDisable()
	{
		INTEvents.GiveEnergy -= AddEnergy;
		INTEvents.TakeEnergy -= TakeEnergy;
		INTEvents.GiveHealth -= AddHealth;
		INTEvents.TakeHealth -= TakeHealth;
	}

	public void AddHealth(float health)
	{

		_healthAmount =(_healthAmount + health) > 100 ? 100 : _healthAmount + health;

		HealthBarGUI.value = (_healthAmount / 100);
		print (_healthAmount);
	}

	public void TakeHealth(float health)
	{
		_healthAmount =(_healthAmount - health) < 0 ? 0 : _healthAmount - health;

		HealthBarGUI.value = (_healthAmount / 100);
	}

	public float GetHealthAmount
	{
		get
		{
			return _healthAmount;
		}
	}

	public void AddEnergy(float energy)
	{
		_energyAmount =(_energyAmount + energy) > 100 ? 100 : _energyAmount + energy;

		EnergyBarGUI.value = (_energyAmount / 100);
	}

	public void TakeEnergy(float energy)
	{
		_energyAmount =(_energyAmount - energy) < 0 ? 0 : _energyAmount - energy;

		EnergyBarGUI.value = (_energyAmount / 100);
	}
}
