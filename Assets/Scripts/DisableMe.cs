using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMe : MonoBehaviour {

    [SerializeField]
    private float delay = 2f;

    private void Start() {
        StartCoroutine(Disable(delay));
    }



    private IEnumerator Disable(float delay) {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
