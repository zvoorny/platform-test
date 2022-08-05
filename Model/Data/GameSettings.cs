using PixelCrew.Model.Data.Properies;
using System;
using System.Collections;
using UnityEngine;

namespace PixelCrew.Model.Data
{
    [CreateAssetMenu(menuName = "Data/GameSettings", fileName = "GameSettings")]//появился в  Utnity при нажатии на правую кнопку
    public class GameSettings : ScriptableObject//для сохранения настроект в settings
    {
        [SerializeField] private FLoatPersisterProperty _music;//для сохранения найсстрйки музыки
        [SerializeField] private FLoatPersisterProperty _sfx;//для сохранения найсстрйки sfx

        public FLoatPersisterProperty Music => _music;
        public FLoatPersisterProperty Sfx => _sfx;

        private static GameSettings _instance;//статическая переменная
        public static GameSettings I => _instance ==null ? LoadGameSettings() : _instance;//доступ к ней

        private static GameSettings LoadGameSettings()
        {
            return _instance = Resources.Load<GameSettings>("GameSettings");//из ресурсов загрузили GameSettings
        }

        private void OnEnable()
        {
            _music = new FLoatPersisterProperty(1, SoundSetting.Music.ToString());
            _sfx = new FLoatPersisterProperty(1, SoundSetting.Sfx.ToString());
        }

        private void OnValidate()//для изменения в GameSettings в Unity что бы тоже применялись изменения
        {
            Music.Validate();
            Sfx.Validate();
        }
    }

    public enum SoundSetting
    {
        Music,
        Sfx
    }
}