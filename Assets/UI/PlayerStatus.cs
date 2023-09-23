using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
	[SerializeField] HealthBar healthBar;
	[SerializeField] ManaBar manaBar;

	[SerializeField] int health;
	[SerializeField] int maxHealth;

	[SerializeField] int mana;
	[SerializeField] int maxMana;

	public float HealthPercent { get => (float)health / maxHealth; }
	public float ManaPercent { get => (float)mana / maxMana; }

	private void Awake()
	{
		maxHealth = 100;
		health = maxHealth;

		maxMana = 100;
		mana = maxMana;
	}

	void Start()
    {
		healthBar = GetComponentInChildren<HealthBar>();
		manaBar = GetComponentInChildren<ManaBar>();

		UpdateStatusBars();
	}

	public void ValidateAction(Component sender, object data)
	{
		if (sender is PowerBar)
		{
			switch (((CardAction)data).actionEffect)
			{
				case ActionEffect.Attack:
					UpdateHealth(((CardAction)data).changeValue * -1);
					break;
				case ActionEffect.Heal:
					UpdateHealth(((CardAction)data).changeValue);
					break;
				case ActionEffect.Mana:
					UpdateMana(((CardAction)data).changeValue);
					break;
				case ActionEffect.Shield:
					break;
				case ActionEffect.Spawn:
					break;
				case ActionEffect.Speed:
					break;
				default:
					break;
			}

			UpdateStatusBars();
		}
	}

	void UpdateHealth(int val)
	{
		if (val > 0)
		{
			if (health >= maxHealth)
				return;

			val = UpdateMana(val * -1) * -1;
		}

		health += val;
		health = Mathf.Clamp(health, 0, maxHealth);
	}

	int UpdateMana(int val)
	{
		int manaDiff = 0;

		if (val < 0 && mana > 0)
		{
			manaDiff = -Mathf.Min(Mathf.Abs(val), mana);
		}
		else if (mana < maxMana)
		{
			manaDiff = Mathf.Min(val, maxMana - mana);
		}

		mana = Mathf.Clamp(mana + manaDiff, 0, maxMana);

		return manaDiff;
	}

	void UpdateStatusBars()
	{
		healthBar.SetTargetValue(HealthPercent);
		manaBar.SetTargetValue(ManaPercent);
	}
}
