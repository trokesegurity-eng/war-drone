using UnityEngine;
using SubDrone.Data;
using SubDrone.Sonar;

namespace SubDrone.Creatures
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class CreatureAI2D : MonoBehaviour, ISonarTarget
    {
        public string TargetKey => _targetKey;
        public Transform Transform => transform;
        public bool CanScan => true;

        private Rigidbody2D _rigidbody;
        private CreatureDefinition _definition;
        private string _targetKey;
        private Vector2 _patrolDirection;
        private float _patrolTimer;
        private Transform _drone;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void SetDefinition(CreatureDefinition definition)
        {
            _definition = definition;
            _targetKey = $"species_{definition.id}";
        }

        private void Start()
        {
            var droneObject = GameObject.FindGameObjectWithTag("Player");
            _drone = droneObject != null ? droneObject.transform : null;
            _patrolDirection = Random.insideUnitCircle.normalized;
            _patrolTimer = Random.Range(1f, 3f);
        }

        private void FixedUpdate()
        {
            if (_definition == null)
            {
                return;
            }

            var direction = _definition.archetype switch
            {
                CreatureArchetype.Patrol => Patrol(),
                CreatureArchetype.Chase => ChaseOrPatrol(),
                CreatureArchetype.Ambush => AmbushOrHold(),
                CreatureArchetype.Flee => FleeOrPatrol(),
                _ => Vector2.zero,
            };

            _rigidbody.AddForce(direction * _definition.stats.speed, ForceMode2D.Force);
            if (_rigidbody.velocity.magnitude > _definition.stats.speed)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _definition.stats.speed;
            }
        }

        private Vector2 Patrol()
        {
            _patrolTimer -= Time.fixedDeltaTime;
            if (_patrolTimer <= 0f)
            {
                _patrolDirection = Random.insideUnitCircle.normalized;
                _patrolTimer = Random.Range(1.5f, 4.5f);
            }

            return _patrolDirection;
        }

        private Vector2 ChaseOrPatrol()
        {
            if (_drone == null)
            {
                return Patrol();
            }

            var direction = (Vector2)(_drone.position - transform.position);
            if (direction.magnitude <= _definition.stats.detectionRadius && _definition.stats.aggression > 0.35f)
            {
                return direction.normalized;
            }

            return Patrol();
        }

        private Vector2 AmbushOrHold()
        {
            if (_drone == null)
            {
                return Vector2.zero;
            }

            var direction = (Vector2)(_drone.position - transform.position);
            if (direction.magnitude <= _definition.stats.detectionRadius * 0.65f && _definition.stats.aggression > 0.55f)
            {
                return direction.normalized * 1.2f;
            }

            return Vector2.zero;
        }

        private Vector2 FleeOrPatrol()
        {
            if (_drone == null)
            {
                return Patrol();
            }

            var direction = (Vector2)(_drone.position - transform.position);
            if (direction.magnitude <= _definition.stats.detectionRadius)
            {
                return (-direction).normalized;
            }

            return Patrol();
        }
    }
}
