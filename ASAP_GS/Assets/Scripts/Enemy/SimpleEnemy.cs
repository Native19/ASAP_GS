using UnityEngine;

public class SimpleEnemy : MonoBehaviour, IEnemyFollow
{
    [SerializeField] protected Transform _target;
    [SerializeField] protected float _speed = 10f;
    [SerializeField] protected int _damage = 10;
    
    [SerializeField] protected float _attackCooldown = 2f;
    protected float _attackCooldownTimer = 0;
    
    protected Rigidbody2D _rb;
    protected Damager _damager;
    protected HealthPoints _hp;

    public void Start()
    {
        if (!_rb)
            _rb = transform.GetComponent<Rigidbody2D>();
        _damager = new Damager(_target.GetComponent<PlayerController>(), _damage);
        _hp = new HealthPoints(5, new Death());
    }

    public virtual void Update()
    {
        EnemyFollow(_target);
        UpdateAttackCooldown();
    }

    //public EnemyMove(Transform target, float speed)
    //{
    //    _target = target;
    //    _speed = speed;
    //}
    public virtual void EnemyFollow(Transform target)
    {
        Vector2 targetDirection = (target.position - transform.position).normalized;
        _rb.velocity = targetDirection * _speed;

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

    public void GetHeal(int hp)
    {
        _hp.GetHeal(hp);
    }

    public void GetDamage(int damage)
    {
        _hp.GetDamage(damage);

        if (!_hp.IsAlife())
            Death();
    }

    private void Death()
    {
        Destroy(transform.gameObject);
    }

    protected virtual void Attack ()
    {
        if (_attackCooldownTimer > 0)
            return;

        _attackCooldownTimer = _attackCooldown;
        _damager.DealDamage();
    }

    protected virtual void UpdateAttackCooldown()
    {
        if (_attackCooldownTimer > 0)
            _attackCooldownTimer -= Time.deltaTime;
    }
}
