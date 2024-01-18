using UnityEngine;

public class EmotionViewController : MonoBehaviour
{
	private static readonly int Happy = Animator.StringToHash("Happy");
	private static readonly int Sad = Animator.StringToHash("Sad");
	private static readonly int Shock = Animator.StringToHash("Shock");

	[SerializeField] private Animator animator;

	public void Play(bool isHappy) => animator.SetTrigger(isHappy ? Happy : Sad);

	public void PlayShock() => animator.SetTrigger(Shock);
}
