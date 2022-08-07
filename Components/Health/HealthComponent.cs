using PixelCrew.Model;
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
        [SerializeField] public UnityEvent _onDie;
        [SerializeField] private HealthChangeEvent _onChange;

        


        public void ModifyHealth(int healthDelta)//передача дамага
        {
            if (_health <= 0) return;//проверка что бы не бить труп
            _health += healthDelta;//дамаг
           // Debug.Log($"{_session.Data.Hp.Value} coins added. Total coins: {_session.Data.Hp}");
            
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

        public void SetHealth(int health)
        {
            _health = health;
        }

        public int GetHealth()
        {
            return _health;
        }

        private void OnDestroy()//если где-то из кода вызыываем это, то что бы destroy object делать
        {
            _onDie.RemoveAllListeners();
        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int> { }

       

    }
}
