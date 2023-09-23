using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data", fileName = "New Game Data")]
public class Data : ScriptableObject
{
	public ParticleSystem.MinMaxGradient spawnColorGradient = new ParticleSystem.MinMaxGradient(new Color(0.1764706f, 0.1372549f, 1f, 1f), new Color(0.9528302f, 0.05946039f, 0.04194841f, 1f));

	public ParticleSystem.MinMaxGradient attackColorGradient = new ParticleSystem.MinMaxGradient(Color.red, Color.red);
	public ParticleSystem.MinMaxGradient healColorGradient = new ParticleSystem.MinMaxGradient(Color.green, Color.green);
	public ParticleSystem.MinMaxGradient shieldColorGradient = new ParticleSystem.MinMaxGradient(Color.yellow, Color.yellow);
	public ParticleSystem.MinMaxGradient speedColorGradient = new ParticleSystem.MinMaxGradient(Color.cyan, Color.cyan);
}
