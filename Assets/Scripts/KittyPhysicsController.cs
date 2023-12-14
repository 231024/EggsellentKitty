using System;
using UnityEngine;

public class KittyPhysicsController : MonoBehaviour
{
	[SerializeField] private Rigidbody2D kittyBody;
	[SerializeField] private KittyInputController input;
	[SerializeField] private GameConfig config;

	public int Direction => Math.Sign(input.HorizontalAxisValue);

	public event Action<DropType, GameObject> OnCollect;

	private void FixedUpdate()
	{
		kittyBody.velocity = new Vector2(input.HorizontalAxisValue * config.kittySpeed, kittyBody.velocity.y);
	}
	
	private void OnCollisionEnter2D(Collision2D other)
	{
		var dropItem = other.gameObject.GetComponent<DropItem>();
		if (dropItem == null)
			return;

		Debug.Log($"Kitty's collected item {dropItem.Type}");
		dropItem.Collect();
		OnCollect?.Invoke(dropItem.Type, other.gameObject);
	}
}