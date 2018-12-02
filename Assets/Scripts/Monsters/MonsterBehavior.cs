using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehavior : MonoBehaviour {
    [SerializeField]
    private GameObject explosion;

    private int hp;
    private Transform target;

    private NavMeshAgent agent;

    [SerializeField]
    private Pooling pooling;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetData() {
        hp = GamePlayManager.Instance.GetHP(gameObject.tag);
        if(hp <= 0) {
            gameObject.SetActive(false);
        } else {
            target = GamePlayManager.Instance.GetPlayerTransform();
        }
    }

    private void Update() {
        if(target != null) {
            agent.destination = target.position;
            float distance = Vector3.Distance(transform.position, target.position);
            if(distance < 2f) {
                GameObject obj = pooling.Spawn(explosion, transform.position, transform.rotation);
                var disableObj = obj.GetComponent<DisableMe>();
                disableObj.StopAllCoroutines();
                gameObject.SetActive(false);
            }
        }
    }

    public void AddDamage(int dmg) {
        hp = hp - dmg;
        if(hp <= 0) {
            GamePlayManager.Instance.AddScore(gameObject.tag);
            GameObject obj = pooling.Spawn(explosion, transform.position, transform.rotation);
            var disableObj = obj.GetComponent<DisableMe>();
            disableObj.StopAllCoroutines();
            gameObject.SetActive(false);
        }
    }
}
