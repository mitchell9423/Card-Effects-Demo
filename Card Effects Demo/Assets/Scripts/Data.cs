using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data", fileName = "New Game Data")]
public class Data : ScriptableObject
{
	public ParticleSystem.MinMaxGradient actionStartColor = new ParticleSystem.MinMaxGradient(Color.white, Color.white);
	public ParticleSystem.MinMaxGradient attackColorGradient = new ParticleSystem.MinMaxGradient(Color.red, Color.red);
	public ParticleSystem.MinMaxGradient healColorGradient = new ParticleSystem.MinMaxGradient(Color.green, Color.green);
	public ParticleSystem.MinMaxGradient shieldColorGradient = new ParticleSystem.MinMaxGradient(Color.yellow, Color.yellow);
	public ParticleSystem.MinMaxGradient speedColorGradient = new ParticleSystem.MinMaxGradient(Color.cyan, Color.cyan);
}
