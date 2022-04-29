using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Move _mover;
    [SerializeField] private float _speed = 10f;
    private bool _isMovingRight = true;

    [SerializeField] private int _dashForce = 50;
    [SerializeField] private float _dashImmunityDuration = 0.5f;
    [SerializeField] private float _dashCooldown = 2f;

    private IJump _jumper;
    [SerializeField] private float _jumpForce = 50f;
    [SerializeField] private Transform _groundCollider;
    [SerializeField] private LayerMask _LayerMask;
    [SerializeField] private float _jumpCoolDown = 0.5f;

    private HealthPoints _hp;
    [SerializeField] private int _maxHP = 3;

    private List<Ability> _abilites = new List<Ability>();
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Transform _particalAttack;
    [SerializeField] private float _meleeAttackRange = 1;

    private Rigidbody2D _rb;
    private Animator _anim;

    [SerializeField] private string _thisScene;

    private void Awake()
    {
        _anim = transform.GetComponent<Animator>();
        if (transform.GetComponent<Rigidbody2D>())
            _rb = transform.GetComponent<Rigidbody2D>();
        else
            _rb = transform.gameObject.AddComponent<Rigidbody2D>();

        //_hp = new HealthPoints(_maxHP, new Death()); // ToDo: ������� ������
        _mover = new Move(_speed, _dashForce, _dashCooldown, _rb, _anim);
        _jumper = new Jump(_jumpForce, _groundCollider, _LayerMask, _rb, _anim);

        _rb.sleepMode = 0;

        Ability ability = new Ability(_attackPoint.position, _meleeAttackRange, 100, _anim);
        ParticalAbility ability2 = new ParticalAbility(_attackPoint.position, _meleeAttackRange, 100, _particalAttack, _anim);
        _abilites.Add(ability);
        _abilites.Add(ability2);
    }

    void Start()
    {
    }

    void Update()
    {
        if (!_hp.IsAlive())
        {
            SceneChanger.ChangeScene(_thisScene);
        }

        _hp.UpdateImmunityTimer();
        _mover.UpdateDash(_isMovingRight);
        _abilites.ForEach(ability => ability.UpdateAbility(_attackPoint.position, _isMovingRight));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumper.Jump();
        }

        Vector2 normalizeHorizontalVelocity = new Vector2(Input.GetAxis("Horizontal"), _rb.velocity.y);
        _anim.SetFloat("XVelocity", Mathf.Abs(normalizeHorizontalVelocity.x));
        _anim.SetFloat("YVelocity", Mathf.Abs(normalizeHorizontalVelocity.y));
        Reflect(normalizeHorizontalVelocity.x);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _mover.Dash(normalizeHorizontalVelocity);
            _hp.SetImmunity(5);
        }

        if (normalizeHorizontalVelocity.magnitude != 0 && !_mover.IsDashActive())
            _mover.Run(normalizeHorizontalVelocity);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _abilites[0].Use();
        }    

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _abilites[1].Use();
        }

    }

    public void GetHeal(int hp)
    {
        _hp.GetHeal(hp);
    }

    public void GetDamage(int damage)
    {
        _hp.GetDamage(damage);
        Debug.Log(_hp.GetHealth());
    }

    private void Reflect(float XVelocity)
    {
        if ((XVelocity > 0 && !_isMovingRight) || (XVelocity < 0 && _isMovingRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            _isMovingRight = !_isMovingRight;
        }
    }

    public int GetCurrentHealth()
    {
        return _hp.GetHealth();
    }

    public int GetMaxHealth()
    {
        return _maxHP;
    }

    public void OverAttack()
    {
        _anim.SetBool("isAttack", false);
    }

    public void OverJump()
    {
        _anim.SetBool("isJump", false);
    }
}
