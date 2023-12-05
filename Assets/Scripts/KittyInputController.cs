using UnityEngine;

public class KittyInputController : MonoBehaviour
{
	private const string HorizontalAxisName = "Horizontal";

	public float HorizontalAxisValue { get; private set; }
	
	private void Update() => HorizontalAxisValue = Input.GetAxis(HorizontalAxisName);
}