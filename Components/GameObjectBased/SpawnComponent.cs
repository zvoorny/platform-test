using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components.GameObjectBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;
        //[SerializeField] private bool _invertXScale;

        [ContextMenu("Spawn")] //для теста
        public void Spawn()
        {
            /*var modifier = _invertXScale ? -1 : 1;*/
            var instance = Instantiate(_prefab,_target.position,Quaternion.identity); //инициализация появления пыли / GameObject, позиция, и поворот
            instance.transform.localScale/*у текущекго объекта*/ = _target.lossyScale;/*у родительского класса берем*/
            instance.SetActive(true);
        }
    }
}
