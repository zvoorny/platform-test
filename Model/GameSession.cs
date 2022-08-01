using System.Collections;
using UnityEngine;

namespace PixelCrew.Model
{
    public class GameSession : MonoBehaviour //Игровая Сессия для сохранения параметров
    {

        [SerializeField] private PlayerData _data; //данные у героя

        public PlayerData Data => _data; // доступ извне 
        private PlayerData _save;

        /*что бы сохранять и чистить данные между lvl*/
        public void Awake()//запуск при запуске объкта
        {
            int numGameSessions = FindObjectsOfType<GameSession>().Length;
            if (numGameSessions > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                Save();//что бы данные остались так же с первой сценной
                DontDestroyOnLoad(this); //для загрузки первой сцены //создает хранилище внутри сцен которые не будет уничтожаться между сценами
            }

        }

        public void Save()
        {
            _save = _data.Clone();//получаем копию с текущих параметров героя (PlayerData.cs)
        }

        public void LoadLastSave()
        {
            _data = _save.Clone();
        }
    }
}