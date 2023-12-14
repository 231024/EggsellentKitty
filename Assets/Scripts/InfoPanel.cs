using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
	[SerializeField] private GameController gameController;
	[SerializeField] private GameConfig config;
	[SerializeField] private Transform livesContainer;
	[SerializeField] private GameObject lifePrefab;
	[SerializeField] private ValueItem eggs;
	[SerializeField] private ValueItem superEggs;
	[SerializeField] private ValueItem score;

	private readonly List<LifeItem> _lives = new();

	private void Awake()
	{
		for (var i = 0; i < config.livesCount; i++)
		{
			var life = Instantiate(lifePrefab, livesContainer).GetComponent<LifeItem>();
			life.SetState(true);
			_lives.Add(life);
		}

		RefreshStats();
	}

	public Transform GetEggsTransform => eggs.transform;

	public Transform GetSuperEggTransform => superEggs.transform;

	public Transform GetScoreTransform => score.transform;

	public Transform GetLiveToRestore => gameController.CurrentLives <= _lives.Count
		? _lives[gameController.CurrentLives - 1].transform
		: null;

	public Transform GetLiveToDestroy => gameController.CurrentLives >= 0
		? _lives[gameController.CurrentLives].transform
		: null;

	private void RefreshStats()
	{
		RefreshEggs();
		RefreshSuperEggs();
		SetScore();
	}

	public void RefreshEggs() => eggs.SetValue(gameController.TotalEggs);
	public void RefreshSuperEggs() => superEggs.SetValue(gameController.TotalSuperEggs);
	public void SetScore() => score.SetScore(gameController.Score);

	public void RefreshLives()
	{
		var current = gameController.CurrentLives;

		for (int i = 0, length = _lives.Count; i < length; i++)
			_lives[i].SetState(i < current);
	}
}