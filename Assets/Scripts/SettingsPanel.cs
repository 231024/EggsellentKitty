using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
	[SerializeField] private TweenController tweenController;
	[SerializeField] private WeatherController weatherController;
	[SerializeField] private MusicController musicController;
	[SerializeField] private GameController gameController;
	[SerializeField] private SwitchButton soundButton;
	[SerializeField] private SwitchButton musicButton;

	private void Awake()
	{
		SetupSoundButton();
		SetupMusicButton();
	}

	public void OnHideClick() => tweenController.HideSettings();

	public void OnSnowClick() => weatherController.ChangeWeather();

	public void OnSoundClick()
	{
		musicController.SwitchSound();
		SetupSoundButton();
	}

	public void OnMusicClick()
	{
		musicController.SwitchMusic();
		SetupMusicButton();
	}

	private void SetupSoundButton() => soundButton.Setup(musicController.SoundIsOn);
	private void SetupMusicButton() => musicButton.Setup(musicController.MusicIsOn);

	public void OnRestartClick() => gameController.RestartGame();
}