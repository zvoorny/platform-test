using System.Collections;
using UnityEngine;

namespace PixelCrew.Components.Interactions
{
    public class DoInteractionComponent : MonoBehaviour
    {
        public void DoInteraction(GameObject go)
        {
            var interactable = go.GetComponent<InteractComponent>();
            if(interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}