using System.Collections;
using UnityEngine;

namespace PixelCrew.Model.Data.Properies
{
    public class ObservableProperty<TPropertyType>
    {
        [SerializeField] private TPropertyType _value;//сохранять его

        public delegate void OnPropertyChanged(TPropertyType newValue, TPropertyType oldValue);
        public event OnPropertyChanged onChanged;

        public TPropertyType Value
        {
            get => _value;
            set 
            {
                var isSame = _value.Equals(value);
                if (isSame) return;
                var OldValue = _value;

                _value = value;
                onChanged?.Invoke(_value, OldValue);
            }

        }
    }
}