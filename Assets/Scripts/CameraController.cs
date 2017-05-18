using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public Vector3 focusPoint;
    public float sensitivityX = 75;
    public float sensitivityY = 75;
    public float sensitivityZ = 75;

    public MinMax angle = new MinMax(320, 50);

    void Start() {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity)) {
            focusPoint = hit.point;
        }
    }

    void Update() {
        if (!Input.GetMouseButton(1)) return;

        var sideRotation = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        var heightRotation = Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime * -1;
        var zoom = Input.GetAxis("Mouse ScrollWheel") * sensitivityZ * Time.deltaTime;

        var distance = (transform.position - focusPoint).magnitude;

        transform.position = focusPoint;
        var oldHeightRotation = transform.rotation.eulerAngles.x;

        transform.Rotate(-oldHeightRotation, 0, 0);
        transform.Rotate(0, sideRotation, 0);
        
        if (heightRotation > 0 && oldHeightRotation <= angle.max + 1) {
            heightRotation = Mathf.Min(oldHeightRotation + heightRotation, angle.max);
        } else if (heightRotation < 0 && oldHeightRotation >= angle.min - 1) {
            heightRotation = Mathf.Max(oldHeightRotation + heightRotation, angle.min);
        } else {
            heightRotation = oldHeightRotation + heightRotation;
        }

        transform.Rotate(heightRotation, 0, 0);

        transform.Translate(transform.InverseTransformVector(transform.forward) * -distance, Space.Self);
    }
}