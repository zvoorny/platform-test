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
    public class HeroOne : Creature //��������� �� ������
    {
        /*����� CheckCircleOverrLap ���������*/
        //private Collider2D[] _interactionResult = new Collider2D[1];//��� ����, ����� ��������������� � �����
        //[SerializeField] private LayerMask _interactionLayer; // ��������� ����
        [SerializeField] private CheckCircleOverLap _interactionCheck;

       
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private float _slamDawnVelocity; // ��� �������� ����������� - ����������
      
        [SerializeField] private Vector3 _groundCheckPositionDelta;
       
        [SerializeField] private float _interactionRadius; //��� �������������� � �����
        [SerializeField] private float _damageVelocity; //���� � ������

        [SerializeField] private Cooldown _throwCooldown; //cooldown �� ������� ���� Cooldown.cs
        [SerializeField] private RuntimeAnimatorController _armed; // �������� � �������

        

        [SerializeField] private RuntimeAnimatorController _disarmed; // �������� ��� ������

        [Header("Super Throw")]
        [SerializeField] private Cooldown _superThrowCooldown;
        [SerializeField] private int _superThrowParticles; //������� ����� ������
        [SerializeField] private float _superThrowDelay; //����� ������ ������� ������� ���

        private static readonly int ThrowKey = Animator.StringToHash("throw");
        private static readonly int IsOnWall = Animator.StringToHash("is-on-wall");//�������� ���������� � ���������

        private bool _allowDoubleJump;
        private bool _superThrow;

        private float _defaultGravityScale; //��� �����
        private bool _isOnWall; //��� �����
        [SerializeField] private ColliderCheck _wallCheck; //��� �����

        private int SwordCount => _session.Data.Inventory.Count("Sword"); //��������� ���� � ���������



        private GameSession _session; //���������� ������

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp.Value = currentHealth;//������� ������ � �����
        }

        private void Start() 
        {
            _session = FindObjectOfType<GameSession>(); //������� � ������ ������ ��� ������
          
            var health = GetComponent<HealthComponent>(); //����� �������� ��� ������ �������������
            _session.Data.Inventory.OnChanged += OnInvetoryChanged; //������������� �� ������� - ���������� ������ �� ���� ������
            _session.Data.Inventory.OnChanged += AnotherHandler; //�������� ��� ��������� � ���������

            health.SetHealth(_session.Data.Hp.Value); //�������� ���������� ����� � ����� � HealthComponent

            UpdateHeroWeapom(); //����� ������ � �������� ������������� �����
        }

        private void OnDestroy()
        {
            _session.Data.Inventory.OnChanged -= OnInvetoryChanged; //������������ ��� �� ��� ��������� ��������� ��� ���� ��
        }
        private void AnotherHandler(string id, int value)//�������� ��� ��������� � ���������
        {
            Debug.Log($"Inventory changed: {id}: {value}");
        }

        private void OnInvetoryChanged(string id, int value)//��������� ��������� - ������ ����
        {
            if (id == "Sword")
                UpdateHeroWeapom();
        }

        protected override void Awake() //��������������� ������ � �������� ������ Creater
        {
            base.Awake();

            _defaultGravityScale = Rigidbody.gravityScale; //�����
        }

        protected override void Update() //��������������� 
        {
            base.Update();

            var moveToSameDirection = Direction.x * transform.lossyScale.x > 0; //� ����� ������� ����� - true ��� false
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
            Animator.SetBool(IsOnWall,_isOnWall);//����������� ��� ��� �������� ���������� �� ������
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
                Sounds.Play("Jump");//���� Jump
                _particles.Spawn("Jump");
                _allowDoubleJump = false;
                return _jumpSpeed;
            }

            return base.CalculateJumpVelocity(yVelocity);
        }


        public void AddInventory(string id,int value)//��� �� �������� ��� ��� ������, � �� ������ ������ � �.�.
        {
            _session.Data.Inventory.Add(id, value);
        }

        /* public void AddCoins(int coins)//��� � ������ AddInventory
         {
             //_coins += coins;
             _session.Data.Coins += coins;
             //Debug.Log($"{coins} coins added. Total coins: {_coins}");
             Debug.Log($"{coins} coins added. Total coins: {_session.Data.Coins}");
         }
        */
        public override void TakeDamage() //��������� �����
        {
            base.TakeDamage();
            //var numCoins = _session.Data.Inventory.Count("Coin");
        }

        /*����� CheckCircleOverLap*/
        /*internal void Interact()
        {
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, _interactionRadius, _interactionResult, _interactionLayer); //�������� ��������������� ������� - ���������� ���-�� �����������

            for (int i = 0; i < size; i++) //������� ����������� ��� �������
            {
               var interactable= _interactionResult[i].GetComponent<Components.InteractComponent>(); //��������� InteractComponent.cs � ����� namespace ������ ����
                if(interactable != null)
                { interactable.Interact(); } //���� �� null, �� ��������� � InteractComponent.cs � ������� Interact()
            }
        }*/

        public void Interact()
        {
            _interactionCheck.Check();
        }

        private void OnCollisionEnter2D(Collision2D other) //����� �� ��������������� � ������� ���������
        {
            if(other.gameObject.IsInLayer(_groundLayer))//��������������� �� ����� �������� //Utils.GameObjectExtensions
            {
                var contact = other.contacts[0];

                 if(contact.relativeVelocity.y >= _slamDawnVelocity) //relativeVelocity - �������� �������������� 2� collider
                {
                    _particles.Spawn("SlamDown");
                }

                if (contact.relativeVelocity.y >= _damageVelocity) //relativeVelocity - �������� �������������� 2� collider
                {
                    GetComponent<HealthComponent>().ModifyHealth(-1); //���� � ������
                }
            }
        }

        
        public override void Attack() //������� ��������� ��������
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
            
            if (SwordCount>0) //�������� ��������� � ������
            {
                Animator.runtimeAnimatorController = _armed; // ������������ ���������
            }
            else
            {
                Animator.runtimeAnimatorController = _disarmed;
            }
        }

        public void OnDoTrow()//��� �� �� ����� ������� ��������� - � �������� �������
        {
            if(_superThrow)
            {
                var numThrows = Mathf.Min(_superThrowParticles, SwordCount - 1/*������ ��������� ���� ���*/);
                StartCoroutine(DoSuperThrow(numThrows));
            }
            else//������� ������
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

        private void ThrowAndRemoveFromInventory()//�������� �������� ����
        {
            Sounds.Play("Range");//���� �����
            _particles.Spawn("Throw");
            _session.Data.Inventory.Remove("Sword", 1);//������� 1 ����� �� ���������
        }

       /* public void Throw()//������� �� shift
        {
            if (_throwCooldown.IsRedy && SwordCount>1)//���� cooldown ������ �� ����� ������
            {
                Animator.SetTrigger(ThrowKey);
                _throwCooldown.Reset(); // ������ reset �� cooldowan
            }
        }*/

        internal void StartThrowing()
        {
            _superThrowCooldown.Reset();
        }


        internal void PerformThownimg()
        {
            if (!_throwCooldown.IsRedy || SwordCount <= 1)//���� �� ������ � ������
                return;

            if (_superThrowCooldown.IsRedy) _superThrow = true;//����������� �����

            Animator.SetTrigger(ThrowKey);
            _throwCooldown.Reset(); // ������ reset �� cooldowan

        }
        

    }
}
