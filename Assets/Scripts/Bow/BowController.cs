using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour {

    [SerializeField]
    private OVRGrabbable stringGrabbable;
    private OVRGrabbable bowGrabbable;

    private Vector3 positionOrigin;
    private Quaternion rotationOrigin;

    private void Start() {
        bowGrabbable = GetComponent<OVRGrabbable>();
        positionOrigin = transform.position;
        rotationOrigin = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.LAYER_DECO)) {
            ComeBackPositionOrigin();
        }
    }

    public void ActiceStringBow() {
        if (!stringGrabbable.isActiveAndEnabled) {
            stringGrabbable.enabled = true;
        }
    }

    public void DisableStringBow() {
        if (stringGrabbable.isActiveAndEnabled) {
            stringGrabbable.enabled = false;
        }
    }

    public void ComeBackPositionOrigin() {
        transform.position = positionOrigin;
        transform.rotation = rotationOrigin;
        DisableStringBow();
    }
}
