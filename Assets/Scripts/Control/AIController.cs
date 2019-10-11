using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using RPG.Attributes;
using RPG.Utils;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [Range (0,1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] float agroTime = 5f;
        [SerializeField] float shoutDistance = 5;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointWaitTime = 3f;
        [SerializeField] float waypointTolerance = 1f;
   

        Fighter fighter;
        GameObject player;
        Health health;
        Mover mover;

        LazyValue<Vector3> guardPosition;
        int currentWaypointIndex = 0;

        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        float timeSinceAggrevated = Mathf.Infinity;

        private void Awake()
        {
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");

            guardPosition = new LazyValue<Vector3>(GetGuardPosition);
        }

        private void Start()
        {
            guardPosition.ForceInit();
        }

        private void Update()
        {
            if (health.IsDead()) { return; }

            if (IsAggrevated() && fighter.CanAttack(player))
            {
                // Attack State
                AttackBehavior();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                // Suspicion State
                SuspicionBehavior();

            }
            else
            {
                // Patrol State
                PatrolBehavior();
            }

            UpdateTimers();
        }

        public void Aggrevate()
        {
            timeSinceAggrevated = 0;
        }

        private void UpdateTimers()
        {
            // Update suspicion timer and waypoint timer
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
            timeSinceAggrevated += Time.deltaTime;
        }

        private void PatrolBehavior()
        {
            //Debug.Log(this + "Entering Patrol State");
            Vector3 nextPosition = guardPosition.value;
            if(patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            if(timeSinceArrivedAtWaypoint > waypointWaitTime)
            mover.StartMoveAction(nextPosition, patrolSpeedFraction);
        }


        private Vector3 GetGuardPosition()
        {
            return transform.position;
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWayPoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
           currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void SuspicionBehavior()
        {
            //Debug.Log(this + "Entering Suspicion State");
            //GetComponent<Animator>().SetTrigger("isSuspicious");
            GetComponent<ActionScheduler>().CancelAction();

        }

        private void AttackBehavior()
        {
           // Debug.Log(this + "Entering Attack State");
            //print(this.name + " is chasing the player.");
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);

            AggrevateNearbyEnemies();
        }

        private void AggrevateNearbyEnemies()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, shoutDistance, Vector3.up, 0);
            foreach (RaycastHit hit in hits) 
            {
                AIController ai = hit.collider.GetComponent<AIController>();
                if(ai != null)
                {
                   ai.Aggrevate();
                }
            }
        }

        private bool IsAggrevated()
        {
            return (timeSinceAggrevated < agroTime) || InAttackRange();
        }

        private bool InAttackRange()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}