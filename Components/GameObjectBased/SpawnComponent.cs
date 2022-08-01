using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components.GameObjectBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;
        //[SerializeField] private bool _invertXScale;

        [ContextMenu("Spawn")] //��� �����
        public void Spawn()
        {
            /*var modifier = _invertXScale ? -1 : 1;*/
            var instance = Instantiate(_prefab,_target.position,Quaternion.identity); //������������� ��������� ���� / GameObject, �������, � �������
            instance.transform.localScale/*� ��������� �������*/ = _target.lossyScale;/*� ������������� ������ �����*/
            instance.SetActive(true);
        }
    }
}
