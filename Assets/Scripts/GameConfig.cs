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
	public int flightDurationMin;
	public int flightDurationMax;

	[Header("Drop Config")]
	public int eggPoints;
	public int superEggPoints;
	public int dropLifetime;
	public int dropDelay;
	public DropProbability[] probabilities;

	[Header("Main Settings")]
	public int scoreToWin;
	public int livesCount;

	public int RandomFlyingDelay => Random.Range(flyingPeriodMin, flyingPeriodMax);

	public int RandomFlightDuration => Random.Range(flightDurationMin, flightDurationMax);

	// todo implement
	public DropType RandomizeDrop()
	{
		return (DropType)Random.Range(1, 5);
	}

	[Serializable]
	public struct DropProbability
	{
		public DropType Type;
		public int Probability;
	}
}