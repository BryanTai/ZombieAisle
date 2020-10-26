using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	private enum WeaponState
	{
		IDLE,
		SHOOTING,
		RELOADING,
	}

	[Header("References")]
	[SerializeField] private Transform _firingPoint;
	[SerializeField] private SpriteRenderer _muzzleFlashEffect; //TODO: Implement!
	[SerializeField] private LineRenderer _bulletEffect;

//TODO: CONSTANTS BUT NOT REALLY - might change with different guns!
	private float _bulletEffectSeconds = 0.05f; //TODO: Frames or seconds??
	private float _secondsBetweenShots = 0.5f;
	private float _fullReloadSeconds = 2.0f;
	private float _reloadEndDelay = 0.5f;
	private int _weaponDamage = 1;
	private int _framesForFlash = 5;

	private int _currentAmmo;
	private int _clipSize = 4;
	private WeaponState _currentState;

	public void ShootWeapon()
	{
		if(_currentState == WeaponState.IDLE)
		{
			_currentState = WeaponState.SHOOTING;
			StartCoroutine(Shoot());
		}
	}

	public void InitializeWeapon()
	{
		UpdateAmmoText();
	}

	private void Awake()
	{
		_bulletEffect.enabled = false;
		_currentState = WeaponState.IDLE;
		_muzzleFlashEffect.gameObject.SetActive(false);
		_currentAmmo = _clipSize;
	}

	private IEnumerator ShotCooldown()
	{
		yield return new WaitForSeconds(_secondsBetweenShots);
		_currentState = WeaponState.IDLE;
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
		if(_currentAmmo <= 0)
		{
			if(_currentState != WeaponState.RELOADING)
			{
				Reload();
			}
		}
		else
		{
			StartCoroutine(ShotCooldown());
			StartCoroutine(ShowMuzzleFlash());

			_currentAmmo--;
			UpdateAmmoText();

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

			int framesFlashed = 0;
			while(framesFlashed < _framesForFlash)
			{
				framesFlashed++;
				yield return null;
			}

			_bulletEffect.enabled = false;
		}
	}

	private void UpdateAmmoText()
	{
		GameController.instance.UIController.SetAmmoCountText($"Ammo: {_currentAmmo.ToString()}");
	}

	private void ShowReloadingText()
	{
		GameController.instance.UIController.SetAmmoCountText("RELOADING");
	}

	private void Reload() //TODO: Implement ammo!
	{
		_currentState = WeaponState.RELOADING;
		ShowReloadingText();
		StartCoroutine(ReloadCooldown());
	}

	private IEnumerator ReloadCooldown()
	{
		yield return new WaitForSeconds(_fullReloadSeconds - _reloadEndDelay);
		
		_currentAmmo = _clipSize;
		UpdateAmmoText();
		yield return new WaitForSeconds(_reloadEndDelay);

		_currentState = WeaponState.IDLE;
	}
}
