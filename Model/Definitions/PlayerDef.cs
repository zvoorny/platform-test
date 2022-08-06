﻿using System.Collections;
using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/PlayerDef", fileName = "PlayerDef")]
    public class PlayerDef : ScriptableObject
    {

        [SerializeField] private int _inventorySize;
        [SerializeField] private int _maxHealth;//max HP

        public int InventorySize => _inventorySize;
        public int MaxHealth => _maxHealth;
    }
}