using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    [RequireComponent(typeof(SpriteRenderer))]
    //�������� ��� ����  
    public class SpriteAnumation : MonoBehaviour
    {
        [SerializeField] private int _frameRate;
        [SerializeField] private bool _loop;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private UnityEvent _onComplete;

        private SpriteRenderer _renderer;
        private float _secondsPerFrame;
        private int _currentSpriteIndex;//������ �������
        private float _nextFrameTime;

        private bool _isPlaying = true; //


        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();

        }

        private void OnBecameVisible()//������ ������� �������
        {
            enabled = _isPlaying;
        }

        private void OnBecameInvisible()//����� �������� � ����
        {
            enabled = false;
        }

       

        private void OnEnable()
        {
            _secondsPerFrame = 1f / _frameRate;
            _nextFrameTime = Time.time + _secondsPerFrame; //time.time ������� �����  
            _currentSpriteIndex = 0;
        }

        private void Update()
        {
            if (_nextFrameTime > Time.time) return;

            if (_currentSpriteIndex >= _sprites.Length)
            {
                if (_loop)
                {
                    _currentSpriteIndex = 0;
                }
                else
                {
                    enabled = false; // �.�. ���� ������� OnEnable
                    _onComplete?.Invoke();
                    return;
                }
            }

            _renderer.sprite = _sprites[_currentSpriteIndex];
            _nextFrameTime += _secondsPerFrame;
            _currentSpriteIndex++;

        }

        /*public void SetClips() //test
        {

            _nextFrameTime = Time.time + _secondsPerFrame;
            _isPlaying = true;
            _currentSpriteIndex = 0;
            return;
               
            //enabled = _isPlaying = false;
        }*/

    }

}