using System.Collections;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace PixelCrew.UI.Widgets.Editor
{
    [CustomEditor(typeof(CustomButton), true)]//указываем для какого объекта 
    [CanEditMultipleObjects]//редактировать вместе
    public class CustomButtonEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            /*сначала находим нужные нам property связанный с CustomButton который указали выше - наш C#*/
            /*через EditorGUILayout отрисовываем*/
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_normal"));//когда запустится найдет эти проперти и обработает их
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_pressed"));
            serializedObject.ApplyModifiedProperties();//что бы сохранить изменения если переназначили

            base.OnInspectorGUI();//а потом отрисует что в базовом классе
        }
    }
}