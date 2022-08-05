using System.Collections;
using UnityEngine;

namespace PixelCrew.UI
{
    public class AnimationWindow : MonoBehaviour //обрабатывать поведение анимации
    {

        private Animator _animator;
        private static readonly int Show = Animator.StringToHash("Show");//взять у аниматора переменную
        private static readonly int Hide = Animator.StringToHash("Hide");//взять у аниматора переменную

        protected virtual void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.SetTrigger(Show);//на старте передать Show
        }
        public void Close()
        {
            _animator.SetTrigger(Hide);//при закрытии передать Hide
        }

        public virtual void OnCloseAnimationComplete()//при завершении закрыть
        {
            Destroy(gameObject);
        }
    }
}