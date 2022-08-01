using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Creatures.Weapons
{
    public class Profectly : BaseProjectile //заставлять лететь наш меч
    {
     
        protected override void Start()
        {
            base.Start();
            var force = new Vector2(Direction * _speed, 0);//через dynamic
            Rigibody.AddForce(force, ForceMode2D.Impulse);//через dynamic
        }

        /*что бы летел вечно, то rigibody - kinematic
        что бы падал, то дабавить силу - dynamic*/
        /*private void FixedUpdate()
        {
            var position = _rigibody.position;
            position.x += _direction * _speed;
            _rigibody.MovePosition(position); //двигаем через Rigibody, так как он есть, если не было бы то через transform
        }*/
    }
}
