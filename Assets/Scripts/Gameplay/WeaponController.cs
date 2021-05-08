using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	private enum WeaponState
	{
		IDLE, // Can only shoot from IDLE
		SHOOTING,
		RELOADING,
	}

	[Header("Colours")]
	[SerializeField] private Color _defaultAmmoColor = Color.white;
	[SerializeField] private Color _reloadingColor = Color.red;


	[Header("References")]
	[SerializeField] private Transform _firingPoint;
	[SerializeField] private SpriteRenderer _muzzleFlashEffect; //TODO: Implement!
	[SerializeField] private LineRenderer _bulletEffect;

//TODO: CONSTANTS BUT NOT REALLY - might change with different guns!
	private float _bulletEffectSeconds = 0.05f; //TODO: Frames or seconds??
	private float _secondsBetweenShots = 0.5f;
	private float _fullReloadSeconds = 1.0f;
	private float _reloadEndDelay = 0.5f;
	private int _weaponDamage = 1;
	private int _framesForFlash = 5;

	private int _currentAmmo;
	private int _clipSize = 7;
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
		StartCoroutine(ShotCooldown());
		StartCoroutine(ShowMuzzleFlash());
		//TODO: Play gunshot sound effect here

		_currentAmmo--;

		if(_currentAmmo <= 0 && _currentState != WeaponState.RELOADING)
		{
			Reload();
		}

		UpdateAmmoText();

		RaycastHit2D hitInfo = Physics2D.Raycast(_firingPoint.position, _firingPoint.up);

		_bulletEffect.SetPosition(0, _firingPoint.position);
		_bulletEffect.SetPosition(1, hitInfo.point);

		//Bullet will ALWAYS hit something, even if it's a wall

		Transform hitInfoTransform = hitInfo.transform;
		if(hitInfo.transform != null)
		{
			GruntController grunt = hitInfo.transform.GetComponent<GruntController>();
			if(grunt != null)
			{
				grunt.TakeDamage(_weaponDamage);
			}
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

	private void UpdateAmmoText()
	{
		GameController.instance.UIController.SetAmmoCountText($"Ammo: {_currentAmmo.ToString()}", _defaultAmmoColor);
	}

	private void ShowReloadingText()
	{
		GameController.instance.UIController.SetAmmoCountText("RELOADING", _reloadingColor);
	}

	private void Reload()
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
