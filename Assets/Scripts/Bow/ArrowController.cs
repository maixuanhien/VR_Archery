using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

    [SerializeField]
    private float force = 10f;
    [SerializeField]
    private int damage = 1;

    private Transform fulcum;
    private float distanceOrigin;

    private Rigidbody rigidArrow;

    public void SetData(Transform newFulcum) {
        if(rigidArrow == null) {
            rigidArrow = GetComponent<Rigidbody>();
        }
        rigidArrow.useGravity = false;
        rigidArrow.isKinematic = true;
        rigidArrow.velocity = Vector3.zero;
        StopAllCoroutines();

        fulcum = newFulcum;
        distanceOrigin = Vector3.Distance(transform.position, fulcum.position);
    }

    private void Update() {
        if (fulcum != null) {
            Vector3 direction = fulcum.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }
 
    private void OnTriggerEnter(Collider other) {
        var monsterBehavior = other.gameObject.GetComponent<MonsterBehavior>();
        if (monsterBehavior != null) {
            monsterBehavior.AddDamage(damage);
        }
    }

    public void Attack() {
        if (fulcum != null) {
            transform.parent = null;
            rigidArrow.useGravity = true;
            rigidArrow.isKinematic = false;
            float distance = Vector3.Distance(transform.position, fulcum.position);
            if (distance > distanceOrigin) {
                rigidArrow.velocity = transform.forward * (distance - distanceOrigin) * force;
            } else {
                rigidArrow.velocity = Vector3.zero;
            }
            fulcum = null;
            StartCoroutine(Disable());
        }
    }

    private IEnumerator Disable() {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
