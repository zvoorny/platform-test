using UnityEngine.SceneManagement;
using UnityEngine;
using PixelCrew.Model;

namespace PixelCrew.Components.LevelManagement
{
    public class ExitLevelComponent : MonoBehaviour //выход из уровня двери
    {
        [SerializeField] private string _sceneName;
        public void Exit()
        {
            var session = FindObjectOfType<GameSession>();
            session.Save();
            SceneManager.LoadScene(_sceneName);//как Reload Level только переход по имени сцены
        }

    }
}