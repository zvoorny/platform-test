using UnityEngine;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.GameObjectBased;
using PixelCrew.Component.Audio;

namespace PixelCrew.Creatures
{
    public class Creature : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private bool _invertScale;//���� �������� � ���� � �����
        [SerializeField] private float _speed;
        [SerializeField] protected float _jumpSpeed;
        [SerializeField] private float _damagejumpSpeed; //��� ����� �����������
        [SerializeField] private int _damage; //����

        [Header("Checkers")]
        [SerializeField] protected LayerMask _groundLayer;
        [SerializeField] private ColliderCheck _groundCheck;
        [SerializeField] private CheckCircleOverLap _attackRange; //��� ������ � ����� �������� �������
        [SerializeField] protected SpawnListComponent _particles; // ������ spawn() - �������� ����, ������ � �.�.

        protected Vector2 Direction;
        protected Rigidbody2D Rigidbody;
        protected Animator Animator;
        protected PlaySoundsComponent Sounds;//����� ��� ������ �����
        protected bool IsGrounded;
        private bool _isJumping;

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground"); // ������ ��� �� ������ ��� ������. Static ��������� ������ ���� ���.
        private static readonly int IsRunningKey = Animator.StringToHash("verical-velocity");
        private static readonly int IsVerticalVelocityKey = Animator.StringToHash("is-running");
        private static readonly int AttackKey = Animator.StringToHash("attack");
        private static readonly int Hit = Animator.StringToHash("hit");

        protected virtual void Awake() //��������������� ������� 
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            Sounds = GetComponent<PlaySoundsComponent>();//����� ��� ������ �����
        }

        public void SetDirection(Vector2 direction) //��������
        {
            Direction = direction;
        }

        protected virtual void Update() //���������������
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
                _isJumping = false;//����� ��������� ��� �� �� ������ ������
            }
            if (isJumpPressing)
            {
              
                _isJumping = true;//����� ��������� ��� �� �� ������ ������

                var isFalling = Rigidbody.velocity.y <= 0.01f;
                if (!isFalling) return yVelocity;

                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;

            }
            else if (Rigidbody.velocity.y > 0 && _isJumping)// && _isJumping ����� ��������� ��� �� �� ������ ������
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
                Sounds.Play("Jump");//���� Jump
                yVelocity += _jumpSpeed;
                _particles.Spawn("Jump");//�� �������� ������� ��������
            }

            return yVelocity;
        }

        public void UpdateSpriteDirections(Vector2 direction)
        {
            var multiplier = _invertScale ? -1 : 1; //��� ������������ ����������� ��������� - ��� ��� �����
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(multiplier, 1, 1);
            }
            else if (direction.x < 0)
            {

                transform.localScale = new Vector3(-1* multiplier, 1, 1);
            }
        }

        public virtual void TakeDamage() //��������� �����
        {
            _isJumping = false; //����� ��������� ��� �� �� ������ ������
            Animator.SetTrigger(Hit);   //�������� �����
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _damagejumpSpeed);
        }


        public virtual void Attack() //������� ��������� ��������
        {
            Animator.SetTrigger(AttackKey);//��������� �������� ������� �����
            Sounds.Play("Melee");//���� �����

        }

        public void OnDoAttack() //������� ������
        {
            _particles.Spawn("Attack");
            _attackRange.Check();
            
        }

    }


}