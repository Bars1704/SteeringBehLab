using UnityEngine;

public class Player : MonoBehaviour, IKillable, IPursuitable
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerShooting _playerShooting;
    public void Kill()
    {
        _playerShooting.Reset();
        _playerMovement.Reset();
    }

    public Vector3 Velocity { get => gameObject.GetComponent<Rigidbody2D>().velocity; }
    public Vector3 Position { get => gameObject.transform.position; }
}
