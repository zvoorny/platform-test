using PixelCrew.Model.Data;
using PixelCrew.UI.Widgets;
using System.Collections;
using UnityEngine;

namespace PixelCrew.UI.Settings
{
    public class SettingWindow : AnimationWindow
    {
        [SerializeField] private AudioSettingsWidget _music;//передаем 2 виджета
        [SerializeField] private AudioSettingsWidget _sfx;
        protected override void Start()
        {
            base.Start();

            _music.SetModel(GameSettings.I.Music);//передаем модельки
            _sfx.SetModel(GameSettings.I.Sfx);//передаем модельки
        }

    }
}