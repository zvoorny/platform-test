using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumCoint : MonoBehaviour
{
    [SerializeField] private GameObject _objectToDestroy;
    int _sum = 0;
    public void SumCoints()
    {
        _sum = _sum + 1;
        Debug.Log(message: _sum);
    }
}
