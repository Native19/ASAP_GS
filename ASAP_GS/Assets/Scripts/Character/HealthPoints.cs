using UnityEngine;

public class HealthPoints
{
    private int _maxHealth, _nowhHealth;
    private bool _isImmunity = false;
    private float _immunityTimer;
    private IDeath _death;

    public void SetImmunity(float immunityDuration)
    {
        _isImmunity = true;
        _immunityTimer = immunityDuration;
    }

    public bool GetImmunity ()
    {
        return _isImmunity;
    }

    public HealthPoints (int maxHealth, IDeath death)
    {
        _maxHealth = maxHealth;
        _nowhHealth = maxHealth;
        _death = death;
    }
    public void UpdateImmunityTimer ()
    {
        if (!_isImmunity)
            return;

        _immunityTimer -= Time.deltaTime;
        if (_immunityTimer < 0)
            _isImmunity = false;
    }
    public int GetHealth()
    {
        return _nowhHealth;
    }

    public bool IsAlive()
    {
        return _nowhHealth > 0;
    }

    public void GetHeal (int hp)
    {
        _nowhHealth += hp;

        if (_nowhHealth > _maxHealth)
            _nowhHealth = _maxHealth;
    }

    public void GetDamage (int damage)
    {
        if (_isImmunity)
            return;

        _nowhHealth -= damage;
        
        if (_nowhHealth < 0)
        {
            _nowhHealth = 0;
            Death();
        }
    }

    private void Death ()
    {
        _death.Death();
    }
}
