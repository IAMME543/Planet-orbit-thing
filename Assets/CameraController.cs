using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float ZoomIntensity = 200;
    private Vector3 mousePosition;
    private Vector3 lastMousePosition;
    private float newSize = 5;

    void Update() {
        mousePosition = Input.mousePosition;

        transform.Translate(Panning());

        lastMousePosition = mousePosition;

        newSize = Camera.main.orthographicSize - Input.mouseScrollDelta.y * ZoomIntensity;
        newSize = Mathf.Clamp(newSize, 1, 10);
        Camera.main.orthographicSize = newSize;

    }
    public Vector3 Panning() {
        Vector3 delta = new Vector3(0, 0, 0);
        if (Input.GetMouseButton(2))
        {
            delta = mousePosition - lastMousePosition;
        }
        return .01f * newSize * -delta.normalized;
        
    }
}
