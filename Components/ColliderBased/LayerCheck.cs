using System.Collections;
using UnityEngine;

namespace PixelCrew.Components.ColliderBased
{
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] protected LayerMask _groundLayer;
        [SerializeField] protected bool _isTouchingLayer;

        public bool isTouchingLayer => _isTouchingLayer; //могли видеть но не менять из вне

    }
}