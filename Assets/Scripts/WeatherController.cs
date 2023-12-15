using UnityEngine;

public class WeatherController : MonoBehaviour
{
	[SerializeField] private GameObject snow;

	public void ChangeWeather() => snow.SetActive(!snow.activeSelf);
}