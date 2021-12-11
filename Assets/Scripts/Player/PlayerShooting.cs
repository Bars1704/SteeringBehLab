using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;
using UnityEngine.Serialization;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private ObjectsPool _bulletsPool;
    [SerializeField, Min(0)] private int _bulletLifetime;
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
        var bullet = _bulletsPool.GetObject();
        bullet.transform.position = _firePoint.position;
        bullet.transform.rotation = _firePoint.rotation;
        
        bullet.GetComponent<Rigidbody2D>().AddForce(_firePoint.up * _bulletForce, ForceMode2D.Impulse);
        StartCoroutine(BulletTimer(bullet));
    }

    private IEnumerator BulletTimer(GameObject obj)
    {
        yield return new WaitForSeconds(_bulletLifetime);
        
        _bulletsPool.ReturnObject(obj);
    }
}