using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
	[SerializeField] GameObject CardPrefab;
	[SerializeField] GameObject cardObject;

    public void SpawnCard(Component sender, object data)
	{
		if (cardObject == null)
			cardObject = PrefabUtility.InstantiatePrefab(CardPrefab) as GameObject;

		if (cardObject.TryGetComponent(out CardConstructor cardConstructor))
		{
			string cardName = (string)data;
			cardConstructor.ChangeCard(cardName);
		}
		else
		{
			Debug.LogWarning("No card constructor found!");
		}
	}
}
