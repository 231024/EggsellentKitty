using UnityEngine;

public class DropItem : MonoBehaviour
{
	private DropType _dropType;
	private float _timeToDestroy;

	private void OnDisable() => CancelInvoke();

	// todo implement
	public void Init(DropType dropType, float timeToDestroy) =>
		(_dropType, _timeToDestroy) = (dropType, timeToDestroy);

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.GetComponent<KittyInputController>())
		{
			Debug.Log($"Kitty's collected item {_dropType}");
			SelfDestroy();
		}

		if (other.gameObject.CompareTag(Constants.FloorTag))
		{
			Debug.Log($"Item {_dropType} is on the floor, start countdown");
			Invoke(nameof(SelfDestroy), _timeToDestroy);
		}
	}

	private void SelfDestroy() => Destroy(gameObject);
}