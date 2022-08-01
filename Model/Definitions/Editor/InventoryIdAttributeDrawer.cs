using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PixelCrew.Model.Definitions.Editor
{
    [CustomPropertyDrawer(typeof(InventoryIdAttribute))]//какой объект отрисовывать какой и название атрибута какой
    public class InventoryIdAttributeDrawer : PropertyDrawer
    {
        
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)//вместо стандартого отрисовать свой в GUI
        {
            var defs = DefsFacade.I.Items.ItemsForEditor;//список всех атрибутов
            var ids = new List<string>();
            foreach(var ItemDef in defs)
            {
                ids.Add(ItemDef.Id);//добавить в ids список элементов
            }

            var index = Mathf.Max(ids.IndexOf(property.stringValue),0);//обявление индекса? если есть индекс то получаем property.stringValue

            index=EditorGUI.Popup(position, property.displayName, index, ids.ToArray());//отрисовка
            property.stringValue = ids[index];
        }

    }
}