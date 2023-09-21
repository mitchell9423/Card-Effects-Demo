using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
	[SerializeField] List<Slot> slots = new List<Slot>();
	[SerializeField] GameObject CardPrefab;
	[SerializeField] CardConstructor card;

	[SerializeField] int slotIndex = 0;

    public void SpawnCard(Component sender, object data)
	{
		Slot slot = slots[slotIndex % slots.Count];

		card = slot.Card;

		if (card == null)
		{
			GameObject cardObject = PrefabUtility.InstantiatePrefab(CardPrefab) as GameObject;

			if (cardObject.TryGetComponent(out card))
			{
				slot.RegisterCard(card);
			}
		}

		if (card != null)
		{
			string cardName = (string)data;
			card.ChangeCard(cardName);
			slotIndex = slotIndex < slots.Count - 1 ? slotIndex + 1 : 0;
		}
		else
		{
			Debug.LogWarning("No card constructor found!");
		}
	}
}
