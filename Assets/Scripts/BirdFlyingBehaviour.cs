using UnityEngine;

public class BirdFlyingBehaviour : BirdBaseBehaviour
{
	private Transform _transform;
	private int _flightDelay;
	private float _flightSpeed;
	private int _direction;
	private bool _isFlying;
	private int _flightDuration;

	private void Start() => _transform = GetComponent<Transform>();

	public void Init(int delay, float speed, int direction, int duration)
	{
		_flightDelay = delay;
		_flightSpeed = speed;
		_direction = direction;
		_flightDuration = duration;

		Invoke(nameof(StartFly), _flightDelay);
	}

	private void StartFly() 
	{
		_isFlying = true;
		Invoke(nameof(ChangeDirection), _flightDuration);
	}

	private void ChangeDirection()
	{
		_direction *= -1;
		Invoke(nameof(ChangeDirection), _flightDuration);
	}

	private void Update()
	{
		if (!_isFlying)
			return;

		_transform.position += new Vector3(_flightSpeed * _direction, 0);
	}
}