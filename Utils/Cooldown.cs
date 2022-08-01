using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Utils
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField] private float _value;

        private float _timesUp;
        public void Reset()
        {
            _timesUp = Time.time/*прошло время от начала запуска игры*/ + _value;
        }

        public bool IsRedy => _timesUp <= Time.time;//проверка если _timesUp <= время с начала запуска проекта
    }
}
