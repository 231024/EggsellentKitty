using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class BirdSpawnBaseController<T> : MonoBehaviour where T : BirdBaseBehaviour
{
	[SerializeField] private GameObject[] birds;
	[SerializeField] private GameObject[] spawnPoints;
	[SerializeField] protected GameConfig config;
	[SerializeField] protected DropController dropController;
	[SerializeField] protected EmotionsController emotionsController;
	[SerializeField] private GameController gameController;

	protected abstract int BirdsCount { get; }
	private List<GameObject> _spawnedBirds = new();
	private int _maxIndex;

	private void Start()
	{
		gameController.OnGameStateChanged += OnGameStateChanged;
		OnGameStateChanged();
	}

	private void OnDestroy()
	{
		gameController.OnGameStateChanged -= OnGameStateChanged;
	}

	private void OnGameStateChanged()
	{
		if (gameController.InProgress)
			SpawnBirds();
		else if (gameController.NotStarted)
			DestroyBirds();
	}

	private void SpawnBirds()
	{
		if (birds == null || spawnPoints == null)
			return;

		var spawnPointsCount = spawnPoints.Length;
		for (var i = 0; i < BirdsCount; i++)
		{
			var spawnPoint = spawnPoints[i % spawnPointsCount];
			var bird = SpawnRandomBird(spawnPoint.transform.position);
			bird.name += i;
			_spawnedBirds.Add(bird);
			var birdBehaviour = bird.GetComponent<T>();
			InitBird(birdBehaviour, spawnPoint);
		}
	}

	private GameObject SpawnRandomBird(Vector3 position)
	{
		_maxIndex = Math.Min(birds.Length, BirdsCount);
		var bird = Instantiate(birds[Random.Range(0, _maxIndex)]);
		bird.transform.position = position;
		bird.gameObject.SetActive(true);

		return bird;
	}

	protected virtual void InitBird(T bird, GameObject spawnPoint)
	{
		dropController.Register(bird.gameObject);
		emotionsController.RegisterBird(bird.gameObject);
	}

	private void DestroyBirds()
	{
		foreach (var bird in _spawnedBirds)
			Destroy(bird);

		_spawnedBirds.Clear();
	}
}