using UnityEngine;

public class KittyAnimationController : MonoBehaviour
{
	private static readonly int Attack = Animator.StringToHash("Attack");
	private static readonly int MoveRight = Animator.StringToHash("MoveRight");
	private static readonly int MoveLeft = Animator.StringToHash("MoveLeft");
	private static readonly int Stay = Animator.StringToHash("Stay");

	[SerializeField] private Animator animator;
	[SerializeField] private KittyPhysicsController physics;
	[SerializeField] private KittyInputController input;

	private int _direction;

	private void Awake()
	{
		_direction = physics.Direction;
		input.Attack += OnAttack;
	}

	private void OnDestroy() => input.Attack -= OnAttack;

	private void Update()
	{
		if (_direction == physics.Direction)
			return;

		_direction = physics.Direction;

		switch (_direction)
		{
			case < 0:
				animator.SetTrigger(MoveLeft);
				break;
			case > 0:
				animator.SetTrigger(MoveRight);
				break;
			case 0:
				animator.SetTrigger(Stay);
				break;
		}
	}

	private void OnAttack()
	{
		animator.SetTrigger(Attack);
	}
}