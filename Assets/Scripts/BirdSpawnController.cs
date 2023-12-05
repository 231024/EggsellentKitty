using UnityEngine;
using Random = UnityEngine.Random;

public abstract class BirdSpawnBaseController<T> : MonoBehaviour where T : BirdBaseBehaviour
{
	[SerializeField] private GameObject[] birds;
	[SerializeField] private GameObject[] spawnPoints;
	[SerializeField] protected GameConfig config;

	protected abstract int BirdsCount { get; }

	private void Start()
	{
		SpawnBirds();
	}

	private void SpawnBirds()
	{
		if (birds == null || spawnPoints == null)
			return;

		for (var i = 0; i < BirdsCount; i++)
		{
			var spawnPoint = spawnPoints[i];
			var bird = SpawnRandomBird(spawnPoint.transform.position);
			var birdBehaviour = bird.GetComponent<T>();
			InitBird(birdBehaviour, spawnPoint);
		}
	}

	private GameObject SpawnRandomBird(Vector3 position)
	{
		var bird = Instantiate(birds[Random.Range(0, BirdsCount)]);
		bird.transform.position = position;
		bird.gameObject.SetActive(true);

		return bird;
	}

	protected abstract void InitBird(T bird, GameObject spawnPoint);
}