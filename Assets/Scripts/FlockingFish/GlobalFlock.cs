using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GlobalFlock : MonoBehaviour {

    [SerializeField]
    private GameObject fish;
    [SerializeField]
    private int amount;
    [SerializeField]
    private float zoneSize = 5.0f;
    [SerializeField]
    private List<GameObject> fishList;

    private float actualZoneSize = 5.0f;
    private Vector3 positionOrigine;
    private Vector3 target;

    private void Update() {
        positionOrigine = transform.position;
        target = positionOrigine;
        if (amount != transform.childCount || actualZoneSize != zoneSize) {
            actualZoneSize = zoneSize;
            List<GameObject> childs = new List<GameObject>();
            foreach (Transform child in transform) {
                childs.Add(child.gameObject);
            }
            foreach (GameObject go in childs) {
                DestroyImmediate(go);
            }
            fishList = new List<GameObject>();
            for (int i = 0; i < amount; i++) {
                float xRandom = Random.Range(-zoneSize, zoneSize);
                float yRandom = Random.Range(-zoneSize, zoneSize);
                float zRandom = Random.Range(-zoneSize, zoneSize);
                Vector3 position = new Vector3(positionOrigine.x + xRandom, positionOrigine.y + yRandom, positionOrigine.z + zRandom);
                GameObject obj = Instantiate(fish, position, Quaternion.identity);
                obj.transform.parent = transform;
                var flock = obj.GetComponent<Flock>();
                flock.SetData(zoneSize / 2f);
                fishList.Add(obj);
            }
        }
    }

    public Vector3 GetTarget() {
        return target;
    }

    public float GetZoneSize() {
        return zoneSize;
    }

    public List<GameObject> GetFishList() {
        return fishList;
    }
}
