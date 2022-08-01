using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health; //HP
        [SerializeField] private UnityEvent _onDamage; //получение урона
        [SerializeField] private UnityEvent _onHeal; //получение здоровья
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private HealthChangeEvent _onChange;

        public void ModifyHealth(int healthDelta)//передача дамага
        {
            if (_health <= 0) return;//проверка что бы не бить труп
            _health += healthDelta;//дамаг
            _onChange?.Invoke(_health); //после того как изменили здоровье будем вызывать ChangeEvent

            if (healthDelta < 0)
            {
                _onDamage?.Invoke();
            }
            else if (healthDelta > 0)
            {
                _onHeal?.Invoke();
            }

            if (_health <=0)
            {
                _onDie?.Invoke();
            }

        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int> { }

        public void SetHealth(int health)
        {
            _health = health;
        }

    }
}
