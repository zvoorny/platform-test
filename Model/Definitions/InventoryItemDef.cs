using System;
using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName ="Defs/InventoryItems",fileName = "InventoryItems")]//появился в  Utnity при нажатии на правую кнопку
    public class InventoryItemDef : ScriptableObject//объект создавать инстансы как assets
    {
        [SerializeField] private ItemDef[] _items;

        public ItemDef Get(string id)
        {
            foreach(var itemDef in _items)
            {
                if (itemDef.Id == id) //если находим возвращяем
                    return itemDef;
            }
            return default;//если struct то null вернуть мы не можем
        }

#if UNITY_EDITOR //толшько для редактора публичный items
        public ItemDef[] ItemsForEditor => _items;
#endif
    }


    [Serializable]
    public struct ItemDef//не можем менять так как описание
    {
        [SerializeField] private string _id;
        public string Id => _id;//для доступа из вне
        public bool IsVoid => string.IsNullOrEmpty(_id); //если id пустое то это пустой itemDef
    }
}
