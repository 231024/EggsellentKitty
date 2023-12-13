using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public enum State
	{
		NotStarted = 0,
		InProgress = 1,
		Finished = 2
	}

	[SerializeField] private GameConfig config;
	[SerializeField] private KittyPhysicsController kitty;

	public int CurrentLives { get; private set; }

	public int Score
	{
		get => _score;
		private set
		{
			_score = value;
			CheckScore();
		}
	}

	public State GameState
	{
		get => _gameState;
		private set
		{
			_gameState = value;
			OnGameStateChanged?.Invoke();
		}
	}

	public int TotalEggs { get; private set; }
	public int TotalSuperEggs { get; private set; }

	public bool? Result { get; private set; }
	public bool InProgress => GameState == State.InProgress;

	public event Action OnGameStateChanged;
	public event Action OnKityStateChanged;

	private int _score;
	private State _gameState;

	private void Awake()
	{
		CurrentLives = config.livesCount;
		Score = 0;
		TotalEggs = 0;
		TotalSuperEggs = 0;

		kitty.OnCollect += OnDropCollected;
		
	}

	private void OnDestroy() => kitty.OnCollect -= OnDropCollected;

	private void OnDropCollected(DropType dropType)
	{
		if (!InProgress)
			return;

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

		OnKityStateChanged?.Invoke();
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
		if (CurrentLives <= 0)
		{
			GameState = State.Finished;
			Result = false;
			return;
		}

		CurrentLives--;
	}

	private void CollectExtraLife()
	{
		if (HasFullLives)
			return;

		CurrentLives++;
	}

	private void CheckScore()
	{
		if (Score < config.scoreToWin)
			return;

		GameState = State.Finished;
		Result = true;
	}

	private bool HasFullLives => CurrentLives >= config.livesCount;
}