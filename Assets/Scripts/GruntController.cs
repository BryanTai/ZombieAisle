using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntController : MonoBehaviour
{
    public int HitPoints = 3;
    public float Speed = 1.0f;

    private SimplePF2D.Path _path;
    private Rigidbody2D _rb;
    private Vector3 _nextPoint;
    private bool _isStationary;
    private Vector3 _endGoalPosition;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _nextPoint = Vector3.zero;
    }

    public void Initialize(SimplePathFinding2D pathFinding2D, Vector3 endGoalPosition)
    {
        _path = new SimplePF2D.Path(pathFinding2D);
        _endGoalPosition = endGoalPosition;

        _path.CreatePath(transform.position, _endGoalPosition);
    }

    private void Update()
    {
        if(_path.IsGenerated())
        {
            if(_isStationary)
            {
                if(_path.GetNextPoint(ref _nextPoint))
                {
                    //Get the next point in the path as a world position
                    _rb.velocity = _nextPoint - transform.position;
                    _rb.velocity = _rb.velocity.normalized;
                    _rb.velocity *= Speed;
                    _isStationary = false;
                }
                else
                {
                    //End of the path!
                    _rb.velocity = Vector3.zero;
                    _isStationary = true;
                }
            }
            else
            {
                Vector3 delta = _nextPoint - transform.position;
                if(delta.magnitude <= 0.2f)
                {
                    _rb.velocity = Vector3.zero;
                    _isStationary = true;
                }

            }
        }
        else
        {
            _rb.velocity = Vector3.zero;
            _isStationary = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO hit by projectile?
    }

    public void TakeDamage(int damage)
    {
        HitPoints -= damage;

        if(HitPoints <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        Destroy(this.gameObject);
    }
}
