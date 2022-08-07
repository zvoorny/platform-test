using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components.Health
{
    public class ModifyHelthComponent : MonoBehaviour
    {
        [SerializeField] private int _hpDelta; //����� ������� ����� ��������



        public void ApplyDamage(GameObject target)//��������� �����. �������� �������  EnterColisionComponent -> _action?.Invoke(other.gameObject);//� ��� �������
        {
            var healthComponent = target.GetComponent<HealthComponent>();//�������� �������� ����������
            if (healthComponent!= null)
            {
                healthComponent?.ModifyHealth(_hpDelta); //�������� ������ 
            }
        }
    }
}
