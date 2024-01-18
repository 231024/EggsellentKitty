using UnityEngine;

public class AbilityPanel : MonoBehaviour
{
	[SerializeField] private GameController gameController;
	[SerializeField] private GameObject hint;

	private void Awake() => gameController.OnKittyStateChanged += OnStateChanged;

	private void OnDestroy() => gameController.OnKittyStateChanged -= OnStateChanged;

	private void OnStateChanged() => hint.SetActive(gameController.HasAttack);
}