using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private ObjectsPool _bulletsPool;
    [SerializeField, Min(0)] private int _bulletLifetime;
    [SerializeField, Min(0)] private float _bulletForce;
    [SerializeField, Min(0)] private int _ammo;
    [SerializeField] private TextMeshProUGUI _ammoIndicator;


    private void Update()
    {
        ProcessInput();
        UpdateUI();
    }

    private void Shoot()
    {
        if (_ammo == 0)
        {
            return;
        }

        var bullet = _bulletsPool.GetObject();
        bullet.transform.position = _firePoint.position;
        bullet.transform.rotation = _firePoint.rotation;

        bullet.GetComponent<Rigidbody2D>().AddForce(_firePoint.up * _bulletForce, ForceMode2D.Impulse);

        _ammo -= 1;

        StartCoroutine(BulletTimer(bullet));
    }

    private IEnumerator BulletTimer(GameObject obj)
    {
        yield return new WaitForSeconds(_bulletLifetime);

        _bulletsPool.ReturnObject(obj);
    }

    private void ProcessInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void UpdateUI()
    {
        _ammoIndicator.text = _ammo.ToString();
    }
}