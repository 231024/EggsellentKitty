using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropController : MonoBehaviour
{
	[SerializeField] private GameController gameController;
	[SerializeField] private Attacker attacker;
	[SerializeField] private GameConfig config;
	[SerializeField] private DropItemConfig dropItems;

	private readonly List<GameObject> _dropSources = new();
	private readonly List<GameObject> _droppedItems = new();
	private int _sourceIndex;

	private const DropType ShockingDropType = DropType.SuperEgg;

	private void Awake()
	{
		gameController.OnGameStateChanged += OnGameStateChanged;
		attacker.OnHit += ShockingDrop;
		ScheduleDrop();
	}

	private void OnDestroy()
	{
		CancelInvoke();
		gameController.OnGameStateChanged -= OnGameStateChanged;
		attacker.OnHit -= ShockingDrop;
	}

	public void Register(GameObject dropSource) => _dropSources.Add(dropSource);

	private void OnGameStateChanged()
	{
		if (gameController.InProgress)
			ScheduleDrop();
		else if (gameController.NotStarted)
		{
			CancelInvoke();
			_dropSources.Clear();

			foreach (var item in _droppedItems)
				Destroy(item);
		}
	}

	private void ScheduleDrop()
	{
		if (!gameController.InProgress)
			return;

		_sourceIndex = Random.Range(0, _dropSources.Count);
		Invoke(nameof(Drop), config.dropDelay);
	}

	private void Drop()
	{
		if (!gameController.InProgress)
			return;

		var dropType = config.RandomizeDrop();
		var drop = dropItems.GetDropByType(dropType);

		if (drop != null)
		{
			var dropItem = Instantiate(drop).GetComponent<DropItem>();
			var source = _dropSources[_sourceIndex];
			dropItem.transform.position = source.transform.position;
			dropItem.Init(dropType, source.name, GetDropLifetime(dropType));
			_droppedItems.Add(dropItem.gameObject);
		}

		ScheduleDrop();
	}

	private void ShockingDrop()
	{
		var drop = dropItems.GetDropByType(ShockingDropType);

		foreach (var source in _dropSources)
		{
			var dropItem = Instantiate(drop).GetComponent<DropItem>();
			dropItem.transform.position = source.transform.position;
			dropItem.Init(ShockingDropType, source.name, GetDropLifetime(ShockingDropType));
			_droppedItems.Add(dropItem.gameObject);
		}
	}

	private float GetDropLifetime(DropType dropType) =>
		dropType == DropType.Shit ? config.badDropLifetime : config.goodDropLifetime;
}