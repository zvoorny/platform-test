using System;
using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName ="Defs/InventoryItems",fileName = "InventoryItems")]//�������� �  Utnity ��� ������� �� ������ ������
    public class InventoryItemDef : ScriptableObject//������ ��������� �������� ��� assets
    {
        [SerializeField] private ItemDef[] _items;

        public ItemDef Get(string id)
        {
            foreach(var itemDef in _items)
            {
                if (itemDef.Id == id) //���� ������� ����������
                    return itemDef;
            }
            return default;//���� struct �� null ������� �� �� �����
        }

#if UNITY_EDITOR //������� ��� ��������� ��������� items
        public ItemDef[] ItemsForEditor => _items;
#endif
    }


    [Serializable]
    public struct ItemDef//�� ����� ������ ��� ��� ��������
    {
        [SerializeField] private string _id;
        [SerializeField] private bool _isStackable;//��� �� �� ��������� �������
        public string Id => _id;//��� ������� �� ���

        public bool IsStackable => _isStackable;
        public bool IsVoid => string.IsNullOrEmpty(_id); //���� id ������ �� ��� ������ itemDef
    }
}
