using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour, IEnemyFollow
{
    [SerializeField] protected Transform _target;
    [SerializeField] protected float _speed = 10f;
    [SerializeField] protected int _damage = 10;
    
    [SerializeField] protected float _attackCooldown = 2f;
    public float _attackCooldownTimer = 0;
    
    protected Rigidbody2D _rb;
    protected Damager _damager;
    protected HealthPoints _hp;
    [SerializeField] protected Animator _animator;

    public delegate void DieAction();
    public static event DieAction OnDie;

    public void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Start()
    {
        if (!_rb)
            _rb = transform.GetComponent<Rigidbody2D>();
        _damager = new Damager(_target.GetComponent<PlayerController>(), _damage);
        _hp = new HealthPoints(5, new Death());
        _animator = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        EnemyFollow(_target);
        UpdateAttackCooldown();
        Attack();
    }

    public virtual void EnemyFollow(Transform target)
    {
        if (Mathf.Abs(target.position.x - transform.position.x) < 0.5)
        {
            _rb.velocity = Vector2.zero;
            return; 
        }

        Vector2 targetDirection = (target.position - transform.position).normalized;
        _rb.velocity = targetDirection * _speed;

        transform.localScale = new Vector3(
            -Mathf.Sign(target.position.x - transform.position.x) * Mathf.Abs(transform.localScale.x),
            transform.localScale.y,
            transform.localScale.z);

        float angle;
        if (Mathf.Sign(_target.position.x - transform.position.x) < 0)
            angle = (Mathf.Atan2(_target.position.y - transform.position.y, _target.position.x - transform.position.x) - 180) * Mathf.Rad2Deg;
        else
            angle = (Mathf.Atan2(_target.position.y - transform.position.y, _target.position.x - transform.position.x)) * Mathf.Rad2Deg;

        Quaternion quaternion = Quaternion.Euler(0, 0, angle);
        transform.rotation = quaternion;
        //Vector3 lerpPosition = Vector3
        //.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        //.Lerp(transform.position, target.position, _speed * Time.deltaTime);

        //transform.position = lerpPosition;
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform == _target)
            Attack();           
    }

    public virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform == _target)
            Attack();
    }

    public void GetHeal(int hp)
    {
        _hp.GetHeal(hp);
    }

    public void GetDamage(int damage)
    {
        _hp.GetDamage(damage);

        if (!_hp.IsAlive())
            Death();
    }

    private void Death()
    {
        OnDie();
        Destroy(transform.gameObject);
    }

    protected virtual void Attack ()
    {
        if (_attackCooldownTimer > 0)
            return;
        Collider2D aggressiveCollider = GetComponent<Collider2D>();
        List<Collider2D> collisions = new List<Collider2D>();
        aggressiveCollider.OverlapCollider(new ContactFilter2D().NoFilter(), collisions);
        List<GameObject> gameObjectsInCollider = collisions.ConvertAll(collision => collision.gameObject);

        if (gameObjectsInCollider.Contains(_target.gameObject))
        {
            _attackCooldownTimer = _attackCooldown;
            _damager.DealDamage();
        }
    }

    protected virtual void UpdateAttackCooldown()
    {
        if (_attackCooldownTimer > 0)
            _attackCooldownTimer -= Time.deltaTime;
    }
}
