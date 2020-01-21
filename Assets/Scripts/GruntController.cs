using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntController : MonoBehaviour
{
    public int hitPoints = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO hit by projectile?
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;

        if(hitPoints <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        Destroy(this.gameObject);
    }
}
