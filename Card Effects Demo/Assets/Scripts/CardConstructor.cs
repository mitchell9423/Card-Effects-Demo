using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardConstructor : MonoBehaviour
{
	[SerializeField] ParticleSystem particleSystem;

	public void ChangeCard(string spriteName)
	{
		StartCoroutine(SetSprite(spriteName));

		particleSystem.Play();
	}

	IEnumerator SetSprite(string spriteName)
	{
		yield return new WaitForSeconds(2);

		if (transform.GetChild(0).TryGetComponent(out SpriteRenderer spriteRenderer))
		{
			string spritePath = $"Icons/Free-Fantasy-Items_{spriteName}";

			Sprite sprite = Resources.Load<Sprite>(spritePath);

			if (sprite != null)
			{
				spriteRenderer.sprite = sprite;
			}
			else
			{
				Debug.LogWarning($"No sprite found @ {spritePath}");
			}
		}
		else
		{
			Debug.LogWarning("No spriteRenderer found!");
		}
	}
}
