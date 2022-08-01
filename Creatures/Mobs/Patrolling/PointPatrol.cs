using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs.Patrolling
{
    public class PointPatrol : Patrol //наследуем от абстрактого класса
    {
        [SerializeField] private Transform[] _points; // массив точек
        [SerializeField] private float _treshold = 1f;// 

        private Creature _creature;
        private int _destinationPointIndex;

        private void Awake()
        {
            _creature = GetComponent<Creature>(); //т.к. во время патрулирования двигать будем
        }
        public override IEnumerator DoPatrol()
        {
            while(enabled) //пока компонент включен
            {
                if(IsOnPoint()) //если дошли до точки, то должны перейти на лед. точку
                {
                    _destinationPointIndex = (int)Mathf.Repeat(_destinationPointIndex + 1, _points.Length);//прибовляя, когда дойдем до конца начнем с 0
                }

                var direction = _points[_destinationPointIndex].position - transform.position;//двигать от позиции текущий куда надо
                direction.y = 0;
                _creature.SetDirection(direction.normalized);

                yield return null;
            }    
        }

        private bool IsOnPoint()//сравнить нашу позицию текущую и до точки, найти длину и понять > или < нашего значения
        {
            return (_points[_destinationPointIndex].position - transform.position).magnitude < _treshold;
                //magnitude длина вектора
        }
    }
}