using System.Collections.Generic;
using UnityEngine;

public class EmotionsController : MonoBehaviour
{
	[SerializeField] private KittyPhysicsController kittyController;

	private EmotionViewController _kitty;
	private readonly Dictionary<string, EmotionViewController> _birds = new();

	private void Awake()
	{
		_kitty = kittyController.gameObject.GetComponentInChildren<EmotionViewController>();
		kittyController.OnCollect += OnCollect;
	}

	private void OnDestroy() => kittyController.OnCollect -= OnCollect;

	public void RegisterBird(GameObject character)
	{
		var emotionView = character.GetComponentInChildren<EmotionViewController>();
		if (emotionView == null)
			return;

		_birds.Add(character.name, emotionView);
	}

	private void OnCollect(DropItem item)
	{
		var kittyEmotion = item.Type is not DropType.Shit;
		var birdEmotion = !kittyEmotion;

		_kitty.Play(kittyEmotion);

		if (_birds.TryGetValue(item.Source, out var bird))
			bird.Play(birdEmotion);
	}
}