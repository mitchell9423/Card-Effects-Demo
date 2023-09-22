using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Playables;
using static UnityEngine.ParticleSystem;

public enum ActionEffect
{
	Attack,
	Heal,
	Shield,
	Spawn,
	Speed
}

public class CardConstructor : MonoBehaviour
{
	[SerializeField] GameEvent changeCard;
	[SerializeField] string cardName;
	[SerializeField] Vector3 rotation = Vector3.zero;
	[SerializeField] Vector3 scale = Vector3.zero;
	[SerializeField] List<ParticleSystemManager> particleSystems = new List<ParticleSystemManager>();

	public void ChangeCard(string spriteName)
	{
		changeCard?.Invoke(this, PowerBarState.Paused);
		cardName = spriteName;
		SetSprite(spriteName);
		PlayActionEffect(ActionEffect.Spawn);
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
				PlayActionEffect(ActionEffect.Attack);
			}
			else if (Int32.Parse(cardName) <= 33)
			{
				PlayActionEffect(ActionEffect.Shield);
			}
			else if (Int32.Parse(cardName) <= 43)
			{
				PlayActionEffect(ActionEffect.Speed);
			}
			else
			{
				PlayActionEffect(ActionEffect.Heal);
			}
		}
	}

	void PlayActionEffect(ActionEffect actionEffect)
	{
		foreach (ParticleSystemManager particleSystem in particleSystems)
		{
			particleSystem.StartDelay = 0f;

			particleSystem.ColorOverLife = GameData.instance.GetActionGradient(actionEffect);

			particleSystem.Play();
		}
	}

	public void AsignToSlot(Transform slotTransform)
	{
		transform.SetParent(slotTransform, false);
		transform.localEulerAngles = rotation;
		transform.localScale = scale;

		particleSystems.ForEach(p => p.ReScale());
	}
}
