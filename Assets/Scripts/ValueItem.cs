using TMPro;
using UnityEngine;

public class ValueItem : MonoBehaviour
{
	[SerializeField] private TMP_Text text;

	private const string ValuePattern = "x{0}";
	private const string ScorePattern = "Score: {0}";

	public void SetValue(int value) => text.SetText(string.Format(ValuePattern, value));
	
	public void SetScore(int score) => text.SetText(string.Format(ScorePattern, score));
}