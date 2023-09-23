using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum CardType
{
	Attack,
	Heal,
	Mana,
	Shield,
	Speed
}

public enum PowerBarState
{
	Paused,
	Running
}

public enum ActionEffect
{
	Attack,
	Heal,
	Mana,
	Shield,
	Spawn,
	Speed
}

public class GameData : MonoBehaviour
{
	[SerializeField] Data settings;

	public static GameData instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public ParticleSystem.MinMaxGradient GetActionGradient(ActionEffect actionEffect)
	{
		switch (actionEffect)
		{
			case ActionEffect.Attack:
				{
					return settings.attackColorGradient;
				}
			case ActionEffect.Heal:
				{
					return settings.healColorGradient;
				}
			case ActionEffect.Shield:
				{
					return settings.shieldColorGradient;
				}
			case ActionEffect.Speed:
				{
					return settings.speedColorGradient;
				}
			default:
				{
					return settings.spawnColorGradient;
				}

		}
	}

	public CardType GetCardType(string name)
	{
		var id = Int32.Parse(name);

		if (id == 29 || id == 30 || id == 16 || id == 17)
		{
			return CardType.Mana;
		}

		if (id <= 13)
		{
			return CardType.Attack;
		}
		else if (id <= 33)
		{
			return CardType.Shield;
		}
		else if (id <= 36)
		{
			return CardType.Speed;
		}
		else if (id <= 43)
		{
			return CardType.Mana;
		}
		else
		{
			return CardType.Heal;
		}
	}
}
