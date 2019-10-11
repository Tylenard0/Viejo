using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] GameObject[] destroyOnhit = null;
        [SerializeField] float maxLifetime = 10f;
        [SerializeField] float lifeAfterImpact = 2f;

        [SerializeField] Health projectileTarget = null;
        [SerializeField] UnityEvent onProjectileHit
        ;

        GameObject instigator = null;
        float damage = 0;


        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }
        // Update is called once per frame
        void Update()
        {
            if (projectileTarget == null) return;

            if (isHoming && !projectileTarget.IsDead()) { transform.LookAt(GetAimLocation()); }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

        }

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this.projectileTarget = target;
            this.damage = damage;
            this.instigator = instigator;

            Destroy(gameObject, maxLifetime);
        }


        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = projectileTarget.GetComponent<CapsuleCollider>();
            if(targetCapsule == null)
            {
                return projectileTarget.transform.position;
            }
            return projectileTarget.transform.position + Vector3.up * targetCapsule.height / 2;
        }


        private void OnTriggerEnter(Collider other)
        {
            //print("Hey I collided with " + other.name);


            if (other.GetComponent<Health>() != projectileTarget ) return;
            if(other.GetComponent<Health>().IsDead()) return;
            projectileTarget.TakeDamage(instigator, damage);

            speed = 0;

            onProjectileHit.Invoke();
            
            if(hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }

            foreach(GameObject toDestroy in destroyOnhit)
            {
                Destroy(toDestroy);
            }

            Destroy(gameObject, lifeAfterImpact);

        }
    }
}