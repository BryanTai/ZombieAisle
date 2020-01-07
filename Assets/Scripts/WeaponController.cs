using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform FiringPoint;

    [SerializeField] private LineRenderer bulletEffect;
    [SerializeField] private float bulletEffectTime = 0.2f;

    private void Awake()
    {
        bulletEffect.enabled = false;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(FiringPoint.position, FiringPoint.up);

        if(hitInfo != null)
        {
            //TODO: Register enemy hit here
            Debug.Log("BAP");

            bulletEffect.SetPosition(0, FiringPoint.position);
            bulletEffect.SetPosition(1, hitInfo.point);
        }
        else
        {
            //We missed!
            bulletEffect.SetPosition(0, FiringPoint.position);
            bulletEffect.SetPosition(1, FiringPoint.position + FiringPoint.right * 100);
        }

        bulletEffect.enabled = true;
        yield return new WaitForSeconds(bulletEffectTime);
        bulletEffect.enabled = false;
    }
}
