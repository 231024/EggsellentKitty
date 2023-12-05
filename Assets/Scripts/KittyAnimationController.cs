using UnityEngine;

public class KittyAnimationController : MonoBehaviour
{
	private static readonly int Movement = Animator.StringToHash("Movement");

	[SerializeField] private Animator animator;
	[SerializeField] private KittyPhysicsController physics;

	private void Update()
	{
		animator.SetInteger(Movement, physics.Direction);
		//animator.SetBool(IsRun, Mathf.Abs(_input.HAxisValue) > float.Epsilon);
		//_renderer.flipX = _input.HAxisValue < 0.0f;
	}
}