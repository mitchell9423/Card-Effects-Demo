using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBar : MonoBehaviour
{
	public PowerBarState State { get; set; } = PowerBarState.Paused;

	[SerializeField] GameEvent actionEvent;

	[SerializeField] int strength;
	[SerializeField] float elapsedTime;
	[SerializeField] float timeInterval;

	[SerializeField] Material m_Material;
	[SerializeField] Vector2 initialTiling = new Vector2(.5f, 0f);
	[SerializeField] Vector2 initialOffset = Vector2.zero;

	void Start()
    {
		GetRandomData();
		GetMaterial();
		UpdateTextureOffset(0f);
		StartTimer();
	}

	private void Update()
	{
		if (State != PowerBarState.Paused)
		{
			elapsedTime += Time.deltaTime;
			if (elapsedTime > timeInterval)
			{
				elapsedTime = 0.0f;
				TriggerAction();
			}

			UpdateTextureOffset(elapsedTime / timeInterval);
		}
	}

	void GetRandomData()
	{
		elapsedTime = 0f;
		timeInterval = UnityEngine.Random.Range(3f, 8f);
		strength = UnityEngine.Random.Range(3, 15);
	}

	void GetMaterial()
	{
		if (TryGetComponent(out MeshRenderer mr))
		{
			m_Material = mr.materials[1];
			m_Material?.SetTextureScale("_MainTex", initialTiling);
			m_Material?.SetTextureOffset("_MainTex", initialOffset);
		}
	}

    public void UpdateTextureOffset(float percent)
	{
		if (m_Material == null)
		{
			GetMaterial();
		}
		else
		{
			m_Material.SetTextureOffset("_MainTex", new Vector2(.5f * percent, 0f));
		}
	}

	private void TriggerAction()
	{
		CardAction action;

		ActionEffect actionType = (ActionEffect)Enum.Parse(typeof(ActionEffect), GetComponent<CardConstructor>().CardType.ToString(), true);

		action = new CardAction(actionType, strength);

		actionEvent?.Invoke(this, action);
	}

	private void StartTimer()
	{
		if (State == PowerBarState.Paused)
			State = PowerBarState.Running;
	}

	public void ChangeCard(Component sender, object data)
	{
		if (sender.gameObject == gameObject)
		{
			GetRandomData();
		}
	}
}
