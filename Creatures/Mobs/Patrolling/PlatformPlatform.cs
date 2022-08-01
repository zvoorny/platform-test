using PixelCrew.Components.ColliderBased;
using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs.Patrolling
{
    public class PlatformPlatform : Patrol
    {
        [SerializeField] private LayerCheck _groundCheck;//чекать
        [SerializeField] private LayerCheck _obstractCheck;//чекать другие объекты что бы разворачивался например бочка
        [SerializeField] private int _direction;//куда идти
        [SerializeField] private Creature _creature;
        public override IEnumerator DoPatrol()//наследование 
        {
            while(enabled)
            {
                if(_groundCheck.isTouchingLayer && !_obstractCheck.isTouchingLayer)//если касаемся зщемли
                {
                    _creature.SetDirection(new Vector2(_direction, 0));//идти по X
                }
                else
                {
                    _direction = -_direction;
                    _creature.SetDirection(new Vector2(_direction, 0));
                }
                yield return null;
            }
        }
    }
}