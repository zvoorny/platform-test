using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components.Health
{
    public class ModifyHelthComponent : MonoBehaviour
    {
        [SerializeField] private int _hpDelta; //дамаг который будет наносить



        public void ApplyDamage(GameObject target)//нанесение урона. Передача объекта  EnterColisionComponent -> _action?.Invoke(other.gameObject);//с кем контакт
        {
            var healthComponent = target.GetComponent<HealthComponent>();//получаем значение переменной
            if (healthComponent!= null)
            {
                healthComponent?.ModifyHealth(_hpDelta); //передача дамага 
            }
        }
    }
}
