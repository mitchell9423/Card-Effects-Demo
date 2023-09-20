using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField] GameEvent spawnCardEvent;

	[SerializeField] TMP_InputField cardNameInput;

	public void SpawnCard()
	{
		spawnCardEvent.Invoke(this, cardNameInput.text);
	}
}
