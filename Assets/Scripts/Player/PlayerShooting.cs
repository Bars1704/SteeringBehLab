using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;
using UnityEngine.Serialization;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Rigidbody2D _bulletRb;
    [SerializeField] private Transform _bulletsContainer;
    
    [SerializeField, Min(0)] private float _bulletForce;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        var bullet = Instantiate(_bulletRb, _firePoint.position, _firePoint.rotation, _bulletsContainer);
        bullet.AddForce(_firePoint.up * _bulletForce, ForceMode2D.Impulse);
    }
}