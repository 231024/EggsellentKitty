using UnityEngine;

public class BirdSpawnFlyingController : BirdSpawnBaseController<BirdFlyingBehaviour>
{
	protected override int BirdsCount => config.flyingBirdsCount;

	protected override void InitBird(BirdFlyingBehaviour bird, GameObject spawnPoint)
	{
		var direction = spawnPoint.GetComponent<FlyingSpawnPoint>().Direction;
		bird.Init(config.RandomFlyingDelay, config.birdSpeed, direction);
	}
}