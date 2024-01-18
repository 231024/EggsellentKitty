using System;
using UnityEngine;
using UnityEngine.Events;

public class Attacker : MonoBehaviour
{
	[SerializeField] private GameConfig config;
	[SerializeField] private Rigidbody2D body;
	[SerializeField] private KittyInputController input;
	[SerializeField] private Animation hit;

	public UnityAction OnHit;
	private const string FloorTag = "Floor";
	private bool _isInAttack;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag(FloorTag))
		{
			_isInAttack = false;
			Debug.Log("finish attack");
		}

		if (_isInAttack && other.gameObject.GetComponent<BirdFlyingBehaviour>())
		{
			Debug.Log("Hit bird");
			hit.Play();
			OnHit?.Invoke();
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag(FloorTag))
		{
			Debug.Log("start attack");
			_isInAttack = true;
		}
	}
}