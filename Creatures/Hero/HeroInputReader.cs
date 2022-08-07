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
            if (context.canceled) //����� ���������
            {
                _hero.Interact(); //����� ������
            }
        }

        public void OnAttack(InputAction.CallbackContext context) // action - space
        {
            if (context.canceled) //����� ���������
            {
                _hero.Attack(); //����� ������ �����
            }
        }

        public void OnThrow(InputAction.CallbackContext context) // action - shift
        {
            if (context.started) //����� ������
            {
                _hero.StartThrowing();
            }
            if(context.canceled)
            {
                _hero.PerformThownimg(); //����� ������ �������
            }
        }

        public void OnUse(InputAction.CallbackContext context) // action - space
        {
            if (context.performed) //����� ���������
            {
                _hero.UsePosition(); //����� ������ �����
            }
        }


    }
}
