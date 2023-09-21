using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerBarState
{
	Paused,
	Running
}

public class PowerBar : MonoBehaviour
{
	public PowerBarState State { get; set; } = PowerBarState.Paused;

	[SerializeField] GameEvent actionEvent;

	[SerializeField] float elapsedTime;
	[SerializeField] float timeInterval;

	[SerializeField] Material m_Material;
	[SerializeField] Vector2 initialTiling = new Vector2(.5f, 0f);
	[SerializeField] Vector2 initialOffset = Vector2.zero;

	void Start()
    {
		GetRandomInterval();
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

	void GetRandomInterval()
	{
		timeInterval = Random.Range(3f, 8f);
		elapsedTime = 0f;
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
		actionEvent?.Invoke(this, "Attack");
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
			GetRandomInterval();
		}
	}
}
