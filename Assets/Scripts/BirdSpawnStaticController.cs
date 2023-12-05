using UnityEngine;

public class BirdSpawnStaticController : BirdSpawnBaseController<BirdStaticBehaviour>
{
	protected override int BirdsCount => config.staticBirdsCount;

	protected override void InitBird(BirdStaticBehaviour bird, GameObject spawnPoint)
	{
		// nothing
	}
}