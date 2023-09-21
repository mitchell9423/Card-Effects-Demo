using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Slot : MonoBehaviour
{
	CardConstructor card;

	public CardConstructor Card { get => card; }

	public void RegisterCard(CardConstructor cardConstructor)
	{
		card = cardConstructor;
		card.AsignToSlot(transform);
	}
}
