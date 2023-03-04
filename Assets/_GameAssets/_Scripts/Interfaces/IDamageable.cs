public interface IHaveHP
{
    public int Health { get; set; }
}

public interface IDamageable : IHaveHP
{
    public void TakeDamage(int damage);
}

