using UnityEngine;

public class DropItem : MonoBehaviour
{
	private float _timeToDestroy;
	private Rigidbody2D _rigidbody;
	private Collider2D _collider;

	private void Awake()
	{
		_collider = GetComponent<Collider2D>();
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	public DropType Type { get; private set; }
	public string Source { get; private set; }

	private void OnDisable() => CancelInvoke();

	public void Init(DropType dropType, string source, float timeToDestroy) =>
		(Type, Source, _timeToDestroy) = (dropType, source, timeToDestroy);

	public void Collect()
	{
		Destroy(_rigidbody);
		Destroy(_collider);
	}

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