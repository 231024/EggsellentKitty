using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
	[SerializeField] private TweenController tweenController;
	[SerializeField] private WeatherController weatherController;

	public void OnHideClick() => tweenController.HideSettings();

	public void OnSnowClick() => weatherController.ChangeWeather();

	public void OnSoundClick()
	{
	}
}