using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ability
{
    protected Vector2 _attackPoint;
    protected float _overlapRadius = 2;
    protected int _damage = 100;

    public Ability (Vector2 attackPoint, float overlapRadius, int damage)
    {
        _attackPoint = attackPoint;
        _overlapRadius = overlapRadius;
        _damage = damage;   
    }
    public virtual void Use ()
    {
        Action();
    }

    protected virtual void Action ()
    {
        List<GameObject> collidObjects = Physics2D.OverlapCircleAll(_attackPoint, _overlapRadius)
            .ToList()
            .ConvertAll(collider => collider.gameObject);

        foreach (GameObject collision in collidObjects)
        {
            try
            {
                SimpleEnemy enemy = collision.transform.GetComponent<SimpleEnemy>();
                enemy.GetDamage(_damage);
            }
            catch
            {

            }
        }
        
    }

    public void UpdateAbility(Vector3 attackPoint)
    {
        _attackPoint = attackPoint;
    }
}
