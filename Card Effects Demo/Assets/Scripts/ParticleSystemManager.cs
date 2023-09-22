using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class ParticleSystemManager : MonoBehaviour
{
	public Vector3 shapeScale;
	public float startSizeConstant;

	public MainModule main;
	public ShapeModule shapeModule;
	public ColorOverLifetimeModule colm;
	public SizeOverLifetimeModule solm;

	ParticleSystem ParticleSystem { get; set; }
	public bool IsPlaying { get => ParticleSystem.isPlaying; }
	public float StartDelay { get => main.startDelayMultiplier; set => main.startDelayMultiplier = value; }
	public Color StartColor
	{
		get => main.startColor.color;
		set
		{
			MinMaxGradient minMax = main.startColor;
			minMax.color = value;
			main.startColor = minMax;
		}
	}
	public MinMaxGradient ColorOverLife
	{
		get => colm.color;
		set
		{
			Gradient gradient = colm.color.gradient;
			GradientColorKey[] colorKeys = gradient.colorKeys;
			colorKeys[0].color = value.colorMin;
			colorKeys[1].color = value.colorMax;
			gradient.SetKeys(colorKeys, gradient.alphaKeys);
			colm.color = gradient;
		}
	}
	public MinMaxCurve StartSize { get => main.startSize; set => main.startSize = value; }
	public MinMaxCurve SizeOverLife { get => solm.size; set => solm.size = value; }

	private void Awake()
	{
		ParticleSystem = GetComponent<ParticleSystem>();
		main = ParticleSystem.main;
		main.loop = false;
		shapeModule = ParticleSystem.shape;
		colm = ParticleSystem.colorOverLifetime;
		solm = ParticleSystem.sizeOverLifetime;
		GetStartingValues();
	}

	void GetStartingValues()
	{
		startSizeConstant = StartSize.constant;
		shapeScale = shapeModule.scale;
	}

	public void Play() => ParticleSystem.Play();

	public void ReScale()
	{
		Transform nextParent = transform.parent;
		Vector3 scale = transform.localScale;

		while (nextParent != null)
		{
			scale = Vector3.Scale(nextParent.localScale, scale);
			nextParent = nextParent.parent;
		}

		shapeModule = ParticleSystem.shape;
		shapeModule.scale = Vector3.Scale(scale, shapeScale);

		MinMaxCurve size = new MinMaxCurve(startSizeConstant * scale.x);
		StartSize = size;
	}
}
