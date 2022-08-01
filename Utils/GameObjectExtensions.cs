using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Utils
{
    public static class GameObjectExtensions  //метод расширения для объектов
    {
        public static bool IsInLayer(this GameObject go, LayerMask layer)//появление метода у всех GameObject
        {
            return layer == (layer | 1<<go.layer);
        }
    }
}