using PixelCrew.Creatures;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew.Creatures.Hero
{

    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private HeroOne _hero;


        public void OnHorizotMovement(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }


        public void OnInteract(InputAction.CallbackContext context) // action - E
        {
            if (context.canceled) //когда отпустили
            {
                _hero.Interact(); //вызов метода
            }
        }

        public void OnAttack(InputAction.CallbackContext context) // action - space
        {
            if (context.canceled) //когда отпустили
            {
                _hero.Attack(); //вызов метода атаки
            }
        }

        public void OnThrow(InputAction.CallbackContext context) // action - shift
        {
            if (context.started) //когда нажали
            {
                _hero.StartThrowing();
            }
            if(context.canceled)
            {
                _hero.PerformThownimg(); //вызов метода метания
            }
        }

        public void OnUse(InputAction.CallbackContext context) // action - space
        {
            if (context.performed) //когда отпустили
            {
                _hero.UsePosition(); //вызов метода атаки
            }
        }


    }
}
