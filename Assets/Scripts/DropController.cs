using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropController : MonoBehaviour
{
	[SerializeField] private GameController gameController;
	[SerializeField] private GameConfig config;
	[SerializeField] private DropItemConfig dropItems;

	private readonly List<GameObject> _dropSources = new();
	private int _sourceIndex;

	private void Awake()
	{
		gameController.OnGameStateChanged += ScheduleDrop;
		ScheduleDrop();
	}

	private void OnDestroy()
	{
		CancelInvoke();
		gameController.OnGameStateChanged -= ScheduleDrop;
	}

	public void Register(GameObject dropSource) => _dropSources.Add(dropSource);

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
			dropItem.transform.position = _dropSources[_sourceIndex].transform.position;
			dropItem.Init(dropType, config.dropLifetime);
		}

		ScheduleDrop();
	}
}