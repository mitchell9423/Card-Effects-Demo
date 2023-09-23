using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Data", fileName = "Player Data")]
public class PlayerData : ScriptableObject
{
	[SerializeField] int health;
	[SerializeField] int maxHealth;

	public int Health { get => health; set => health = value; }
	public int MaxHealth { get => maxHealth; set => maxHealth = value; }


}
