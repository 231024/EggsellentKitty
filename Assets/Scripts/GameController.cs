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
			if (_gameState == value)
				return;

			_gameState = value;
			OnGameStateChanged?.Invoke();
		}
	}

	public int TotalEggs { get; private set; }
	public int TotalSuperEggs { get; private set; }
	public bool HasAttack { get; private set; }

	public bool? Result { get; private set; }
	public bool InProgress => GameState == State.InProgress;
	public bool NotStarted => GameState == State.NotStarted;

	public event Action OnGameStateChanged;
	public event Action OnKittyStateChanged;

	private int _score;
	private State _gameState;

	private void Awake()
	{
		Init();
		kitty.OnCollect += OnDropCollected;
	}

	public void StartGame() => GameState = State.InProgress;

	public void RestartGame()
	{
		Init();
		Result = null;
		GameState = State.NotStarted;
	}

	private void Init()
	{
		CurrentLives = config.livesCount;
		Score = 0;
		TotalEggs = 0;
		TotalSuperEggs = 0;
	}

	private void OnDestroy() => kitty.OnCollect -= OnDropCollected;

	private void OnDropCollected(DropItem item)
	{
		if (!InProgress)
			return;

		switch (item.Type)
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
			case DropType.Attack:
				CollectAttack();
				break;
			default: return;
		}

		OnKittyStateChanged?.Invoke();
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
			return;

		CurrentLives--;
		if (CurrentLives == 0)
		{
			Result = false;
			GameState = State.Finished;
		}
	}

	private void CollectExtraLife()
	{
		if (HasFullLives)
			return;

		CurrentLives++;
	}
	
	private void CollectAttack()
	{
		if (HasAttack)
			return;

		HasAttack = true;
	}

	private void CheckScore()
	{
		if (Score < config.scoreToWin)
			return;

		Result = true;
		GameState = State.Finished;
	}

	private bool HasFullLives => CurrentLives >= config.livesCount;
}