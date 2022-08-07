using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Widgets
{
    public class ProgressBarWindget : MonoBehaviour //процесс виджета
    {
        [SerializeField] private Image _bar;

        public void SetProgress(float progress)
        {
            _bar.fillAmount = progress;//двигать ползунок у Image
        }
    }
}