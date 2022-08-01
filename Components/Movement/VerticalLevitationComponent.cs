using System.Collections;
using UnityEngine;

namespace PixelCrew.Components.Movement
{
    public class VerticalLevitationComponent : MonoBehaviour
    {
        [SerializeField] private float _frequency = 1f; //частота синуса
        [SerializeField] private float _amplitude = 1f; //амплитуда/высота синуса
        [SerializeField] private bool _randomize;//если нужно рандомизировать

        private float _originalY;
        private Rigidbody2D _rigibody;
        private float _seed;//для радномности движения

        private void Awake()
        {
            _rigibody = GetComponent<Rigidbody2D>();//компонент Rigibody2D
            _originalY = _rigibody.transform.position.y;//начальную точку Y
            if(_randomize)
            {
                _seed = Random.value /*рандомное значение от 0 до 1*/ * Mathf.PI * 2/*нам нужны радианы 2ПИ это целый круг*/;
            }
        }

        private void Update()
        {
            var pos = _rigibody.position;
            pos.y = _originalY + Mathf.Sin(_seed+Time.time * _frequency) * _amplitude;
            _rigibody.MovePosition(pos);
        }
    }
}