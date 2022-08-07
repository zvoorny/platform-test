using System;
using System.Collections;
using UnityEngine;

namespace PixelCrew.Model.Data.Properies
{
    [Serializable]//что бы в Data->GameSettings были параметры - отображались
    public class FLoatPersisterProperty : PrefsPersistentProperty<float>
    {
        public FLoatPersisterProperty(float defaultValue, string key) : base(defaultValue, key)
        {
            Init();//что бы проинициализировать
        }

        protected override void Write(float value)
        {
            PlayerPrefs.SetFloat(Key, value);////PlayerPrefs стандартное для сохранении в рекгистр разные данные - по ключу и данные
            PlayerPrefs.Save();
        }


        protected override float Read(float defaultValue)
        {
            return PlayerPrefs.GetFloat(Key, defaultValue);
        }

        
    }
}