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
	[SerializeField] private Transform settings;

	private const float MenuShowDuration = 1f;
	private const float MenuHideDuration = 0.3f;
	private const float GameResultShowDuration = 0.3f;
	private const float DropFlightDuration = 0.8f;
	private const float ShakeDuration = 0.2f;

	private const int PanelOffset = -60;
	private const int SettingsOffset = -400;
	private const int GameResultOffset = 950;

	private Vector3 _panelDefaultPosition;
	private Vector3 _gameResultDefaultPosition;
	private Vector3 _settingsDefaultPosition;

	private Sequence _moveEggSequence;
	private Sequence _increaseScoreSequence;
	private Sequence _moveSuperEggSequence;
	private Sequence _moveShitSequence;
	private Sequence _moveExtraLifeSequence;

	private void Start()
	{
		_moveEggSequence = DOTween.Sequence();
		_increaseScoreSequence = DOTween.Sequence();
		_moveSuperEggSequence = DOTween.Sequence();
		_moveShitSequence = DOTween.Sequence();
		_moveExtraLifeSequence = DOTween.Sequence();

		_panelDefaultPosition = panel.position;
		_gameResultDefaultPosition = gameResult.position;
		_settingsDefaultPosition = settings.position;
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

	private void OnKittyCollected(DropItem item)
	{
		switch (item.Type)
		{
			case DropType.Egg:
				MoveEgg(item.transform);
				break;
			case DropType.SuperEgg:
				MoveSuperEgg(item.transform);
				break;
			case DropType.Shit:
				MoveShit(item.transform);
				break;
			case DropType.ExtraLife:
				MoveExtraLive(item.transform);
				break;
		}
	}


	private void MoveEgg(Transform drop)
	{
		var uiPosition = Camera.main.ScreenToWorldPoint(infoPanelView.GetEggsTransform.position);

		if (_moveEggSequence.IsPlaying())
			_moveEggSequence.Kill(true);

		_moveEggSequence = DOTween.Sequence();
		_moveEggSequence.Append(drop.DOMove(uiPosition, DropFlightDuration));
		_moveEggSequence.AppendCallback(() => Destroy(drop.gameObject));
		_moveEggSequence.Append(infoPanelView.GetEggsTransform.DOShakeRotation(ShakeDuration));
		_moveEggSequence.AppendCallback(() => infoPanelView.RefreshEggs());
		_moveEggSequence.AppendCallback(IncreaseScore);
	}


	private void MoveSuperEgg(Transform drop)
	{
		var uiPosition = Camera.main.ScreenToWorldPoint(infoPanelView.GetSuperEggTransform.position);

		if (_moveSuperEggSequence.IsPlaying())
			_moveSuperEggSequence.Kill(true);
		
		_moveSuperEggSequence = DOTween.Sequence();
		_moveSuperEggSequence.Append(drop.DOMove(uiPosition, DropFlightDuration));
		_moveSuperEggSequence.AppendCallback(() => Destroy(drop.gameObject));
		_moveSuperEggSequence.Append(infoPanelView.GetSuperEggTransform.DOShakeRotation(ShakeDuration));
		_moveSuperEggSequence.AppendCallback(() => infoPanelView.RefreshSuperEggs());
		_moveSuperEggSequence.AppendCallback(IncreaseScore);
	}

	private void IncreaseScore()
	{
		if (_increaseScoreSequence.IsPlaying())
			_increaseScoreSequence.Kill(true);

		_increaseScoreSequence = DOTween.Sequence();
		_increaseScoreSequence.Append(infoPanelView.GetScoreTransform.DOPunchScale(Vector3.one, ShakeDuration));
		_increaseScoreSequence.AppendCallback(() => infoPanelView.SetScore());
	}

	private void MoveShit(Transform drop)
	{
		var uiTransform = infoPanelView.GetLiveToDestroy;
		if (uiTransform == null)
		{
			Destroy(drop.gameObject);
			return;
		}

		var uiPosition = Camera.main.ScreenToWorldPoint(uiTransform.position);
		
		if (_moveShitSequence.IsPlaying())
			_moveShitSequence.Kill(true);
		
		_moveShitSequence = DOTween.Sequence();
		_moveShitSequence.Append(drop.DOMove(uiPosition, DropFlightDuration));
		_moveShitSequence.AppendCallback(() => Destroy(drop.gameObject));
		_moveShitSequence.Append(uiTransform.DOPunchScale(Vector3.one, ShakeDuration));
		_moveShitSequence.AppendCallback(() => infoPanelView.RefreshLives());
	}

	private void MoveExtraLive(Transform drop)
	{
		var uiTransform = infoPanelView.GetLiveToRestore;
		if (uiTransform == null)
		{
			Destroy(drop.gameObject);
			return;
		}

		var uiPosition = Camera.main.ScreenToWorldPoint(uiTransform.position);

		if(_moveExtraLifeSequence.IsPlaying())
			_moveExtraLifeSequence.Kill(true);

		_moveExtraLifeSequence = DOTween.Sequence();
		_moveExtraLifeSequence.Append(drop.DOMove(uiPosition, DropFlightDuration));
		_moveExtraLifeSequence.AppendCallback(() => Destroy(drop.gameObject));
		_moveExtraLifeSequence.Append(uiTransform.DOPunchScale(Vector3.one, ShakeDuration));
		_moveExtraLifeSequence.AppendCallback(() => infoPanelView.RefreshLives());
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
		seq.AppendCallback(() => panel.gameObject.SetActive(true));
		seq.Append(panel.DOMove(
			new Vector3(_panelDefaultPosition.x, _panelDefaultPosition.y + PanelOffset, _panelDefaultPosition.z),
			MenuShowDuration));
		seq.AppendCallback(() => menu.blocksRaycasts = false);
	}

	public void ShowSettings()
	{
		if (settings.gameObject.activeSelf)
			return;

		var seq = DOTween.Sequence();
		seq.AppendCallback(() => settings.gameObject.SetActive(true));
		seq.Append(settings.DOMove(new Vector3(_settingsDefaultPosition.x, _settingsDefaultPosition.y + SettingsOffset,
			_settingsDefaultPosition.z), MenuShowDuration));
	}

	public void HideSettings()
	{
		if (!settings.gameObject.activeSelf)
			return;
		
		var seq = DOTween.Sequence();
		seq.Append(settings.DOMove(new Vector3(_settingsDefaultPosition.x, _settingsDefaultPosition.y - SettingsOffset,
			_settingsDefaultPosition.z), MenuShowDuration));
		seq.AppendCallback(() => settings.gameObject.SetActive(false));
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