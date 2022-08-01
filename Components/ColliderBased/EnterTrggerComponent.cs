using PixelCrew.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static PixelCrew.Components.ColliderBased.EnterColisionComponent;

namespace PixelCrew.Components.ColliderBased
{
    public class EnterTrggerComponent : MonoBehaviour
    {
        [SerializeField] private string _tag; //чекать по тэгам
        [SerializeField] private LayerMask _layer=~0 /*everething*/; //та же чекать по слоям
        //[SerializeField] private UnityEvent _action;
        [SerializeField] private EnterEvent _action;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.IsInLayer(_layer)) return; //объект не в этом слоя то прерываем
            if (!string.IsNullOrEmpty(_tag)/*пусто*/ && !other.gameObject.CompareTag(_tag)) return;//если не пустой таг и не совпадает то прерываем
            
            _action?.Invoke(other.gameObject);
        }

    }
}