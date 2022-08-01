using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Component.Audio
{
    public class PlaySoundsComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;//
        [SerializeField] private AudioData[] _sounds;//массив звуков

        public void Play(string id)
        {
            foreach(var audioData in _sounds)
            {
                if (audioData.Id != id) continue;//если не равно, то пропускаем
                

                _source.PlayOneShot(audioData.Clip);//иначе проигрываем PlayOneShot  
                break;//прерываем цикл
                

            }
            
        }

        [Serializable]
        public class AudioData//массив звуков
        {
            [SerializeField] private string _id;
            [SerializeField] private AudioClip _clip;

            public string Id => _id;//для безопасности приватные но открыть доступ
            public AudioClip Clip => _clip;
        }
    }
}
