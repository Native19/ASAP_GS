using UnityEngine;

public class Move : MonoBehaviour, IMove
{
    private float _speed = 10f;
    private bool _isMooving = false;
    private Rigidbody2D _rb;
    private bool _isMovingRight = true;
    
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

    public void Run(Vector2 normalizeVelocity)
    {
        _rb.velocity = new Vector2 (normalizeVelocity.x * _speed, _rb.velocity.y);

        _isMooving = normalizeVelocity.magnitude > 0.1;
    }

    public void Dash(Vector2 normalizeVelocity)
    {
        if (!_isDashPossible)
            return;

        Vector2 horizontalVelocity = Vector2.zero;
        horizontalVelocity.x = _isMovingRight ? 1 : -1;


        _rb.AddForce(horizontalVelocity * _dashForce, ForceMode2D.Impulse);
        _animator.SetBool("isDash", true);

        _isDashPossible = false;
        _isDashInProcess = true;
        _dashProcessTimer = _dashDuration;
        _dashColldownTimer = _dashCooldown;
    }

    public void UpdateDash(bool isMovingRight)
    {
        _isMovingRight = isMovingRight;

        if (_dashColldownTimer > 0)
        {
            _dashColldownTimer -= Time.deltaTime;
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

