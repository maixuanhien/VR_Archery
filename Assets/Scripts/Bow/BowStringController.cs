using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowStringController : MonoBehaviour {
    [SerializeField]
    private Transform fulcrum;

    private Vector3 arrowLocalRotationAngle = new Vector3(0f, 90f, 90f);
    private GameObject currentArrow;
    private ArrowController arrowController;
    private bool isCreated = false;

    private OVRGrabbable bowStringGrabbable;
    private Vector3 localPosition;
    private Quaternion localRotation;
    private bool reset = true;

    [SerializeField]
    private Pooling pooling;

    private void Start() {
        bowStringGrabbable = GetComponent<OVRGrabbable>();
        localPosition = transform.localPosition;
        localRotation = transform.localRotation;
    }

    private void Update() {
        if (bowStringGrabbable.isActiveAndEnabled) {
            if (bowStringGrabbable.isGrabbed) {
                reset = false;
            } else {
                reset = true;
            }
        } else {
            reset = true;
        }
        if (reset) {
            if (isCreated) {
                arrowController.Attack();
                isCreated = false;
            }
            transform.localPosition = localPosition;
            transform.localRotation = localRotation;
        } else {
            if (!isCreated) {
                currentArrow = pooling.Spawn(GamePlayManager.Instance.GetArrow(), transform.position, transform.rotation);
                currentArrow.transform.parent = transform;
                currentArrow.transform.localRotation = Quaternion.Euler(arrowLocalRotationAngle);
                isCreated = true;
                arrowController = currentArrow.GetComponent<ArrowController>();
                arrowController.SetData(fulcrum);
            }
        }
    }
}
