using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components.GameObjectBased
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDestroy;
        public void DestroyObjects()
        {
            Destroy(_objectToDestroy);
        }
    }
}