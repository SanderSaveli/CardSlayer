using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleSystem.Field
{
    public class EntityView : MonoBehaviour, IEntityView
    {
        [field: SerializeField] 
        public RectTransform view { get; set; }
        public Image portrait;
        public Image healthBar;
        public TextMeshProUGUI healthText;
        private IFieldEntity _entity;
        public IFieldEntity entity
        {
            get => _entity; set
            {
                if (_entity != null)
                {
                    UnsubscibeToAllEvents();
                }
                _entity = value;
                SetNewEntity();
            }
        }

        private void OnDisable()
        {
            if (entity != null)
            {
                UnsubscibeToAllEvents();
            }
        }

        private void SubscibeToAllEvents() 
        {
            entity.OnTakeDamage += RefillHealth;
            entity.OnDie += OnDie;
            entity.OnMove += ChangePosition;
        }
        private void UnsubscibeToAllEvents()
        {
            entity.OnTakeDamage -= RefillHealth;
            entity.OnDie -= OnDie;
            entity.OnMove -= ChangePosition;
        }
        private void RefillHealth(IFieldEntity enemy)
        {
            healthBar.fillAmount = (float)this.entity.currentHealth / this.entity.data.maxHealth;
            healthText.text = this.entity.currentHealth + "/" + this.entity.data.maxHealth;
        }

        private void ChangePosition(IFieldEntity enemy) 
        {
            RectTransform cellRt= enemy.ocupatedCells[0].cellView.rectTransform;
            view.SetParent(cellRt);
            view.anchoredPosition = cellRt.anchoredPosition;
            view.anchorMax = Vector2.one;
            view.anchorMin = Vector2.zero;
            view.sizeDelta = Vector2.zero;
            view.localScale = Vector3.one;
        }

        private void OnDie(IFieldEntity enemy) 
        {
        
        }

        private void SetNewEntity()
        {
            SubscibeToAllEvents();
            ChangePosition(entity);
            RefillHealth(entity);
            portrait.sprite = entity.data.portrait;
        }
    }
}
