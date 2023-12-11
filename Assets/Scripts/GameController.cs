using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField] private GameConfig config;
	[SerializeField] private KittyPhysicsController kitty;

	public int CurrentLives { get; private set; }
	public int Score { get; private set; }
	public int TotalEggs { get; private set; }
	public int TotalSuperEggs { get; private set; }

	public event Action OnChanged;

	private void Awake()
	{
		CurrentLives = config.livesCount;
		Score = 0;
		TotalEggs = 0;
		TotalSuperEggs = 0;

		kitty.OnCollect += OnDropCollected;
	}

	private void OnDestroy() => kitty.OnCollect -= OnDropCollected;

	public void OnDropCollected(DropType dropType)
	{
		switch (dropType)
		{
			case DropType.Egg:
				CollectEgg();
				break;
			case DropType.SuperEgg:
				CollectSuperEgg();
				break;
			case DropType.Shit:
				CollectShit();
				break;
			case DropType.ExtraLife:
				CollectExtraLife();
				break;
			default: return;
		}

		OnChanged?.Invoke();
	}

	private void CollectEgg()
	{
		TotalEggs++;
		Score += config.eggPoints;
	}

	private void CollectSuperEgg()
	{
		TotalSuperEggs++;
		Score += config.superEggPoints;
	}

	private void CollectShit()
	{
		// todo implement game over
		if (CurrentLives <= 0)
			return;

		CurrentLives--;
	}

	private void CollectExtraLife()
	{
		if (HasFullLives)
			return;

		CurrentLives++;
	}

	private bool HasFullLives => CurrentLives >= config.livesCount;
}
