using System.Collections;
using UnityEngine;

namespace PixelCrew.Model.Data.Properies
{
    public abstract class PrefsPersistentProperty<TPropertyType> : PersistentPropety<TPropertyType>
    {
        protected string Key;
        protected PrefsPersistentProperty(TPropertyType defaultValue, string key) : base(defaultValue)
        {
            Key = key;
        }
    }
}