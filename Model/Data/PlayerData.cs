using PixelCrew.Model.Data;
using System;
using System.Collections;
using UnityEngine;

namespace PixelCrew.Model
{
    [Serializable]
    public class PlayerData //класс для передачи параметров между сценами
    {

        [SerializeField] private InventoryData _inventory; //предметы

        public InventoryData Inventory => _inventory;
        //public int Coins;
        public int Hp;
        //public bool IsArmed;

        public PlayerData Clone() //для сохранения
        {
            var json = JsonUtility.ToJson(this);//сначала заганть класс
            return JsonUtility.FromJson<PlayerData>(json);//загнать класс
            /*return new PlayerData //более проще, но если много переменных то геморройнее
            {
                Coins = Coins,
                Hp=Hp,
                IsArmed=IsArmed
            };*/
        }
    }
}