using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _barrierSprite;
	[SerializeField] private float _damageFlashSeconds = 0.2f;

	private Coroutine _currentDamageAnimation;

	private int _hitPoints;

	public void Awake()
	{
		if (_barrierSprite == null)
		{
			Debug.LogError("[Barrier] Cannot find sprite!");
		}

		_hitPoints = 10;
	}

	public void OnHit(int damage)
	{
		_hitPoints -= damage;

		if (_hitPoints <= 0)
		{
			//Barrier is destroyed!
			_barrierSprite.enabled = false;
			//gameObject.SetActive(false);
		}
		//TODO: reduce health bar, show vibration animation!

		if(_currentDamageAnimation != null)
		{
			StopCoroutine(_currentDamageAnimation);
		}
		_currentDamageAnimation = StartCoroutine(ShowDamageAnimation());
	}

	public bool IsBarrierUp()
	{
		return _hitPoints > 0;
	}

	private IEnumerator ShowDamageAnimation()
	{
		_barrierSprite.color = Color.red;

		yield return new WaitForSeconds(_damageFlashSeconds);

		_barrierSprite.color = Color.white;
		_currentDamageAnimation = null;
	}
}
