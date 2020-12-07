using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using RPG.Control;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour,  IRaycastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
             if (!callingController.GetComponent<Fighter>().CanAttack(gameObject))
            {
                return false;
            }

            if (Input.GetMouseButton(0))
            {
                callingController.GetComponent<Fighter>().Attack(gameObject);
            }
            return true;
        }

        public readonly static HashSet<CombatTarget> Pool = new HashSet<CombatTarget>();

        private void OnEnable()
        {
            if(this.CompareTag("Enemy"))
            {
            CombatTarget.Pool.Add(this);
            }
        }

        public void RemoveFromCombatTargetPool()
        {
            if (this.CompareTag("Enemy"))
            {
            CombatTarget.Pool.Remove(this);
            }
        }


        public static CombatTarget IdentifyClosestCombatTarget(Vector3 pos)
        {
            CombatTarget result = null;
            float dist = float.PositiveInfinity;
            var e = CombatTarget.Pool.GetEnumerator();
            while (e.MoveNext())
            {
                float d = (e.Current.transform.position - pos).sqrMagnitude;
                if (d < dist)
                {
                    result = e.Current;
                    dist = d;
                }
            }
            Debug.Log("targeting " + result.name);
            return result;
        }
    
  }

}