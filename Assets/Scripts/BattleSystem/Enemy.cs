using BattleSystem;
using UnityEngine;

public class Enemy
{
    #region Events
    public delegate void TakeDamage(Enemy enemy);
    public event TakeDamage OnTakeDamage;

    public event TakeDamage OnDie;
    #endregion
    public EnemyData data;
    public int currentHealth { get; private set; }

    public Enemy(EnemyData data)
    {
        this.data = data;
        currentHealth = data.maxHealth;
    }

    public void MakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, data.maxHealth);
        OnTakeDamage?.Invoke(this);
        if (currentHealth <= 0)
        {
            OnDie?.Invoke(this);
        }
    }
}
