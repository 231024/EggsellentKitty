using System;
using UnityEngine;

public class DropItem : MonoBehaviour
{
	public DropType Type {get; private set; }
	private float _timeToDestroy;
	private Rigidbody2D _rigidbody;
	private Collider2D _collider;

	private void Awake()
	{
		_collider = GetComponent<Collider2D>();
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	private void OnDisable() => CancelInvoke();

	public void Init(DropType dropType, float timeToDestroy) =>
		(Type, _timeToDestroy) = (dropType, timeToDestroy);

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