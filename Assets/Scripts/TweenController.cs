using DG.Tweening;
using UnityEngine;

public class TweenController : MonoBehaviour
{
	[SerializeField] private GameController gameController;
	[SerializeField] private KittyPhysicsController kittyController;
	[SerializeField] private CanvasGroup menu;
	[SerializeField] private InfoPanel infoPanelView;
	[SerializeField] private Transform panel;
	[SerializeField] private Transform gameResult;

	private const float MenuShowDuration = 1f;
	private const float MenuHideDuration = 0.3f;
	private const float GameResultShowDuration = 0.3f;
	private const float DropFlightDuration = 0.8f;

	private const int PanelOffset = -60;
	private const int GameResultOffset = 950;

	private Vector3 _panelDefaultPosition;
	private Vector3 _gameResultDefaultPosition;

	private void Start()
	{
		_panelDefaultPosition = panel.position;
		_gameResultDefaultPosition = gameResult.position;
		gameController.OnGameStateChanged += OnGameStateChanged;
		kittyController.OnCollect += OnKittyCollected;
		ShowMenu();
	}

	private void OnDestroy()
	{
		gameController.OnGameStateChanged -= OnGameStateChanged;
		kittyController.OnCollect -= OnKittyCollected;
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

	private void OnKittyCollected(DropType dropType, GameObject drop)
	{
		switch (dropType)
		{
			case DropType.Egg:
				MoveEgg(drop.transform);
				break;
			case DropType.SuperEgg:
				MoveSuperEgg(drop.transform);
				break;
			case DropType.Shit:
				MoveShit(drop.transform);
				break;
			case DropType.ExtraLife:
				MoveExtraLive(drop.transform);
				break;
		}
	}

	private void MoveEgg(Transform drop)
	{
		var seq = DOTween.Sequence();
		var uiPosition = Camera.main.ScreenToWorldPoint(infoPanelView.GetEggsTransform.position);

		seq.Append(drop.DOMove(uiPosition, DropFlightDuration));
		seq.AppendCallback(() => Destroy(drop.gameObject));
		seq.Append(infoPanelView.GetEggsTransform.DOShakeRotation(0.2f));
		seq.AppendCallback(() =>infoPanelView.RefreshEggs());
		seq.Append(infoPanelView.GetScoreTransform.DOPunchScale(Vector3.one, 0.2f));
		seq.AppendCallback(() => infoPanelView.SetScore());
	}

	private void MoveSuperEgg(Transform drop)
	{
		var seq = DOTween.Sequence();
		var uiPosition = Camera.main.ScreenToWorldPoint(infoPanelView.GetSuperEggTransform.position);

		seq.Append(drop.DOMove(uiPosition, DropFlightDuration));
		seq.AppendCallback(() => Destroy(drop.gameObject));
		seq.Append(infoPanelView.GetSuperEggTransform.DOShakeRotation(0.2f));
		seq.AppendCallback(() => infoPanelView.RefreshSuperEggs());
		seq.Append(infoPanelView.GetScoreTransform.DOPunchScale(Vector3.one, 0.2f));
		seq.AppendCallback(() => infoPanelView.SetScore());
	}

	private void MoveShit(Transform drop)
	{
		var seq = DOTween.Sequence();
		var uiTransform = infoPanelView.GetLiveToDestroy;
		if (uiTransform == null)
		{
			Destroy(drop.gameObject);
			return;
		}

		var uiPosition = Camera.main.ScreenToWorldPoint(uiTransform.position);

		seq.Append(drop.DOMove(uiPosition, DropFlightDuration));
		seq.AppendCallback(() => Destroy(drop.gameObject));
		seq.Append(uiTransform.DOPunchScale(Vector3.one, 0.2f));
		seq.AppendCallback(() => infoPanelView.RefreshLives());
	}

	private void MoveExtraLive(Transform drop)
	{
		var seq = DOTween.Sequence();
		var uiTransform = infoPanelView.GetLiveToRestore;
		if (uiTransform == null)
		{
			Destroy(drop.gameObject);
			return;
		}

		var uiPosition = Camera.main.ScreenToWorldPoint(uiTransform.position);

		seq.Append(drop.DOMove(uiPosition, DropFlightDuration));
		seq.AppendCallback(() => Destroy(drop.gameObject));
		seq.Append(uiTransform.DOPunchScale(Vector3.one, 0.2f));
		seq.AppendCallback(() => infoPanelView.RefreshLives());
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
			new Vector3(_gameResultDefaultPosition.x - GameResultOffset, _gameResultDefaultPosition.y,
				_gameResultDefaultPosition.z),
			GameResultShowDuration));
	}
}