using System.Collections.Generic;
using UnityEngine;

public class EmotionsController : MonoBehaviour
{
	[SerializeField] private KittyPhysicsController kittyController;
	[SerializeField] private GameController gameController;
	[SerializeField] private Attacker attacker;

	[SerializeField] private AudioSource kittyAudioSource;
	[SerializeField] private AudioClip kittyHappy;
	[SerializeField] private AudioClip kittySad;

	[SerializeField] private AudioSource birdAudioSource;
	[SerializeField] private AudioClip birdHappy;
	[SerializeField] private AudioClip birdSad;

	private EmotionViewController _kitty;
	private readonly Dictionary<string, EmotionViewController> _birds = new();

	private void Awake()
	{
		_kitty = kittyController.gameObject.GetComponentInChildren<EmotionViewController>();
		kittyController.OnCollect += OnCollect;
		gameController.OnGameStateChanged += OnGameStateChanged;
		attacker.OnHit += OnHit;
	}

	private void OnDestroy()
	{
		kittyController.OnCollect -= OnCollect;
		gameController.OnGameStateChanged -= OnGameStateChanged;
		attacker.OnHit -= OnHit;
	}

	private void OnGameStateChanged()
	{
		if (gameController.NotStarted)
			_birds.Clear();
	}

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
		PlaySound(kittyAudioSource, GetKittyAudio(kittyEmotion));

		if (_birds.TryGetValue(item.Source, out var bird))
		{
			bird.Play(birdEmotion);
			PlaySound(birdAudioSource, GetBirdAudio(birdEmotion));
		}
	}

	private void OnHit()
	{
		foreach (var bird in _birds)
		{
			bird.Value.PlayShock();
		}
	}

	private AudioClip GetKittyAudio(bool emotion) => emotion ? kittyHappy : kittySad;
	private AudioClip GetBirdAudio(bool emotion) => emotion ? birdHappy : birdSad;

	private void PlaySound(AudioSource source, AudioClip clip)
	{
		source.PlayOneShot(clip);
	}
}