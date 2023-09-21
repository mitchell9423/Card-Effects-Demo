using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Playables;

public enum ActionEffect
{
	Attack,
	Heal,
	Shield,
	Speed
}

public class CardConstructor : MonoBehaviour
{
	[SerializeField] GameEvent changeCard;
	string cardName;
	[SerializeField] Vector3 rotation = Vector3.zero;
	[SerializeField] Vector3 scale = Vector3.zero;
	[SerializeField] List<ParticleSystem> particleSystems = new List<ParticleSystem>();

	public void ChangeCard(string spriteName)
	{
		changeCard?.Invoke(this, PowerBarState.Paused);
		cardName = spriteName;
		SetSprite(spriteName);
		particleSystems.ForEach(p => p.Play());
	}

	private void SetSprite(string spriteName)
	{
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

	public void TriggerActionEffects(Component sender, object data)
	{
		if (sender.gameObject == gameObject)
		{
			if (Int32.Parse(cardName) <= 13)
			{
				StartCoroutine(PlayActionEffect(ActionEffect.Attack));
			}
			else if (Int32.Parse(cardName) <= 33)
			{
				StartCoroutine(PlayActionEffect(ActionEffect.Shield));
			}
			else if (Int32.Parse(cardName) <= 43)
			{
				StartCoroutine(PlayActionEffect(ActionEffect.Speed));
			}
			else
			{
				StartCoroutine(PlayActionEffect(ActionEffect.Heal));
			}
		}
	}

	IEnumerator PlayActionEffect(ActionEffect actionEffect)
	{
		ParticleSystem.MinMaxGradient actionColorGradient = GameData.instance.GetActionGradient(actionEffect);

		ParticleSystem particleSystem = particleSystems[0];

		ParticleSystem.MainModule main = particleSystem.main;
		ParticleSystem.MinMaxGradient startColor = main.startColor;

		float delay = main.startDelayMultiplier;
		main.startDelayMultiplier = 0.0f;
		main.startColor = GameData.instance.GetActionStartGradient();

		ParticleSystem.ColorOverLifetimeModule colt = particleSystem.colorOverLifetime;
		ParticleSystem.MinMaxGradient colorGradient = colt.color;
		colt.color = actionColorGradient;

		particleSystem.Play();

		yield return new WaitUntil(() => particleSystem.isPlaying == false);

		main.startColor = startColor;
		main.startDelayMultiplier = delay;
		colt.color = colorGradient;
	}

	public void AsignToSlot(Transform slotTransform)
	{
		transform.SetParent(slotTransform, false);
		transform.localEulerAngles = rotation;
		transform.localScale = scale;
	}
}
