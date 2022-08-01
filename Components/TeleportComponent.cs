using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private Transform _destTransform; //в какую точку

        public void Teleport(GameObject target) //какой объект перемещаем
        {
            target.transform.position = _destTransform.position;
        }
    }
}
