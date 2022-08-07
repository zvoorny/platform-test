using System;
using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName ="Defs/InventoryItems",fileName = "InventoryItems")]//по€вилс€ в  Utnity при нажатии на правую кнопку
    public class InventoryItemDef : ScriptableObject//объект создавать инстансы как assets
    {
        [SerializeField] private ItemDef[] _items;

        public ItemDef Get(string id)
        {
            foreach(var itemDef in _items)
            {
                if (itemDef.Id == id) //если находим возвращ€ем
                    return itemDef;
            }
            return default;//если struct то null вернуть мы не можем
        }

#if UNITY_EDITOR //толшько дл€ редактора публичный items
        public ItemDef[] ItemsForEditor => _items;
#endif
    }


    [Serializable]
    public struct ItemDef//не можем мен€ть так как описание
    {
        [SerializeField] private string _id;
        [SerializeField] private bool _isStackable;//что бы не стакались объекты
        public string Id => _id;//дл€ доступа из вне

        public bool IsStackable => _isStackable;
        public bool IsVoid => string.IsNullOrEmpty(_id); //если id пустое то это пустой itemDef
    }
}
