using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    [SerializeField]
    private List<GameObject> monsters;
    [SerializeField]
    private float spawnRate = 5f;

    private float spawnTime = 0f;
    
    [SerializeField]
    private Pooling pooling;

    private void Update() {
        if (monsters.Count > 0) {
            if (Time.time > spawnTime) {
                spawnTime = spawnTime + spawnRate;
                int difficulty = GamePlayManager.Instance.GetDiffCulty();
                if (difficulty > 0) {
                    int nb = (int)Random.Range(1, difficulty + 1);
                    if (nb > monsters.Count) {
                        nb = monsters.Count;
                    }
                    GameObject obj = pooling.Spawn(monsters[nb - 1], transform.position, transform.rotation);
                    var monsterBehavior = obj.GetComponent<MonsterBehavior>();
                    monsterBehavior.SetData();
                }
            }
        }
    }
}
