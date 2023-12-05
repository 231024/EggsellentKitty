using UnityEngine;
using Random = UnityEngine.Random;

public class BirdSpawnController : MonoBehaviour
{
	[SerializeField] private GameObject[] birds;
	[SerializeField] private GameObject[] spawnPoints;
	[SerializeField] private GameObject[] spawnPointsFlying;
	[SerializeField] private GameConfig config;

	private int _birdsCount;

	private void Start()
	{
		_birdsCount = birds?.Length ?? 0;

		SpawnStaticBirds();
		SpawnFlyingBirds();
	}

	private void SpawnStaticBirds()
	{
		var spawnPointsCount = spawnPoints?.Length ?? 0;

		if (birds == null || spawnPoints == null)
			return;

		for (var i = 0; i < spawnPointsCount; i++)
		{
			var bird = SpawnRandomBird(spawnPoints[i].transform.position);
			var birdBehaviour = bird.GetComponent<BirdBehaviour>();
			birdBehaviour.InitStatic();
		}
	}

	private void SpawnFlyingBirds()
	{
		if (spawnPointsFlying == null)
			return;
		
		for (var i = 0; i < config.flyingBirdsCount; i++)
		{
			var position = spawnPointsFlying[Random.Range(0, spawnPointsFlying.Length)];
			var bird = SpawnRandomBird(position.transform.position);
			var birdBehaviour = bird.GetComponent<BirdBehaviour>();
			if (birdBehaviour == null)
				continue;

			var spawnPoint = position.GetComponent<FlyingSpawnPoint>();
			birdBehaviour.InitFlying(config.RandomFlyingDelay, config.birdSpeed, spawnPoint.Direction);
		}
	}

	private GameObject SpawnRandomBird(Vector3 position)
	{
		var bird = Instantiate(birds[Random.Range(0, _birdsCount)]);
		bird.transform.position = position;
		bird.gameObject.SetActive(true);

		return bird;
	}
}