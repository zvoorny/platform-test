using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.Health;
using PixelCrew.Model;
using PixelCrew.Utils;
using System;
using System.Collections;
//using UnityEditor.Animations;
using UnityEngine;

namespace PixelCrew.Creatures.Hero
{
    public class HeroOne : Creature //наследуем от класса
    {
        /*через CheckCircleOverrLap переделан*/
        //private Collider2D[] _interactionResult = new Collider2D[1];//для руля, герой взаимодействует с рулем
        //[SerializeField] private LayerMask _interactionLayer; // получение слоя
        [SerializeField] private CheckCircleOverLap _interactionCheck;

       
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private float _slamDawnVelocity; // для анимации призимление - переменная
      
        [SerializeField] private Vector3 _groundCheckPositionDelta;
       
        [SerializeField] private float _interactionRadius; //для взаимодействия с рулем
        [SerializeField] private float _damageVelocity; //урон с высоты

        [SerializeField] private Cooldown _throwCooldown; //cooldown на метание меча Cooldown.cs
        [SerializeField] private RuntimeAnimatorController _armed; // анимация с оружием

        

        [SerializeField] private RuntimeAnimatorController _disarmed; // анимация без оружия

        [Header("Super Throw")]
        [SerializeField] private Cooldown _superThrowCooldown;
        [SerializeField] private int _superThrowParticles; //сколько мечей кидать
        [SerializeField] private float _superThrowDelay; //между каждым броском сколько сек

        private static readonly int ThrowKey = Animator.StringToHash("throw");
        private static readonly int IsOnWall = Animator.StringToHash("is-on-wall");//получаем переменную у аниматора

        private bool _allowDoubleJump;
        private bool _superThrow;

        private float _defaultGravityScale; //для стены
        private bool _isOnWall; //для стены
        [SerializeField] private ColliderCheck _wallCheck; //для стены

        private int SwordCount => _session.Data.Inventory.Count("Sword"); //получение меча в инвентаре



        private GameSession _session; //переменная сессии

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp.Value = currentHealth;//обновим данные в сесии
        }

        private void Start() 
        {
            _session = FindObjectOfType<GameSession>(); //находим в сессии нужный нам объект
          
            var health = GetComponent<HealthComponent>(); //взять значение при первой инициализации
            _session.Data.Inventory.OnChanged += OnInvetoryChanged; //подписываемся на функцию - складывать ссылки на наши методы
            _session.Data.Inventory.OnChanged += AnotherHandler; //выводить все изменения в инвентаре

            health.SetHealth(_session.Data.Hp.Value); //передача значениями сесии и героя в HealthComponent

            UpdateHeroWeapom(); //взять данные и обновить предстовления героя
        }

        private void OnDestroy()
        {
            _session.Data.Inventory.OnChanged -= OnInvetoryChanged; //отписываемся что бы при следующем изменении все было ок
        }
        private void AnotherHandler(string id, int value)//выводить все изменение в инвентаре
        {
            Debug.Log($"Inventory changed: {id}: {value}");
        }

        private void OnInvetoryChanged(string id, int value)//изменение инвентаря - взятие мяча
        {
            if (id == "Sword")
                UpdateHeroWeapom();
        }

        protected override void Awake() //переопределение метода с базового класса Creater
        {
            base.Awake();

            _defaultGravityScale = Rigidbody.gravityScale; //стена
        }

        protected override void Update() //переопределение 
        {
            base.Update();

            var moveToSameDirection = Direction.x * transform.lossyScale.x > 0; //в какую сторону давим - true или false
            if(_wallCheck.isTouchingLayer && moveToSameDirection)
            {
                _isOnWall = true;
                Rigidbody.gravityScale = 0;
            }
            else
            {
                _isOnWall = false;
                Rigidbody.gravityScale = _defaultGravityScale;
            }
            Animator.SetBool(IsOnWall,_isOnWall);//срабатывать или нет анимацию карабканья по стенам
        }

        

        protected override float CalculateYVelocity()
        {
            var isJumpPressing = Direction.y > 0;

            if (IsGrounded || _isOnWall)
            {
                _allowDoubleJump = true;
            }

            if (!isJumpPressing && _isOnWall)
            {
                return 0f;
            }
           

            return base.CalculateYVelocity();

        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {
            if (!IsGrounded && _allowDoubleJump && !_isOnWall)//double jump
            {
                Sounds.Play("Jump");//звук Jump
                _particles.Spawn("Jump");
                _allowDoubleJump = false;
                return _jumpSpeed;
            }

            return base.CalculateJumpVelocity(yVelocity);
        }


        public void AddInventory(string id,int value)//что бы добавить все что угодно, а не только монеты и т.д.
        {
            _session.Data.Inventory.Add(id, value);
        }

        /* public void AddCoins(int coins)//все в методе AddInventory
         {
             //_coins += coins;
             _session.Data.Coins += coins;
             //Debug.Log($"{coins} coins added. Total coins: {_coins}");
             Debug.Log($"{coins} coins added. Total coins: {_session.Data.Coins}");
         }
        */
        public override void TakeDamage() //получение урона
        {
            base.TakeDamage();
            //var numCoins = _session.Data.Inventory.Count("Coin");
        }

        /*через CheckCircleOverLap*/
        /*internal void Interact()
        {
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, _interactionRadius, _interactionResult, _interactionLayer); //получить пересекающиейся объекты - возвращяет кол-во результатов

            for (int i = 0; i < size; i++) //перебор результатов что вернуло
            {
               var interactable= _interactionResult[i].GetComponent<Components.InteractComponent>(); //получение InteractComponent.cs в одном namespace должны быть
                if(interactable != null)
                { interactable.Interact(); } //если не null, то переходим в InteractComponent.cs и функцию Interact()
            }
        }*/

        public void Interact()
        {
            _interactionCheck.Check();
        }

        private void OnCollisionEnter2D(Collision2D other) //метод за соприкосновение с другими объектами
        {
            if(other.gameObject.IsInLayer(_groundLayer))//соприкосновение со слоем проверка //Utils.GameObjectExtensions
            {
                var contact = other.contacts[0];

                 if(contact.relativeVelocity.y >= _slamDawnVelocity) //relativeVelocity - скорость взаимодействия 2х collider
                {
                    _particles.Spawn("SlamDown");
                }

                if (contact.relativeVelocity.y >= _damageVelocity) //relativeVelocity - скорость взаимодействия 2х collider
                {
                    GetComponent<HealthComponent>().ModifyHealth(-1); //урон с высоты
                }
            }
        }

        
        public override void Attack() //функция получения анимации
        {
            //if (!_isArmed) return;
            if (SwordCount <= 0) return;
            base.Attack();

        }

        /*
        public void ArmHero()
        {
            //_isArmed = true;
            _session.Data.IsArmed = true;
            UpdateHeroWeapom();
             
        }*/

        private void UpdateHeroWeapom()
        {
            
            if (SwordCount>0) //проверка параметра у сессии
            {
                Animator.runtimeAnimatorController = _armed; // переключение аниматора
            }
            else
            {
                Animator.runtimeAnimatorController = _disarmed;
            }
        }

        public void OnDoTrow()//что бы не могли спамить метаниями - в анимации функция
        {
            if(_superThrow)
            {
                var numThrows = Mathf.Min(_superThrowParticles, SwordCount - 1/*всегда оставался один меч*/);
                StartCoroutine(DoSuperThrow(numThrows));
            }
            else//обычный бросок
            {
                ThrowAndRemoveFromInventory();
            }
            _superThrow = false;
        }

        private IEnumerator DoSuperThrow(int NumThrows)
        {
            for(int i=0;i<NumThrows;i++)
            {
                ThrowAndRemoveFromInventory();
                yield return new WaitForSeconds(_superThrowDelay);
            }
        }

        private void ThrowAndRemoveFromInventory()//киданием обычного меча
        {
            Sounds.Play("Range");//звук атаки
            _particles.Spawn("Throw");
            _session.Data.Inventory.Remove("Sword", 1);//удалить 1 штуку из инвентаря
        }

       /* public void Throw()//метание на shift
        {
            if (_throwCooldown.IsRedy && SwordCount>1)//если cooldown прошел то можем кидать
            {
                Animator.SetTrigger(ThrowKey);
                _throwCooldown.Reset(); // делаем reset на cooldowan
            }
        }*/

        internal void StartThrowing()
        {
            _superThrowCooldown.Reset();
        }


        internal void PerformThownimg()
        {
            if (!_throwCooldown.IsRedy || SwordCount <= 1)//если не готовы к броску
                return;

            if (_superThrowCooldown.IsRedy) _superThrow = true;//супербросок готов

            Animator.SetTrigger(ThrowKey);
            _throwCooldown.Reset(); // делаем reset на cooldowan

        }
        

    }
}
