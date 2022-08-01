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
        //[SerializeField] private EnterEvent _action;//с кем контакт - так можно
        [SerializeField] private EnterEvent _action;//с кем контакт - так нельзя
        private void OnCollisionEnter2D(Collision2D other)//урон от колючек
        {
            if (other.gameObject.CompareTag(_tag))
            {
                _action?.Invoke(other.gameObject);//с кем контакт
            }
        }

        [Serializable]
        public class EnterEvent : UnityEvent<GameObject>
        {
        }
    }
}
