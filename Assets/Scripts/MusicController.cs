using System;
using UnityEngine;

public class MusicController : MonoBehaviour
{
	[SerializeField] private AudioSource musicSource;
	[SerializeField] private AudioSource[] snowSources;

	private const string SoundIsOnKey = "sound/is-on";

	public void Awake() => SwitchAll();

	public void Switch()
	{
		IsOn = !IsOn;
		SwitchAll();
	}

	private void SwitchAll()
	{
		musicSource.mute = !IsOn;

		foreach (var snowSource in snowSources)
		{
			snowSource.mute = !IsOn;
		}
	}

	public bool IsOn
	{
		get => PlayerPrefs.GetInt(SoundIsOnKey, 1) == 1;
		set
		{
			PlayerPrefs.SetInt(SoundIsOnKey, value ? 1 : 0);
			PlayerPrefs.Save();
		}
	}
}