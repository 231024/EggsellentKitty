using UnityEngine;

public class FlyingSpawnPoint : MonoBehaviour
{
	[SerializeField] private int _direction;

	public int Direction => _direction;
}