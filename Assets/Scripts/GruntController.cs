using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntController : MonoBehaviour
{
    public int hitPoints = 3;

    private SimplePF2D.Path path;
    private Rigidbody2D rb;
    private Vector3 nextPoint;
    private bool isStationary;
    private float speed = 5.0f;

    private void Start()
    {
        path = new SimplePF2D.Path(GameObject.Find("Grid").GetComponent<SimplePathFinding2D>());
        rb = GetComponent<Rigidbody2D>();
        nextPoint = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseworldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseworldpos.z = 0.0f;

            path.CreatePath(transform.position, mouseworldpos);
        }

        if(path.IsGenerated())
        {
            if(isStationary)
            {
                if(path.GetNextPoint(ref nextPoint))
                {
                    //Get the next point in the path as a world position
                    rb.velocity = nextPoint - transform.position;
                    rb.velocity = rb.velocity.normalized;
                    rb.velocity *= speed;
                    isStationary = false;
                }
                else
                {
                    //End of the path!
                    rb.velocity = Vector3.zero;
                    isStationary = true;
                }
            }
            else
            {
                Vector3 delta = nextPoint - transform.position;
                if(delta.magnitude <= 0.2f)
                {
                    rb.velocity = Vector3.zero;
                    isStationary = true;
                }

            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            isStationary = true;
        }
    }

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
