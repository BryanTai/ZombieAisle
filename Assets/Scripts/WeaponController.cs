using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Transform _firingPoint;
	[SerializeField] private SpriteRenderer _muzzleFlashEffect; //TODO: Implement!
	[SerializeField] private LineRenderer _bulletEffect;

	private float _bulletEffectTime = 0.2f;
	private float _timeBetweenShots = 0.5f;
	private float _reloadTime = 2.0f;
	private int _weaponDamage = 1;
	private int _framesForFlash = 10;

	private int _currentAmmo;
	private bool _canShoot;

	public void ShootWeapon()
	{
		if(_canShoot)
		{
			StartCoroutine(Shoot());
		}
	}

	private void Awake()
	{
		_bulletEffect.enabled = false;
		_canShoot = true;
		_muzzleFlashEffect.gameObject.SetActive(false);
	}

	private IEnumerator ShotCooldown()
	{
		yield return new WaitForSeconds(_timeBetweenShots);
		_canShoot = true;
	}

	private IEnumerator ShowMuzzleFlash()
	{
		_muzzleFlashEffect.gameObject.SetActive(true);
		int framesFlashed = 0;

		while(framesFlashed < _framesForFlash)
		{
			framesFlashed++;
			yield return null;
		}

		_muzzleFlashEffect.gameObject.SetActive(false);
	}

	private IEnumerator Shoot()
	{
		_canShoot = false;

		StartCoroutine(ShotCooldown());
		StartCoroutine(ShowMuzzleFlash());

		RaycastHit2D hitInfo = Physics2D.Raycast(_firingPoint.position, _firingPoint.up);

		if(hitInfo != null) //TODO: Always true??
		{
			_bulletEffect.SetPosition(0, _firingPoint.position);
			_bulletEffect.SetPosition(1, hitInfo.point);

			GruntController grunt = hitInfo.transform.GetComponent<GruntController>();
			if(grunt != null)
			{
				grunt.TakeDamage(_weaponDamage);
			}
		}
		else
		{
			//We missed!
			_bulletEffect.SetPosition(0, _firingPoint.position);
			_bulletEffect.SetPosition(1, _firingPoint.position + _firingPoint.up * 100);
		}

		_bulletEffect.enabled = true;
		yield return new WaitForSeconds(_bulletEffectTime);
		_bulletEffect.enabled = false;
	}

	private IEnumerator ReloadCooldown()
	{
		yield return new WaitForSeconds(_reloadTime);
		_canShoot = true;
	}

	private void Reload() //TODO: Implement ammo!
	{
		_canShoot = false;
		StartCoroutine(ReloadCooldown());
	}
}
