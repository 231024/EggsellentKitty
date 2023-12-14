using DG.Tweening;
using UnityEngine;

public class TweenController : MonoBehaviour
{
	[SerializeField] private GameController gameController;
	[SerializeField] private CanvasGroup menu;
	[SerializeField] private MenuPanel menuController;
	[SerializeField] private Transform panel;
	[SerializeField] private Transform gameResult;

	private const float MenuShowDuration = 1f;
	private const float MenuHideDuration = 0.3f;
	private const float GameResultShowDuration = 0.3f;
	private const int PanelOffset = -60;
	private const int GameResultOffset = 950;
	private Vector3 _panelDefaultPosition;
	private Vector3 _gameResultDefaultPosition;

	private void Start()
	{
		_panelDefaultPosition = panel.position;
		_gameResultDefaultPosition = gameResult.position;
		gameController.OnGameStateChanged += OnGameStateChanged;
		ShowMenu();
	}

	private void OnDestroy()
	{
		gameController.OnGameStateChanged -= OnGameStateChanged;
	}

	private void OnGameStateChanged()
	{
		switch (gameController.GameState)
		{
			case GameController.State.InProgress:
				HideMenu();
				break;
			case GameController.State.Finished:
				ShowGameResult();
				break;
		}
	}

	private void ShowMenu()
	{
		var seq = DOTween.Sequence();
		seq.Append(menu.DOFade(1f, MenuShowDuration));
	}

	private void HideMenu()
	{
		var seq = DOTween.Sequence();
		seq.Append(menu.DOFade(0f, MenuHideDuration));
		seq.Append(panel.DOMove(
			new Vector3(_panelDefaultPosition.x, _panelDefaultPosition.y + PanelOffset, _panelDefaultPosition.z),
			MenuShowDuration));
	}

	private void ShowGameResult()
	{
		var seq = DOTween.Sequence();
		seq.SetEase(Ease.InOutBack);
		seq.Append(gameResult.DOMove(
			new Vector3(_gameResultDefaultPosition.x - GameResultOffset, _gameResultDefaultPosition.y, _gameResultDefaultPosition.z),
			GameResultShowDuration));
	}
}