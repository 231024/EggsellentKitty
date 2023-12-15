using TMPro;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
	[SerializeField] private GameController gameController;
	[SerializeField] private GameConfig config;
	[SerializeField] private TMP_Text eggRule;
	[SerializeField] private TMP_Text superEggRule;
	[SerializeField] private TMP_Text scoreToWin;
	[SerializeField] private WeatherController weatherController;

	private const string EggRuleText = "+{0} point to your score";
	private const string ScoreToWinRuleText = "collect {0} points to win";

	private void Awake()
	{
		FillRules();
	}

	private void FillRules()
	{
		eggRule.text = string.Format(EggRuleText, config.eggPoints);
		superEggRule.text = string.Format(EggRuleText, config.superEggPoints);
		scoreToWin.text = string.Format(ScoreToWinRuleText, config.scoreToWin);
	}

	public void StartGame() => gameController.StartGame();

	public void LetItSnow() => weatherController.ChangeWeather();
}