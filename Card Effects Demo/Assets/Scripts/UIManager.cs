using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField] GameEvent spawnCardEvent;

	[SerializeField] TMP_InputField cardNameInput;

	private void Start()
	{
		GetRandomCard();
	}

	void GetRandomCard()
	{
		cardNameInput.text = Random.Range(1, 51).ToString();
	}

	public void SpawnCard()
	{
		spawnCardEvent.Invoke(this, cardNameInput.text);
		GetRandomCard();
	}
}
