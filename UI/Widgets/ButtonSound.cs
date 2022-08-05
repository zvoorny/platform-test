using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PixelCrew.UI.Widgets
{
    public class ButtonSound : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AudioClip _audioClip; //какой звук проигрывать

        private AudioSource _source;

        public void OnPointerClick(PointerEventData eventData) //в котором будет проигровать звук
        {
            if(_source == null)
            {
                _source = GameObject.FindWithTag("SfxAudioSource").GetComponent<AudioSource>(); //с каким тэгом проигрывать - нашли объект и нашил AudioSource
            }
            _source.PlayOneShot(_audioClip);//проиграть 1 раз
        }
    }
}