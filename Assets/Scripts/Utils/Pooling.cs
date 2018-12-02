using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour {
    [SerializeField]
    private List<GameObject> listObj = new List<GameObject>();
    [SerializeField]
    private Dictionary<int, List<GameObject>> poolObj = new Dictionary<int, List<GameObject>>();
    private int GetIndex(GameObject prefab) {
        return listObj.FindIndex(t => t == prefab);
    }
    private List<GameObject> GetPoolObj(GameObject prefab) {
        int index = GetIndex(prefab);
        if (index < 0) {
            listObj.Add(prefab);
            index = listObj.Count - 1;
        }
        //get list object pooling
        if (poolObj.ContainsKey(index)) {
            return poolObj[index];
        }
        poolObj.Add(index, new List<GameObject>());
        return poolObj[index];
    }

    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null) {
        int index = GetIndex(prefab);
        if (index < 0) {
            listObj.Add(prefab);
            index = listObj.Count - 1;
        }
        //get list object pooling
        if (!poolObj.ContainsKey(index)) {
            poolObj.Add(index, new List<GameObject>());
        }
        foreach (var item in poolObj[index]) {
            if (!item.activeInHierarchy) {
                if (parent != null) {
                    item.transform.SetParent(parent);
                    item.transform.localScale = Vector3.one;
                }
                item.transform.SetPositionAndRotation(position, rotation);
                item.SetActive(true);
                return item;
            }
        }
        GameObject itemReturn = Instantiate(prefab, position, rotation);
        if (parent != null) {
            itemReturn.transform.SetParent(parent);
            itemReturn.transform.localScale = Vector3.one;
        }
        itemReturn.SetActive(true);
        poolObj[index].Add(itemReturn);
        return itemReturn;
    }
}
