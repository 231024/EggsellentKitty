using UnityEngine;
using UnityEngine.UI;

public class LifeItem : MonoBehaviour
{
	[SerializeField] private Image image;
	[SerializeField] private CanvasGroup canvasGroup;
	[SerializeField] private Sprite full;
	[SerializeField] private Sprite empty;

	private const float FullAlpha = 1f;
	private const float EmptyAlpha = 0.5f;

	public void SetState(bool isFull)
	{
		image.overrideSprite = isFull ? full : empty;
		canvasGroup.alpha = isFull ? FullAlpha : EmptyAlpha;
	}
}