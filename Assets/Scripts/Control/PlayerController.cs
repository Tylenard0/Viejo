using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Attributes;
using System;
using UnityEngine.EventSystems;
using UnityEngine.AI;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;
 
        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float maxNavMeshProjectionDistance = 1f;
        [SerializeField] float selectCursorRaycastRadius = 1f;
        CombatTarget nearestEnemy;


        private void Awake()
        {
            health = GetComponent<Health>();
        }

            private void Update()
        {

            // Auto target / attach when pressing "T"
            if (Input.GetKeyDown(KeyCode.T))
            {
                nearestEnemy = CombatTarget.IdentifyClosestCombatTarget(transform.position);
                GetComponent<Fighter>().Attack(nearestEnemy.gameObject);
            }


            if (InteractWithUI()) return;
            if (health.IsDead())
            {
                SetCursor(CursorType.None);
                return;
            }

            if (InteractWithComponent()) return;
            if (InteractWithMovement()) return;



            SetCursor(CursorType.None);
        }

        RaycastHit[] RaycastSorted()
        {
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), selectCursorRaycastRadius);
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastSorted();
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        private bool RaycastNavMesh(out Vector3 target)
        {
            target = new Vector3();

            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (!hasHit) return false;

            NavMeshHit navMeshHit;
            bool hasCastToNavMesh = NavMesh.SamplePosition(
                hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas);
            if (!hasCastToNavMesh) return false;

            target = navMeshHit.position;
             
            return true;
        }

        private bool InteractWithMovement()
        {
            {
                Vector3 target;
                bool hasHit = RaycastNavMesh(out target);
                if (hasHit)
                {
                    if(!GetComponent<Mover>().CanMoveTo(target)) return false;

                    if (Input.GetMouseButton(0))
                    {
                        GetComponent<Mover>().StartMoveAction(target, 1f);
                    }
                    SetCursor(CursorType.Movement);
                    // if can move return true, else false
                    return true;
                }
                return false;
            }
        }

        private bool InteractWithUI()
        {
            // Detect if over UI Game Object and return true / false
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if(mapping.type == type)
                {
                    return mapping;
                }             
            }
            return cursorMappings[0];
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        // private void AutoTargetEnemy()
        // {   
        //     RaycastHit[] hits = Physics.SphereCastAll(transform.position, shoutDistance, Vector3.up, 0);
        //     foreach (RaycastHit hit in hits)
        //     {}
        // }

        Transform GetClosestEnemy(Transform[] enemies)
        {
            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = transform.position;
            foreach (Transform potentialTarget in enemies)
            {
                Vector3 directionToTarget = potentialTarget.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }
            Debug.Log(this + "targeting " + bestTarget.name);
            return bestTarget;
        }

    }
}

