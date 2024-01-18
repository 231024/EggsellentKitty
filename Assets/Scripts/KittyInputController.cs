using UnityEngine;
using UnityEngine.Events;

public class KittyInputController : MonoBehaviour
{
	[SerializeField] private GameController gameController;

	private const string HorizontalAxisName = "Horizontal";

	public float HorizontalAxisValue { get; private set; }

	public UnityAction Attack;

	private void Update()
	{
		HorizontalAxisValue = Input.GetAxis(HorizontalAxisName);

		if (gameController.HasAttack && Input.GetKeyDown(KeyCode.Space))
			Attack?.Invoke();
	}
}