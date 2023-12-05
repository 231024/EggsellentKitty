using UnityEngine;

public class BirdFlyingBehaviour : BirdBaseBehaviour
{
	private Transform _transform;
	private int _flightDelay;
	private float _flightSpeed;
	private int _direction;
	private bool _isFlying;

	private void Start() => _transform = GetComponent<Transform>();

	public void Init(int delay, float speed, int direction)
	{
		_flightDelay = delay;
		_flightSpeed = speed;
		_direction = direction;

		Invoke(nameof(StartFly), _flightDelay);
	}

	private void StartFly() => _isFlying = true;

	private void Update()
	{
		if (!_isFlying)
			return;

		_transform.position += new Vector3(_flightSpeed * _direction, 0);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.gameObject.GetComponent<FlyingSpawnPoint>())
			return;

		_isFlying = false;
		_direction *= -1;
		Invoke(nameof(StartFly), _flightDelay);
	}
}