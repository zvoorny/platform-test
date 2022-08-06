using System;
using System.Collections;
using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade")]//появился в  Utnity при нажатии на правую кнопку
    public class DefsFacade : ScriptableObject
    {
        [SerializeField] private InventoryItemDef _items;
        [SerializeField] private PlayerDef _player;
        public InventoryItemDef Items => _items;
        public PlayerDef Player => _player;

        private static DefsFacade _instance; //инвентарь
        public static DefsFacade I => _instance == null ? LoadDefs() : _instance; //забираем, если item == null то загрузить иначи возвращяем instance

        private static DefsFacade LoadDefs()
        {
           return _instance = Resources.Load<DefsFacade>("DefsFacade");//Resources.Load стандартный класс
        }
    }
}