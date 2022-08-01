using System;
using UnityEngine;
using System.Linq;

namespace PixelCrew.Components.GameObjectBased //несколько Spawn Components которые будет вызывать по их имени
{
    public class SpawnListComponent : MonoBehaviour
    {

        [SerializeField] private SpawnData[] _spawners;//массив Spawn()

        public void SpawnAll()//что бы спавнилось не только по id
        {
            foreach (var spawnData in _spawners)
            {
                spawnData.Component.Spawn();
            }
        }

        public void Spawn(string id) //что хотим spawn
        {
            var spawner = _spawners.FirstOrDefault(element => element.Id == id); //для работы с массивом, тоже самое что и перебор массива в цикле
            spawner?.Component.Spawn();

            /*foreach(var data in _spawners)
            {
                if(data.Id ==id)
                {
                    data.Component.Spawn();
                    break;
                }
            }*/
        }

        [Serializable]
        public class SpawnData
        {
            public string Id;
            public SpawnComponent Component; 
        }
       
    }
}