using PixelCrew.Model.Data;
using PixelCrew.Model.Data.Properies;
using System;
using System.Collections;
using UnityEngine;

namespace PixelCrew.Components.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSettingComponent : MonoBehaviour
    {
        
        [SerializeField] private SoundSetting _mode;
        private AudioSource _source;//что будем менять менять через getComponent
        private FLoatPersisterProperty _model;

        private void Start()
        {
            _source = GetComponent<AudioSource>();//получаем что будем менять

            _model = FindProperty();//находим property
            _model.onChanged += OnSoundSettingChanged;//подписываемся
            OnSoundSettingChanged(_model.Value, _model.Value);//вызовим что бы сразу проставить значения
        }

        private void OnSoundSettingChanged(float newValue, float oldValue)
        {
            _source.volume = newValue;
        }

        private FLoatPersisterProperty FindProperty()
        {
            switch (_mode)//выбор по моду - если находим то возвращяем
            {
                case SoundSetting.Music:
                    return GameSettings.I.Music;
                case SoundSetting.Sfx:
                    return GameSettings.I.Sfx;
            }
            throw new ArgumentException("Undefined mode");//если не находим то ошибка
        }

        private void OnDestroy()
        {
            _model.onChanged -= OnSoundSettingChanged;//отписываемся от всех событий
        }
    }
}