using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


namespace PixelCrew.Components.ColliderBased
{
    public class EnterColisionComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        //[SerializeField] private EnterEvent _action;//� ��� ������� - ��� �����
        [SerializeField] private EnterEvent _action;//� ��� ������� - ��� ������
        private void OnCollisionEnter2D(Collision2D other)//���� �� �������
        {
            if (other.gameObject.CompareTag(_tag))
            {
                _action?.Invoke(other.gameObject);//� ��� �������
            }
        }

        [Serializable]
        public class EnterEvent : UnityEvent<GameObject>
        {
        }
    }
}
