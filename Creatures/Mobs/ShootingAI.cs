using PixelCrew.Components;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Utils;
using System;
using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs
{
    public class ShootingAI : MonoBehaviour
    {
        [SerializeField] public ColliderCheck _vision;
        [SerializeField] private Cooldown _cooldown;
        [SerializeField] private SpriteAnumationMass _animation;


        private void Update()
        {
            if(_vision.isTouchingLayer && _cooldown.IsRedy)
            {
                Shoot();
            }
        }

        public void Shoot()
        {
            _cooldown.Reset();
            _animation.SetClips("start_attack");
        }
    }
}