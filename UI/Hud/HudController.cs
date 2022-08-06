using PixelCrew.Model;
using PixelCrew.Model.Definitions;
using PixelCrew.UI.Widgets;
using System;
using System.Collections;
using UnityEngine;

namespace PixelCrew.UI.Hud
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWindget _healthBar;//прокинуть
        private GameSession _session;
        private void Start()
        {
            _session = FindObjectOfType<GameSession>();//на старте получаем сессию
            _session.Data.Hp.onChanged += OnHealthChanged;//подписываемся на изменения
            OnHealthChanged(_session.Data.Hp.Value, 0);//запустить первый раз
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {
            var maxHealth = DefsFacade.I.Player.MaxHealth;
            var value = (float)newValue / maxHealth;//значение прогресс бара нормальзированое от 0 до 1
            _healthBar.SetProgress(value);
        }


    }
}