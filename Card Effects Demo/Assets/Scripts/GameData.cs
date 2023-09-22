using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

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
}
