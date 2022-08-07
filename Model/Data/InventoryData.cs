using PixelCrew.Model.Definitions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Model.Data
{
    [Serializable]
    public class InventoryData //все изменяемые данные
    {
        [SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>(); //лист данных

        public delegate /*описание*/void OnInventoryChanged(String id, int value);//описание функции

        public OnInventoryChanged OnChanged; //тип описаной функции 
        //в HeroOne _session.Data.Inventory.OnChanged += OnInvetoryChanged; -> автоматически внесет данные сюда

        public void Add(string id,int value)
        {
            if (value <= 0) return;

            var itemDef = DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return; //если не существует в strucr такого объекта то выходим

            var isFull = _inventory.Count >= DefsFacade.I.Player.InventorySize;//
            if (itemDef.IsStackable)//если стакается
            {

                var item = GetItem(id);
                if (item == null)
                {
                    if (isFull) return;//если полный инвентарь на тот или иной предмет то выходим
                    item = new InventoryItemData(id);
                    _inventory.Add(item);

                }

                item.Value += value; //добавляем количество если даже уже есть он в инвентаре

            }
            else//если нет то доабвить 1
            {
                var itemLast = DefsFacade.I.Player.InventorySize - _inventory.Count;// сколько осталось
                value = Mathf.Min(itemLast, value);
                for(int i=0;i<value;i++)
                {
                    var item = new InventoryItemData(id)
                    { Value =1 };
                    _inventory.Add(item);
                }
            }
           
            OnChanged?.Invoke(id,Count(id));
        }

        public void Remove(string id, int value)
        {
            var itemDef = DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return; //если не существует в strucr такого объекта то выходим

            if (itemDef.IsStackable)
            {
                var item = GetItem(id);//получаем предмет
                if (item == null) return;

                item.Value -= value;

                if (item.Value <= 0)
                    _inventory.Remove(item);
            }
            else //если не стакаются то удалить сразу все из инвентори
            {
                for(int i =0;i<value;i++)
                {
                    var item = GetItem(id);//получаем предмет
                    if (item == null) return;

                    _inventory.Remove(item);
                }
            }


            

            OnChanged?.Invoke(id, Count(id));
        }
        private InventoryItemData GetItem(string id)//возвращать есть ли такой инвентарь у нас уже
        {
            foreach(var itemData in _inventory)
            {
                if (itemData.Id == id)
                    return itemData;
            }
            return null;
        }

        internal int Count(string id)
        {
            var count = 0;
            foreach(var item in _inventory)
            {
                if (item.Id == id)
                    count += item.Value;
            }
            return count;
        }
    }

    [Serializable]
    public class InventoryItemData
    {
        [InventoryId] public string Id;
        public int Value;

        public InventoryItemData(string id)//конструктор для передачи данных
        {
            Id = id;
        }
    }
}