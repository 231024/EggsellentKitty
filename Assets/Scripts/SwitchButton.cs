using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
	[SerializeField] private Image image;
	[SerializeField] private Sprite spriteOn;
	[SerializeField] private Sprite spriteOff;

	public void Setup(bool isOn) => image.overrideSprite = isOn ? spriteOn : spriteOff;
}