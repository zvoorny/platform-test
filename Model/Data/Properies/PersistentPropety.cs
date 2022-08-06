using System;
using System.Collections;
using UnityEngine;

namespace PixelCrew.Model.Data.Properies
{
    [Serializable]
    public abstract class PersistentPropety<TPropertyType> 
    {
        [SerializeField] protected TPropertyType _value;//что бы отображалось в редакторе
        protected TPropertyType _stored;//то что на диске
        private TPropertyType _defaultValue;//если нет то дефолтное значение

        public delegate void OnPropertyChanged(TPropertyType newValue, TPropertyType oldValue);
        public event OnPropertyChanged onChanged;

        public PersistentPropety(TPropertyType defaultValue)
        {
            _defaultValue = defaultValue;
        }

        public TPropertyType Value
        {
            get => _stored;
            set
            {
                var isEquals = _stored.Equals(value);
                if (isEquals) return; //возвращяем если равные что бы не дергать Write функцию постоянно


                var oldValue = _stored;
                Write(value);
                _stored = _value = value;

                onChanged?.Invoke(value, oldValue);
            }
        }

        protected void Init()
        {
            _stored = _value = Read(_defaultValue);
        }
        protected abstract void Write(TPropertyType value);
        protected abstract TPropertyType Read(TPropertyType defaultValue);

        internal void Validate()//для  сохранения изменений
        {
            if(!_stored.Equals(_value))//если то что записано на диске не равно то что в Unity
            {
                Value = _value;//то перезапишем
            }    
        }

    }
}