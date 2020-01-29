﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform FiringPoint;
    public int weaponDamage = 1;

    [SerializeField] private LineRenderer bulletEffect;
    [SerializeField] private float bulletEffectTime = 0.2f;
    [SerializeField] private float timeBetweenShots = 0.5f;
    [SerializeField] private float reloadTime = 2.0f;


    private int currentAmmo;
    private bool canShoot;

    public void ShootWeapon()
    {
        if(canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    private void Awake()
    {
        bulletEffect.enabled = false;
        canShoot = true;
    }

    private IEnumerator ShotCooldown()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private IEnumerator Shoot()
    {
        canShoot = false;

        StartCoroutine(ShotCooldown());

        RaycastHit2D hitInfo = Physics2D.Raycast(FiringPoint.position, FiringPoint.up);

        if(hitInfo != null)
        {
            //TODO: Register enemy hit here
            bulletEffect.SetPosition(0, FiringPoint.position);
            bulletEffect.SetPosition(1, hitInfo.point);

            GruntController grunt = hitInfo.transform.GetComponent<GruntController>();
            if(grunt != null)
            {
                grunt.TakeDamage(weaponDamage);
            }
        }
        else
        {
            //We missed!
            bulletEffect.SetPosition(0, FiringPoint.position);
            bulletEffect.SetPosition(1, FiringPoint.position + FiringPoint.up * 100);
        }

        bulletEffect.enabled = true;
        yield return new WaitForSeconds(bulletEffectTime);
        bulletEffect.enabled = false;
    }

    private IEnumerator ReloadCooldown()
    {
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
    }

    private void Reload()
    {
        canShoot = false;
        StartCoroutine(ReloadCooldown());
    }
}
