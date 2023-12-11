using TMPro;
using UnityEngine;

public class ValueItem : MonoBehaviour
{
	private TMP_Text _text;

	private void Awake() => _text = GetComponent<TMP_Text>();

	private const string ValuePattern = "x{0}";
	private const string ScorePattern = "Score: {0}";

	public void SetValue(int value) => _text.SetText(string.Format(ValuePattern, value));
	
	public void SetScore(int score) => _text.SetText(string.Format(ScorePattern, score));
}