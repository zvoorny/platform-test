using PixelCrew.Model.Data.Properies;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Widgets
{
    public class AudioSettingsWidget : MonoBehaviour
    {
        [SerializeField] private Slider _slider; //слайдер
        [SerializeField] private Text _value;//и текст

        private FLoatPersisterProperty _model;

        private void Start()
        {
            _slider.onValueChanged.AddListener(OnSliderValueChanged); //обновление значение в другую сторону
        }

        private void OnSliderValueChanged(float value)
        {
            _model.Value = value;//обновим модельку
        }

        public void SetModel(FLoatPersisterProperty model)//проинициализируем
        {
            _model = model;
            model.onChanged += OnValueChanged;//сначала подпишимся
            OnValueChanged(model.Value,model.Value);//обновить что хотели
        }

        /*обновление в 1 сторону в слайдер*/
        private void OnValueChanged(float newValue, float oldValue)//обновим значения слайдера и текста
        {
            var textValue = Mathf.Round(newValue * 100);//у нас от 0 до 1, а мы отображаем от 0 до 100, поэтому умножаем на 100 и округляем что бы были целые значения
            _value.text = textValue.ToString();//обновим текст
            _slider.normalizedValue = newValue; //обновить слайдер
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);//отписываемся от всех событий
            _model.onChanged -= OnValueChanged;//отписываемся
        }

    }
}