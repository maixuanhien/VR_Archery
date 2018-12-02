using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {

    public float speed;
    public float rotationSpeed;
    public float neighbourDistance;

    public bool turning = false;

    private GlobalFlock global;
    private const float ROTATION_SPEED = 4f;

    private void Start() {
        global = transform.parent.GetComponent<GlobalFlock>();
    }

    public void SetData(float newSpeed) {
        speed = newSpeed;
        rotationSpeed = ROTATION_SPEED;
        neighbourDistance = transform.localScale.x * 2f;
    }

    private void Update() {
        if (Vector3.Distance(transform.position, global.GetTarget()) >= global.GetZoneSize()) {
            turning = true;
        } else {
            turning = false;
        }
        if (turning) {
            Vector3 direction = global.GetTarget() - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        } else {
            if (Random.Range(0, 5) < 1) {
                applyRules();
            }
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    void applyRules() {
        List<GameObject> fishList = global.GetFishList();
        Vector3 targetPos = global.GetTarget();

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;

        float distance;
        float groupSpeed = 0.1f;
        int groupSize = 0;

        foreach (GameObject fish in fishList) {
            if (fish != gameObject) {
                distance = Vector3.Distance(fish.transform.position, transform.position);
                if (distance < neighbourDistance) {
                    vcentre = vcentre + fish.transform.position;
                    groupSize++;
                    if (distance < neighbourDistance / 2f) {
                        vavoid = vavoid + (transform.position - fish.transform.position);
                    }
                    var anotherFlock = fish.GetComponent<Flock>();
                    groupSpeed = groupSpeed + anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0) {
            vcentre = vcentre / groupSize + (targetPos - transform.position);
            speed = groupSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero) {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            }
        }
    }
}
