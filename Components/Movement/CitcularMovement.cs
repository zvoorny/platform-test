using System;
using System.Collections;
using UnityEngine;

namespace PixelCrew.Components.Movement
{
    public class CitcularMovement : MonoBehaviour //класс для движения монет
    {
        [SerializeField] private float _radius = 1f;
        [SerializeField] private float _speed = 1f;
        private Rigidbody2D[] _bodies;
        private Vector2[] _position; //для отрисовки в Editor
        private float _time;

        private void Awake()
        {
            UpdateContent();
        }

        private void UpdateContent()
        {
            _bodies = GetComponentsInChildren<Rigidbody2D>();//получить rigibody для дочерних объектов
            _position = new Vector2[_bodies.Length];
        }

        private void Update()
        {
            CulculatePositions();
            var isAllDead = true;
            for (var i =0;i<_bodies.Length;i++)
            {
                if (_bodies[i])//проерка, если есть то работает если нет то skip
                {
                    _bodies[i].MovePosition(_position[i]);
                    isAllDead = false;
                }
                
            }
            if(isAllDead)//если нет ни одного рабочего то удалить надо
            {
                enabled = false;
                Destroy(gameObject, 1f);
            }
            _time += Time.deltaTime;
            
            
        }

        private void CulculatePositions()
        {
            var step = 2 * Mathf.PI / _bodies.Length;//шаг как будет растовлять по кругу
            Vector2 containerPosition = transform.position;//взять позицию родителя

            for (var i = 0; i < _bodies.Length; i++)
            {
                var angle = step * i;//угол
                var pos = new Vector2(Mathf.Cos(angle + _time * _speed) * _radius, Mathf.Sin(angle + _time * _speed) * _radius);

                _position[i] = containerPosition + pos;
            }
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            UpdateContent();
            CulculatePositions();
            for (var i = 0; i < _bodies.Length; i++)
            {
                _bodies[i].transform.position = _position[i];
            }

        }
        private void OnDrawGizmos()
        {
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, _radius);
        }

#endif

    }
}