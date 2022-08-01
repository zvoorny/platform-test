using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.Interactions
{
    public class InteractComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _action; //что делать в случае с interact

        public void Interact() //вызов в Hero т.к. он взаимодействует
        {
            _action?.Invoke();
        }
    }
}
