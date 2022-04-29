using UnityEngine;

public class Move : MonoBehaviour, IMove
{
    private float _speed = 10f;
    private bool _isMooving = false;
    private Rigidbody2D _rb;
    private bool _isLeft = false;
    private bool _isIdle = true;
    
    private int _dashForce = 20;
    private float _dashCooldown = 2f;
    private float _dashDuration = 0.5f;
    private bool _isDashPossible = true;
    private bool _isDashInProcess = false;
    private float _dashColldownTimer;
    private float _dashProcessTimer;

    private Animator _animator;

    public bool IsMooving() => _isMooving;
    public bool IsDashActive() => _isDashInProcess;

    public Move(float speed, int dashForce, float dashCooldown, Rigidbody2D rb, Animator animator)
    {
        _speed = speed;
        _dashForce = dashForce;
        _dashCooldown = dashCooldown;
        _rb = rb;
        _animator = animator;
    }
    //private void AddVelocity ()
    //{
    //    Vector2 normalizeHorizontalVelocity = new Vector2(Input.GetAxis("Horizontal"), 0);
    //    _rb.velocity = normalizeHorizontalVelocity * _speed;

    //    _isMooving = normalizeHorizontalVelocity.magnitude > 0.1;
    //    _isLeft = normalizeHorizontalVelocity.x < -0.1;
    //}

    public void Run(Vector2 normalizeVelocity)
    {
        _rb.velocity = new Vector2 (normalizeVelocity.x * _speed, _rb.velocity.y);

        _isMooving = normalizeVelocity.magnitude > 0.1;
        _isLeft = normalizeVelocity.x < -0.1;
    }

    public void Dash(Vector2 normalizeVelocity)
    {
        if (!_isDashPossible)
            return;

        Vector2 horizontalVelocity = Vector2.zero;
        horizontalVelocity.x = Mathf.Sign(normalizeVelocity.x);

        _rb.AddForce(horizontalVelocity * _dashForce, ForceMode2D.Impulse); // dash в сторону последнего направления??????????
        _animator.SetBool("isDash", true);

        _isDashPossible = false;
        _isDashInProcess = true;
        _dashProcessTimer = _dashDuration;
        _dashColldownTimer = _dashCooldown;
    }

    public void UpdateDash()
    {
        //Debug.Log(_dashColldownTimer);
        if (_dashColldownTimer > 0)
        {
            _dashColldownTimer -= Time.deltaTime;
            //if (_dashColldownTimer <= 0)
            //    _isDashPossible = false;
        }
        else
        {
            _isDashPossible = true;
        }

        if (_dashProcessTimer > 0)
        {
            _dashProcessTimer -= Time.deltaTime;
            if (_dashProcessTimer <= 0)
            {
                _isDashInProcess = false;
                _animator.SetBool("isDash", false);
            }
        }

    }
}

