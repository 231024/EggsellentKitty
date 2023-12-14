using UnityEngine;

public class GameResult : MonoBehaviour
{
	[SerializeField] private GameController gameController;
	[SerializeField] private GameObject winPanel;
	[SerializeField] private GameObject loosePanel;

	private void Awake()
	{
		gameController.OnGameStateChanged += OnGameStateChanged;
	}

	private void OnDestroy() => gameController.OnGameStateChanged -= OnGameStateChanged;

	private void OnGameStateChanged()
	{
		if (gameController.GameState != GameController.State.Finished || gameController.Result == null)
			return;
		
		winPanel.SetActive(gameController.Result.Value);
		loosePanel.SetActive(!gameController.Result.Value);
	}
}