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
	public int staticBirdsCount;
	public int flyingBirdsCount;
	public int flyingPeriodMin;
	public int flyingPeriodMax;
	public float birdSpeed;

	public int RandomFlyingDelay => Random.Range(flyingPeriodMin, flyingPeriodMax);
}