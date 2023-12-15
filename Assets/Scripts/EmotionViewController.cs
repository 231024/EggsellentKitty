using UnityEngine;

public class EmotionViewController : MonoBehaviour
{
	private static readonly int Happy = Animator.StringToHash("Happy");
	private static readonly int Sad = Animator.StringToHash("Sad");

	[SerializeField] private Animator animator;

	public void Play(bool isHappy) => animator.SetTrigger(isHappy ? Happy : Sad);
}
