using PixelCrew.Model;
using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.Interactions
{
    public class RequireItemComponent : MonoBehaviour
    {
        [SerializeField] private InventoryItemData[] _required;

        // [InventoryId][SerializeField] private string _id; //изменен на _required
        // [SerializeField] private int _count;//кол-во //изменен на _required
        [SerializeField] private bool _removeAfterUse;//если кол-во ок то удаляем кол-во из инвентори

        [SerializeField] private UnityEvent _onSuccess;
        [SerializeField] private UnityEvent _onFail;
        public void Check()
        {
            var session = FindObjectOfType<GameSession>();//найти сессию
            var areAllRequirementsMet = true; //все requrie сходятся
            foreach(var item in _required)//список всех в инвентори 
            {
                var numItem = session.Data.Inventory.Count(item.Id); //кол - во в инвентори
                if (numItem < item.Value)
                    areAllRequirementsMet = false; //если что-то не сходится то отключаем
            }

            //var numItems = session.Data.Inventory.Count(_id);//кол-во в инвентори

            if(areAllRequirementsMet)
            {
                if (_removeAfterUse)//удаляем
                {
                    foreach (var item in _required)//список всех в инвентори 
                    {
                        session.Data.Inventory.Remove(item.Id, item.Value);
                    }    
                    //session.Data.Inventory.Remove(_id, _count);//удоляем

                }

                _onSuccess?.Invoke();
            }
            else
            {
                _onFail?.Invoke();
            }
        }
    }
}