using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.Interactions
{
    public class InteractComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _action; //��� ������ � ������ � interact

        public void Interact() //����� � Hero �.�. �� ���������������
        {
            _action?.Invoke();
        }
    }
}
