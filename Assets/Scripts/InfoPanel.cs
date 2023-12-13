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

		RefreshScore();
		gameController.OnKityStateChanged += Refresh;
	}

	private void OnDestroy() => gameController.OnKityStateChanged -= Refresh;

	private void Refresh()
	{
		RefreshScore();
		RefreshLives();
	}

	private void RefreshScore()
	{
		eggs.SetValue(gameController.TotalEggs);
		superEggs.SetValue(gameController.TotalSuperEggs);
		score.SetScore(gameController.Score);
	}

	private void RefreshLives()
	{
		var current = gameController.CurrentLives;

		for (int i = 0, length = _lives.Count; i < length; i++)
			_lives[i].SetState(i < current);
	}
}