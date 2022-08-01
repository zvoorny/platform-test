using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.GameObjectBased;
using PixelCrew.Utils;
using System;
using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs
{
    public class ShootingTrapAI : MonoBehaviour
    {
        [SerializeField] private ColliderCheck _vision; //проверка доступности

        [Header("Melee")]
        [SerializeField] private Cooldown _meleeColldown; //перезарядка
        [SerializeField] private CheckCircleOverLap _meleeAttack;//кусать
        [SerializeField] private ColliderCheck _meleeCanAttack; //проверка возможности атаки

        [Header("Range")]
        [SerializeField] private Cooldown _rangeColldown; //перезарядка
        [SerializeField] private SpawnComponent _rangeAttack;//атаковать пулять

        private static readonly int Melee = Animator.StringToHash("melee"); //создать анимационные Event
        private static readonly int Range = Animator.StringToHash("range");//создать анимационные Event

        private Animator _animator;//получить аниматор


        private void Awake()
        {
            _animator = GetComponent<Animator>();//получить аниматор
        }
        private void Update()
        {
            if(_vision.isTouchingLayer)//в vision что-то есть
            {
                if(_meleeCanAttack.isTouchingLayer)//и мы можем атаковать
                {
                    if(_meleeColldown.IsRedy)//melee cooldown у нас готов
                    MeleeAttack();//атакуем в близи
                    return;//выйти что бы следующая атака не прошла
                }
                
                if(_rangeColldown.IsRedy)
                {
                    RangeAttack();//иначи в дали
                }
            }    
        }

        private void RangeAttack()
        {
            _rangeColldown.Reset();
            _animator.SetTrigger(Range);//получить триггер range
        }

        private void MeleeAttack()
        {
            _meleeColldown.Reset();//reset cooldown
            _animator.SetTrigger(Melee);//получить триггер melee
        }

        private void OnMeleeAttack() //вызывать из анимационных event 
        {
            _meleeAttack.Check(); //чекать урон
        }

        private void OnRangeAttack()//вызывать из анимационных event
        {
            _rangeAttack.Spawn(); //чекать project tile
        }
    }
}