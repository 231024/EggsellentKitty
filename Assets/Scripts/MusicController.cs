using UnityEngine;

public class MusicController : MonoBehaviour
{
	[SerializeField] private AudioSource musicSource;
	[SerializeField] private AudioSource snowSource;
	[SerializeField] private AudioSource[] emotionSources;

	private const string MusicIsOnKey = "music/is-on";
	private const string SoundIsOnKey = "sound/is-on";

	public void Awake()
	{
		SwitchAllMusicSources();
		SwitchAllSoundSources();
	}

	public void SwitchMusic()
	{
		MusicIsOn = !MusicIsOn;
		SwitchAllMusicSources();
	}
	
	public void SwitchSound()
	{
		SoundIsOn = !SoundIsOn;
		SwitchAllSoundSources();
	}

	private void SwitchAllMusicSources()
	{
		musicSource.mute = !MusicIsOn;
		snowSource.mute = !MusicIsOn;
	}

	private void SwitchAllSoundSources()
	{
		foreach (var source in emotionSources)
			source.mute = !SoundIsOn;
	}

	public bool MusicIsOn
	{
		get => PlayerPrefs.GetInt(MusicIsOnKey, 1) == 1;
		set
		{
			PlayerPrefs.SetInt(MusicIsOnKey, value ? 1 : 0);
			PlayerPrefs.Save();
		}
	}

	public bool SoundIsOn
	{
		get => PlayerPrefs.GetInt(SoundIsOnKey, 1) == 1;
		set
		{
			PlayerPrefs.SetInt(SoundIsOnKey, value ? 1 : 0);
			PlayerPrefs.Save();
		}
	}
}