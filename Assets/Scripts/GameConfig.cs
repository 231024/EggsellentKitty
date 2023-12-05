using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
[CreateAssetMenu(menuName = "Game/Create Config", fileName = "GameConfig")]
public class GameConfig : ScriptableObject
{
	[Header("Kitty Config")]
	public float kittySpeed;

	[Header("Birds Config")]
	public int flyingBirdsCount;
	public int flyingPeriodMin;
	public int flyingPeriodMax;
	public int birdSpeed;

	public int RandomFlyingDelay => Random.Range(flyingPeriodMin, flyingPeriodMax);
}