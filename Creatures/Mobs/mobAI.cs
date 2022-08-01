using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.GameObjectBased;
using PixelCrew.Creatures.Mobs.Patrolling;
using System.Collections;
using UnityEngine;


namespace PixelCrew.Creatures.Mobs
{
    public class mobAI : MonoBehaviour //AI mobs
    {
        [SerializeField] private ColliderCheck _vision; //проверка доступности
        [SerializeField] private ColliderCheck _canAttack; //проверка возможности атаки

        [SerializeField] private float _alarmDelay=0.5f;//время ожидания после агра
        [SerializeField] private float _attackCooldown = 1f;//время перезарядки атаки
        [SerializeField] private float _missHeroCooldown = 0.5f;//время перезарядки если пропал

        private IEnumerator _current; //переменная хранить в себе запущенные Корутины
        private GameObject _target; //цель куда идти

        private static readonly int isDieKey = Animator.StringToHash("is-dead");

        private SpawnListComponent _particles; //для анимации
        private Creature _creature; //получаем направление героя
        private Animator _animator;
        private bool isDead = false;
        private Patrol _patrol; //патрулирование

        private void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();//инициализация партиклов = анимации
            _creature = GetComponent<Creature>(); //инициализируем
            _animator = GetComponent<Animator>(); //инициализация анимации
            _patrol = GetComponent<Patrol>(); //берем хлас родителя (абстрактный) но берем дочерний(наследумый) класс
        }

        private void Start()
        {
            /*StartState(Patrolling());*///т.к. сделали ксласс на патрулирование
            StartState(_patrol.DoPatrol());
        }

        public void OnHeroOnVision(GameObject go) //проверка в радиусе Героя //в Vision добавить Action -> Enter Trigger Component
        {
            if (isDead) return; //если Герой Мертвый не агриться

            _target = go;

            StartState(AgroToHero());//сначала агриться а потом идти
        }

        private IEnumerator AgroToHero()
        {
            LookAtHero();
            _particles.Spawn("Exclamation");//анимация агр моба на героя
            yield return new WaitForSeconds(_alarmDelay); //подождали

            StartState(GoToHero());//теперь идем к Герою
        }

        private void LookAtHero() //поворот акулы если заметила героя
        {
            var direction = GetDirectionToTarget();
            _creature.UpdateSpriteDirections(direction);
        }

        private IEnumerator GoToHero()
        {
             while(_vision.isTouchingLayer) //идти пока в точке vision (пересекается радиус)
            {
                if (_canAttack.isTouchingLayer) //если можем атаковать - атакуем
                {
                    StartState(Attack());
                }
                else //иначи идем к Герою
                {
                    SetDirectionToTarget(); //метод куда идти могу - к Герою
                }
                yield return null; //пропустить 1 кадр
            }
            _creature.SetDirection(Vector2.zero);//остановиться как потерял героя
            _particles.Spawn("Miss");
            yield return new WaitForSeconds(_missHeroCooldown);
            StartState(_patrol.DoPatrol());
        }

        private IEnumerator Attack()
        {
            while(_canAttack.isTouchingLayer) // проверка можем атаковать или нет
            {
                _creature.Attack();
                yield return new WaitForSeconds(_attackCooldown); //ждем время после атаки
            }

            StartState(GoToHero()); //если не можем атаковать больше то бежим к герою
        }

        private void SetDirectionToTarget()
        {
            var direction = GetDirectionToTarget(); //текущая точка таргета - нашу позицию / вектор направления
            direction.y = 0; //движемся по горизонтали
            _creature.SetDirection(direction); //нормализированный - единичный вектор не было перепадов на скорость
            
        }

        private Vector2 GetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position; //текущая точка таргета - нашу позицию / вектор направления
            direction.y = 0; //движемся по горизонтали
            return direction.normalized;
        }

        /*private IEnumerator Patrolling() //Корутина отвечает за протрулирование
        {
            //новый класс Patrol;
            yield return null;
        }*/

        private void StartState(IEnumerator coroutine) //моб мог делать одну вещб за раз
        {
            _creature.SetDirection(Vector2.zero);//останавливать движение перед каждой смены Корутины //иначи после смерти движется
            if(_current!= null) //если текущая корутина не пустая то останавливаем
            {
                StopCoroutine(_current);
            }
            _current = coroutine;
            StartCoroutine(coroutine);//передавать корутину
        }

        public void OnDie() //умирает
        {
            isDead = true;
            _animator.SetBool(isDieKey, true);
            _creature.SetDirection(Vector2.zero);//останавливать движение перед каждой смены Корутины //иначи после смерти движется

            //_particles.Spawn("Dead");
            //после смерти остановить все корутины
            if (_current != null) //если текущая корутина не пустая то останавливаем
            {
                StopCoroutine(_current);
            }

            
        }
    }
}