using UnityEngine;
using UnityEngine.Events;

public class KittyInputController : MonoBehaviour
{
	private const string HorizontalAxisName = "Horizontal";

	public float HorizontalAxisValue { get; private set; }
	public UnityAction Attack;

	private void Update()
	{
		HorizontalAxisValue = Input.GetAxis(HorizontalAxisName);

		if (Input.GetKeyDown(KeyCode.Space))
			Attack?.Invoke();
	}
}