using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures.Weapons
{
    public class BaseProjectile : MonoBehaviour
    {
        [SerializeField] protected float _speed;
        [SerializeField] private bool _invertX; //это для ракушки, т.к. без относительно героя проверка

        protected Rigidbody2D Rigibody; //
        protected int Direction;

        protected virtual void Start()
        {
            var mod = _invertX ? -1 : 1;//проверка для Ракушки стрельбы в правильную сторону
            Direction = mod * transform.lossyScale.x > 0 ? 1 : -1; //это относительно героя если глобавльный Scale > 0 то 1 иначе -1 (что бы летел в разные стороны)
            Rigibody = GetComponent<Rigidbody2D>(); //получить позицию объекта
        }

    }
}