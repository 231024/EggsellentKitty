using System;
using UnityEngine;

public class KittyPhysicsController : MonoBehaviour
{
	[SerializeField] private Rigidbody2D kittyBody;
	[SerializeField] private KittyInputController input;
	[SerializeField] private float speed;

	public int Direction => Math.Sign(input.HorizontalAxisValue);

	private void FixedUpdate()
	{
		kittyBody.velocity = new Vector2(input.HorizontalAxisValue * speed, kittyBody.velocity.y);
	}
}