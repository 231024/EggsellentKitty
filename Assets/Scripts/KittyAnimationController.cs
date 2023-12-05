using UnityEngine;

public class KittyAnimationController : MonoBehaviour
{
	private static readonly int Direction = Animator.StringToHash("Direction");

	[SerializeField] private Animator animator;
	[SerializeField] private KittyPhysicsController physics;

	private void Update()
	{
		animator.SetInteger(Direction, physics.Direction);
		//animator.SetBool(IsRun, Mathf.Abs(_input.HAxisValue) > float.Epsilon);
		//_renderer.flipX = _input.HAxisValue < 0.0f;
	}
}