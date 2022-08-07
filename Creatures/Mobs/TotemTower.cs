using PixelCrew.Components.Health;
using PixelCrew.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs
{
    public class TotemTower : MonoBehaviour //что бы стреляли по очереди
    {
        [SerializeField] private List<ShootingAI> _traps;//не потому что со временем будут убывать/уменьшаться
        [SerializeField] private Cooldown _cooldown;

        private int _currentTrap;

        private void Start()
        {
            foreach(var shootingTrapAI in _traps)
            {
                shootingTrapAI.enabled = false;
                var hp = shootingTrapAI.GetComponent<HealthComponent>();
                hp._onDie.AddListener(()=>OnTrapDead(shootingTrapAI));//замыкание, т.к. по дефолту _onDie пустой
            }
        }

        private void OnTrapDead(ShootingAI shootingAI)
        {
            var index = _traps.IndexOf(shootingAI);//находим индекс
            _traps.Remove(shootingAI);//удалить из списка
            if(index < _currentTrap)//если index меньше текущего
            {
                _currentTrap--;//минус 1 по индексу
            }
        }

        private void Update()
        {

            if(_traps.Count == 0)//если кол-во тотемов =0
            {
                enabled = false;//выключим что бы update не слал
                Destroy(gameObject, 1f);//удаляем через 1 сек что бы обработали анимации spawn и т.д.
            }

            var hasAnyTarget = _traps.Any(x => x._vision.isTouchingLayer);//если какая-то из башен будет в вижине, то получим true
            if(hasAnyTarget)
            {
                if (_cooldown.IsRedy)
                {
                    _traps[_currentTrap].Shoot();
                    _cooldown.Reset();
                    _currentTrap = (int)Mathf.Repeat(_currentTrap + 1, _traps.Count);
                }
            }
        }
    }
}