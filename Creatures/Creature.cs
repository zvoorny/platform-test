using UnityEngine;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.GameObjectBased;
using PixelCrew.Component.Audio;

namespace PixelCrew.Creatures
{
    public class Creature : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private bool _invertScale;//куда смотреть и мобы и Герои
        [SerializeField] private float _speed;
        [SerializeField] protected float _jumpSpeed;
        [SerializeField] private float _damagejumpSpeed; //при уроне подпрыгивал
        [SerializeField] private int _damage; //урон

        [Header("Checkers")]
        [SerializeField] protected LayerMask _groundLayer;
        [SerializeField] private ColliderCheck _groundCheck;
        [SerializeField] private CheckCircleOverLap _attackRange; //для аттаки в файле проверки радиуса
        [SerializeField] protected SpawnListComponent _particles; // список spawn() - анимация бега, прыжка и т.д.

        protected Vector2 Direction;
        protected Rigidbody2D Rigidbody;
        protected Animator Animator;
        protected PlaySoundsComponent Sounds;//класс для вызыва звука
        protected bool IsGrounded;
        private bool _isJumping;

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground"); // вывели что бы меньше ело памяти. Static посчитает только один раз.
        private static readonly int IsRunningKey = Animator.StringToHash("verical-velocity");
        private static readonly int IsVerticalVelocityKey = Animator.StringToHash("is-running");
        private static readonly int AttackKey = Animator.StringToHash("attack");
        private static readonly int Hit = Animator.StringToHash("hit");

        protected virtual void Awake() //переопределение методов 
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            Sounds = GetComponent<PlaySoundsComponent>();//класс для вызыва звука
        }

        public void SetDirection(Vector2 direction) //движение
        {
            Direction = direction;
        }

        protected virtual void Update() //переопределение
        {
            IsGrounded = _groundCheck.isTouchingLayer;
        }

        private void FixedUpdate()
        {
            var xVelocity = Direction.x * _speed;
            var yVelocity = CalculateYVelocity();

            Rigidbody.velocity = new Vector2(xVelocity, yVelocity);



            Animator.SetBool(IsGroundKey, IsGrounded);
            Animator.SetFloat(IsRunningKey, Rigidbody.velocity.y);
            Animator.SetBool(IsVerticalVelocityKey, Direction.x != 0);

            UpdateSpriteDirections(Direction);
        }

        protected virtual float CalculateYVelocity()
        {
            var yVelocity = Rigidbody.velocity.y;
            var isJumpPressing = Direction.y > 0;

            if (IsGrounded)
            {
                _isJumping = false;//после получения что бы не прыгал высоко
            }
            if (isJumpPressing)
            {
              
                _isJumping = true;//после получения что бы не прыгал высоко

                var isFalling = Rigidbody.velocity.y <= 0.01f;
                if (!isFalling) return yVelocity;

                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;

            }
            else if (Rigidbody.velocity.y > 0 && _isJumping)// && _isJumping после получения что бы не прыгал высоко
            {
                yVelocity *= 0.5f;
                //_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, yVelocity);
            }

            return yVelocity;

        }

        protected virtual float CalculateJumpVelocity(float yVelocity)
        {

            if (IsGrounded)
            {
                Sounds.Play("Jump");//звук Jump
                yVelocity += _jumpSpeed;
                _particles.Spawn("Jump");//из массивуа партикл передаем
            }

            return yVelocity;
        }

        public void UpdateSpriteDirections(Vector2 direction)
        {
            var multiplier = _invertScale ? -1 : 1; //для корректности направления персонажа - Моб или герой
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(multiplier, 1, 1);
            }
            else if (direction.x < 0)
            {

                transform.localScale = new Vector3(-1* multiplier, 1, 1);
            }
        }

        public virtual void TakeDamage() //получение урона
        {
            _isJumping = false; //после получения что бы не прыгал высоко
            Animator.SetTrigger(Hit);   //анимация урона
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _damagejumpSpeed);
        }


        public virtual void Attack() //функция получения анимации
        {
            Animator.SetTrigger(AttackKey);//получение анимации тригера атаки
            Sounds.Play("Melee");//звук атаки

        }

        public void OnDoAttack() //функция дамаги
        {
            _particles.Spawn("Attack");
            _attackRange.Check();
            
        }

    }


}