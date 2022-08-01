using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class ProbabilityDropComponent : MonoBehaviour
    {
        [SerializeField] private int _count; //кол-во выпадающих элементов
        [SerializeField] private DropData[] _drop;
        [SerializeField] private DropEvent _onDropCalculeted;
        [SerializeField] private bool _spawnOnEnable;

        private void OnEnable()
        {
            if(_spawnOnEnable)
            {
                CalculateDrop();
            }
        }

        [ContextMenu("CalculateDrop")]
        public void CalculateDrop()//расщитывать
        {
            var itemsToDrop = new GameObject[_count];
            var itemCount =  0;
            var total = _drop.Sum(dropData => dropData.Probality);
            var sorterDrop = _drop.OrderBy(dropData => dropData.Probality);

            while(itemCount<_count) //вероятность
            {
                var random = UnityEngine.Random.value * total;
                var current = 0f;
                foreach(var  dropData in sorterDrop)
                {
                    current += dropData.Probality;
                    if(current >= random)
                    {
                        itemsToDrop[itemCount] = dropData.drop;
                        itemCount++;
                        break;
                    }
                }
            }

            _onDropCalculeted?.Invoke(itemsToDrop);//елементы которые викинуть в Unity
        }

        [Serializable]
        public class DropData
        {
            public GameObject drop;//объект на что
            [Range(0f,100f)]public float Probality;//рандомность
        }

        [Serializable]
        public class DropEvent : UnityEvent<GameObject[]>
        { }
    }
}