using PixelCrew.Creatures.Hero;
using PixelCrew.Model.Definitions;
using System.Collections;
using UnityEngine;

namespace PixelCrew.Components.Collectables
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [InventoryId][SerializeField] private string _id;
        [SerializeField] private int _coint;

        public void Add(GameObject go)
        {
            var hero = go.GetComponent<HeroOne>();
            if (hero != null)
                hero.AddInventory(_id, _coint);
        }
    }
}