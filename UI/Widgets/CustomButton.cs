using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Widgets
{
    public class CustomButton : Button //переписали на свою кнопку что бы передать текст
    {

        [SerializeField] private GameObject _normal;//текст
        [SerializeField] private GameObject _pressed;//текст

        protected override void DoStateTransition(SelectionState state, bool instant)//определения нажали или нет
        {
            base.DoStateTransition(state, instant);//когда меняем состояние срабатывает ниже

            _normal.SetActive(state != SelectionState.Pressed);//когда не нажали
            _pressed.SetActive(state == SelectionState.Pressed);//когда нажали
        }
    }
}
