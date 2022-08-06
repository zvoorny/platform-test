using System.Collections;
using UnityEngine;

namespace PixelCrew.Components.GameObjectBased
{
    public class GOContainerComponent : MonoBehaviour //рандмно разлетаются при смерти
    {
        [SerializeField] private GameObject[] _gos;
        [SerializeField] private DropEvent _onDrop;

        [ContextMenu("Drop")]
        public void Drop()
        {
            _onDrop.Invoke(_gos);
        }
        
    }
}