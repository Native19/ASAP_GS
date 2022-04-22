public class Damager
{
    private PlayerController _player;
    private int _damage { get; set; }

    public Damager(PlayerController player, int damage)
    {
        _player = player;
        _damage = damage;
    }

    public void DealDamage ()
    {
        _player.GetDamage(_damage);
    }
}
