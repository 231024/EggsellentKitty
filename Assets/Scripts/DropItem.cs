using UnityEngine;

public class DropItem : MonoBehaviour
{
	public DropType Type {get; private set; }
	private float _timeToDestroy;

	private void OnDisable() => CancelInvoke();

	public void Init(DropType dropType, float timeToDestroy) =>
		(Type, _timeToDestroy) = (dropType, timeToDestroy);

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag(Constants.FloorTag))
		{
			Debug.Log($"Item {Type} is on the floor, start countdown");
			Invoke(nameof(SelfDestroy), _timeToDestroy);
		}
	}

	private void SelfDestroy() => Destroy(gameObject);
}