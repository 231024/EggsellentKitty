using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
	[SerializeField] private TweenController tweenController;
	[SerializeField] private WeatherController weatherController;
	[SerializeField] private MusicController musicController;
	[SerializeField] private GameController gameController;
	[SerializeField] private Image soundIcon;
	[SerializeField] private Sprite soundIsOn;
	[SerializeField] private Sprite soundIsOff;

	private void Awake() => SetupSoundIcon();

	public void OnHideClick() => tweenController.HideSettings();

	public void OnSnowClick() => weatherController.ChangeWeather();

	public void OnSoundClick()
	{
		musicController.Switch();
		SetupSoundIcon();
	}

	public void OnRestartClick() => gameController.RestartGame();

	private void SetupSoundIcon() => soundIcon.overrideSprite = musicController.IsOn ? soundIsOn : soundIsOff;
}