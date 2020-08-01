using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public int HitPoints;


    public void OnHit(int damage)
    {
        HitPoints -= damage;

        if(HitPoints <= 0)
        {
            //Barrier is destroyed!
        }
        //TODO: reduce health bar, show vibration animation!
    }
}
