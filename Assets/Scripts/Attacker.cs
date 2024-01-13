using UnityEngine;

public class Attacker : MonoBehaviour
{
	[SerializeField] private GameConfig config;
	[SerializeField] private Rigidbody2D body;
	[SerializeField] private KittyInputController input;
	[SerializeField] private Animation hit;

	private void Awake() => input.Attack += OnAttack;

	private void OnDestroy() => input.Attack -= OnAttack;

	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.GetComponent<BirdFlyingBehaviour>())
		{
			Debug.LogError("Hit bird");
			hit.Play();
		}
	}

	private void OnAttack()
	{
		//body.velocity = new Vector2(body.velocity.x, config.kittyJumpHeight);
	}
}