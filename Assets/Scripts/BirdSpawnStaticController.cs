public class BirdSpawnStaticController : BirdSpawnBaseController<BirdStaticBehaviour>
{
	protected override int BirdsCount => config.staticBirdsCount;
}