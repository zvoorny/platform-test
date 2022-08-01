using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor; //удаляется в процессе сборки
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.ColliderBased
{
    public class CheckCircleOverLap : MonoBehaviour
    {

        [SerializeField] private float _raius = 1f;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private OnOverlapEvent _onOverlap;
        [SerializeField] private string[] _tags;
        private Collider2D[] _interactionResult = new Collider2D[10];

        internal void Check()
        {
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, _raius, _interactionResult,_mask); //получить пересекающиейся объекты - возвращяет кол-во результатов

            for (var i = 0; i < size; i++)
            {
                var isInTags = _tags.Any(tag => _interactionResult[i].CompareTag(tag));//возвращает true если хотя бы 1 true //проверка tag
                if (isInTags)//если true один элемент пересекает тэг одного из перечисленных, то вызываем _onOverlap
                {
                    _onOverlap?.Invoke(_interactionResult[i].gameObject);
                    //создавать массив GameObject те которые вошли вокруг объекта, а потом эти объекты будем
                    //разбирать в файле HeroOne.cs
                }
            }
        }

        [Serializable]
        public class OnOverlapEvent : UnityEvent<GameObject>
         {


         }
    }
}