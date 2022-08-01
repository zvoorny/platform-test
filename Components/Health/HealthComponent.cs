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
        [SerializeField] private UnityEvent _onDamage; //��������� �����
        [SerializeField] private UnityEvent _onHeal; //��������� ��������
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private HealthChangeEvent _onChange;

        public void ModifyHealth(int healthDelta)//�������� ������
        {
            if (_health <= 0) return;//�������� ��� �� �� ���� ����
            _health += healthDelta;//�����
            _onChange?.Invoke(_health); //����� ���� ��� �������� �������� ����� �������� ChangeEvent

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
