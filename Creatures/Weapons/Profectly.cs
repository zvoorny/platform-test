using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Creatures.Weapons
{
    public class Profectly : BaseProjectile //���������� ������ ��� ���
    {
     
        protected override void Start()
        {
            base.Start();
            var force = new Vector2(Direction * _speed, 0);//����� dynamic
            Rigibody.AddForce(force, ForceMode2D.Impulse);//����� dynamic
        }

        /*��� �� ����� �����, �� rigibody - kinematic
        ��� �� �����, �� �������� ���� - dynamic*/
        /*private void FixedUpdate()
        {
            var position = _rigibody.position;
            position.x += _direction * _speed;
            _rigibody.MovePosition(position); //������� ����� Rigibody, ��� ��� �� ����, ���� �� ���� �� �� ����� transform
        }*/
    }
}
