using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace BattleSystem
{
    public class EnemyView : MonoBehaviour
    {
        public Image portrait;
        public Image healthBar;
        public TextMeshProUGUI healthText;
        private Enemy _enemy;
        public Enemy enemy
        {
            get => _enemy; set
            {
                if (_enemy != null)
                {
                    _enemy.OnTakeDamage -= RefillHealth;
                }
                _enemy = value;
                SetNewEnemy();
            }
        }

        private void OnDisable()
        {
            if (enemy != null)
            {
                enemy.OnTakeDamage -= RefillHealth;
            }
        }

        private void RefillHealth(Enemy enemy)
        {
            healthBar.fillAmount = this.enemy.currentHealth / this.enemy.data.maxHealth;
            healthText.text = this.enemy.currentHealth + "/" + this.enemy.data.maxHealth;
        }

        private void SetNewEnemy()
        {
            enemy.OnTakeDamage += RefillHealth;
            RefillHealth(enemy);
            portrait.sprite = enemy.data.portrait;
        }
    }
}
