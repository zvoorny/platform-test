﻿using PixelCrew.Creatures.Hero;
using UnityEngine;

namespace PixelCrew.Components.Collectables
{
    public class ArmHeroComponent : MonoBehaviour
    {

        public void ArmHero(GameObject go)
        {
            var hero = go.GetComponent<HeroOne>();
            if(hero!=null)
            {
                //hero.ArmHero();
            }
        }
        
    }
}