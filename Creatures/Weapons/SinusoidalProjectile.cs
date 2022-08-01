using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures.Weapons
{
    public class SinusoidalProjectile : BaseProjectile //для синусодной атаки
    {
        [SerializeField] private float _frequency = 1f; //частота синуса
        [SerializeField] private float _amplitude = 1f; //амплитуда/высота синуса
        private float _originalY;
        private float _time;//что бы с одной точки плевался
        protected override void Start()
        {
            
            base.Start();
            _originalY = Rigibody.position.y;

        }

        private void FixedUpdate()
        {
            var position = Rigibody.position;
            position.x += Direction * _speed;
            position.y = _originalY + Mathf.Sin(_time* _frequency)* _amplitude;
            Rigibody.MovePosition(position);
            _time += Time.fixedDeltaTime;
        }
    }
}