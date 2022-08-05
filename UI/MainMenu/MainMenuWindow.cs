using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.UI.MainMenu
{
    public class MainMenuWindow : AnimationWindow //при перемещения по меню основному
    {
        private Action _closeAction;
        public void OnShowSetting()
        {
            var window = Resources.Load<GameObject>("UI/SettingsWindow");//указываем путь к настройкам prefab
            var canvs = FindObjectOfType<Canvas>();//находим canvas
            Instantiate(window, canvs.transform);//инициализируем
        }

        public void OnStartGame()
        {
            _closeAction = () => { SceneManager.LoadScene("Level1"); /*при старте в 1 локацию*/ }; //какое-то замыкание - в переменную _closeAction закидываем загрузку 1 lvl
            Close();
        }

        public void OnExit() //когда вызываем Exit обработку записываем в _closeAction
        {
            _closeAction = () => {
                Application.Quit();//если не в редакторе

#if UNITY_EDITOR //если в редакторе
                UnityEditor.EditorApplication.isPlaying = false;//остановим проигрователь - какое-то замыкание - анонимная функицЯ
#endif 
            };
                Close();
            }

        public override void OnCloseAnimationComplete()
        {
            _closeAction?.Invoke(); //сработают после того как отыграет анимация
            base.OnCloseAnimationComplete(); //destroy object
        }
    }
}