using System;
using UnityEngine;
using Unity.Netcode;

namespace Mosquito.Character
{
    public enum State
    {
        None,
        Idle, // 기본 상태(순찰)
        Attack, // 공격
        Suprise,
        // 경계 + 탐색
        Boundary,  // 탐색
        // 쫓다??
        // 벽 공격
    }
    
    public class CharacterController : NetworkBehaviour
    {
        
        public bool aiOn
        {
            get => _aiOn;
            set
            {
                if (_aiOn == value)
                    return;

                _aiOn = value;
                _agent.enabled = value;

                if (value)
                    _agent.speed = moveGain;
            }
        }

        public virtual float horizontal
        {
            get => _horizontal.Value;
            set => _horizontal.Value = value;
        }
        public virtual float vertical
        {
            get => _vertical.Value;
            set => _vertical.Value = value;
        }
        public virtual float moveGain
        {
            get => _moveGain.Value;
            set
            {
                _moveGain.Value = value;

                if (_aiOn)
                    _agent.speed = value;
            }
        }

        public virtual Vector3 velocity 
        { 
            get => _velocity;
            set => _velocity = value;
        }

        public int hp
        {
            get => _hp.Value;
            set
            {
                _hp.Value = value;
            }
        }

        public int hpMax => _hpMax.Value;

        private NetworkVariable<int> _hp = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        private NetworkVariable<int> _hpMax = new NetworkVariable<int>(100);
        public event Action<int> onHpChanged;

        public State state
        {
            get
            {
                return (State)_animator.GetInteger(AnimationStrings.State);
            }
            set
            {
                _animator.SetInteger(AnimationStrings.State, (int)value);
            }
        }
        private bool _aiOn;
        private NetworkVariable<float> _horizontal = new NetworkVariable<float>(0.0f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        private NetworkVariable<float> _vertical = new NetworkVariable<float>(0.0f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        private NetworkVariable<float> _moveGain = new NetworkVariable<float>(0.0f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        private Vector3 _velocity;
        private Vector3 _accel;
        [SerializeField] private float _slopeAngle = 45.0f;
        [SerializeField] private float _step = 0.2f;
        [SerializeField] private LayerMask _groundMask;
        private Animator _animator;
        private Rigidbody _rigidbody;
        private UnityEngine.AI.NavMeshAgent _agent;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsOwner)
            {
                _hp.Value = hpMax;
            }
            _hp.OnValueChanged += (prev, current) => onHpChanged?.Invoke(current);
        }

        // F = m a  (힘 = 질량 x 가속도)
        public void AddForce(Vector3 force, ForceMode forceMode)
        {
            switch (forceMode)
            {
                case ForceMode.Force:
                    _accel += force / _rigidbody.mass;
                    break;
                case ForceMode.Acceleration:
                    _accel += force;
                    break;
                case ForceMode.Impulse:
                    _velocity += force / _rigidbody.mass;
                    break;
                case ForceMode.VelocityChange:
                    _velocity += force;
                    break;
                default:
                    break;
            }
        }
        

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
            _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            //Skill[] skills = _animator.GetBehaviours<Skill>();
            // for (int i = 0; i < skills.Length; i++)
            // {
            //     skills[i].Init(this);
            // }
        }

        protected virtual void Update()
        {
            //Debug.Log("CharaterController");
            //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        private void FixedUpdate()
        {
            if (IsOwner)
                Move();
            
            
        }

        public bool IsGrounded()
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, 0.15f, _groundMask);
            return cols.Length > 0;
        }

        private void Move()
        {
            if (_aiOn)
            {

            }
            else
            {
                ManualMove();
            }
        }


        private void ManualMove()
        {
            RaycastHit hit;
            UnityEngine.AI.NavMeshHit navMeshHit;
            Vector3 expected = transform.position
                               + Quaternion.LookRotation(transform.forward) * _velocity * Time.fixedDeltaTime;

            Debug.DrawRay(transform.position,
                          expected - transform.position, Color.yellow, 0.1f);
            if (Physics.Raycast(transform.position,
                                (expected - transform.position).normalized,
                                out hit,
                                Vector3.Distance(transform.position, expected),
                                _groundMask))
            {
                if (UnityEngine.AI.NavMesh.SamplePosition(hit.point,
                                           out navMeshHit,
                                           1.0f,
                                           UnityEngine.AI.NavMesh.AllAreas))
                {
                    transform.position = navMeshHit.position;
                }
            }
            else
            {
                transform.position = expected;
            }


            if (IsGrounded())
            {
                _accel.y = .0f;
                _velocity.y = .0f;
                expected = transform.position
                               + Quaternion.LookRotation(transform.forward) * _velocity * Time.fixedDeltaTime;

                float distance = Vector3.Distance(expected, transform.position);
                float slopeHeight = Mathf.Abs(distance * Mathf.Tan(Mathf.Rad2Deg * _slopeAngle));

                Debug.DrawRay(expected + Vector3.up * slopeHeight,
                              Vector3.down * 2 * slopeHeight,
                              Color.red,
                              1.0f);

                Debug.Log($"{velocity}, {slopeHeight}");

                // slope
                if (Physics.Raycast(expected + Vector3.up * slopeHeight,
                                    Vector3.down,
                                    out hit,
                                    2 * slopeHeight,
                                    _groundMask))
                {
                    if (UnityEngine.AI.NavMesh.SamplePosition(hit.point,
                                           out navMeshHit,
                                           1.0f,
                                           UnityEngine.AI.NavMesh.AllAreas))
                    {
                        transform.position = navMeshHit.position;
                    }
                }
                // step
                else if (Physics.Raycast(expected + Vector3.up * _step,
                                         Vector3.down,
                                         out hit,
                                         _step * 2,
                                         _groundMask))
                {
                    if (UnityEngine.AI.NavMesh.SamplePosition(hit.point,
                                           out navMeshHit,
                                           1.0f,
                                           UnityEngine.AI.NavMesh.AllAreas))
                    {
                        transform.position = navMeshHit.position;
                    }
                }
            }
            else
            {
                _velocity += _accel * Time.fixedDeltaTime;
                _accel += Physics.gravity * Time.fixedDeltaTime;
            }
        }
    }
}