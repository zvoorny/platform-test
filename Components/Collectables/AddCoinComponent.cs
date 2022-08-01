using PixelCrew.Creatures.Hero;
using UnityEngine;

namespace PixelCrew.Components.Collectables
{
    public class AddCoinComponent : MonoBehaviour
    {
        [SerializeField] private int _numCoins;

        private HeroOne _hero;

        private void Start()
        {
            _hero = FindObjectOfType<HeroOne>();
        }

        public void Add()
        {
           // _hero.AddCoins(_numCoins);
        }
    }
}
