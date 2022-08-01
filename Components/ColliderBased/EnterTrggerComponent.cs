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
        [SerializeField] private string _tag; //������ �� �����
        [SerializeField] private LayerMask _layer=~0 /*everething*/; //�� �� ������ �� �����
        //[SerializeField] private UnityEvent _action;
        [SerializeField] private EnterEvent _action;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.IsInLayer(_layer)) return; //������ �� � ���� ���� �� ���������
            if (!string.IsNullOrEmpty(_tag)/*�����*/ && !other.gameObject.CompareTag(_tag)) return;//���� �� ������ ��� � �� ��������� �� ���������
            
            _action?.Invoke(other.gameObject);
        }

    }
}