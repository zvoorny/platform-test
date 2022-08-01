using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components.Interactions
{
    public class SwitchComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private bool _state; //статус двери
        [SerializeField] private string _animationKey; //проперти у аниматора вызывать


        public void Switch()
        {
            _state = !_state;//менять состояние
            _animator.SetBool(_animationKey, _state);//и пихать в аниматор соответствующий bool
        }

        [ContextMenu("Swich")]//отладчик
        public void SwitchIt()
        {
            Switch();
        }
    }
}
