using PixelCrew.Model;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace PixelCrew.Components.LevelManagement
{
    public class ReloadLevelComponent : MonoBehaviour
    {
      public void Reload()
        {
            var session = FindObjectOfType<GameSession>();//получить данные о сесии
            //Destroy(session.gameObject);//удаляем GameObject
            session.LoadLastSave();//загрузить последний метод

            var scene = SceneManager.GetActiveScene();//получить активную сцену
            SceneManager.LoadScene(scene.name); // загрузить
        }
    }
}
