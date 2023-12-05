using System;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour
{
	private enum BirdType
	{
		None = 0,
		Static = 1,
		Flying = 2
	}

	private Transform _transform;
	private int _flightDelay;
	private int _flightSpeed;
	private int _direction;
	private BirdType _type;
	private bool _isFlying;

	private void Start()
	{
		_transform = GetComponent<Transform>();
	}

	public void InitStatic()
	{
		_type = BirdType.Static;
		gameObject.AddComponent<Rigidbody2D>();
	}

	public void InitFlying(int delay, int speed, int direction)
	{
		_type = BirdType.Flying;
		_flightDelay = delay;
		_flightSpeed = speed;
		_direction = direction;

		Invoke(nameof(StartFly), _flightDelay);
	}

	private void StartFly() => _isFlying = true;

	private void Update()
	{
		if (_type != BirdType.Flying || _isFlying)
			return;

		_transform.position += new Vector3(0.004f, 0);
	}

	private void OnCollisionEnter(Collision collision)
	{
		// todo change direction
		// todo stop flying and reschedule new one
		
	}
}