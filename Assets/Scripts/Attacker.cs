using UnityEngine;
using UnityEngine.Events;

public class Attacker : MonoBehaviour
{
	[SerializeField] private GameConfig config;
	[SerializeField] private Rigidbody2D body;
	[SerializeField] private KittyInputController input;
	[SerializeField] private Animation hit;

	public UnityAction OnHit;
	public UnityAction OnFinishAttack;
	private bool _isInAttack;

	private const string FloorTag = "Floor";

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag(FloorTag))
		{
			Debug.Log("finish attack");

			_isInAttack = false;
			OnFinishAttack?.Invoke();
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