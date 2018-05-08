using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class KillOffScreen : MonoBehaviour
{
    [SerializeField]
    Rect buffer;
    // Update is called once per frame
    void FixedUpdate()
    {

        Camera cam = Camera.main;
        var bottomLeft = cam.ScreenToWorldPoint(Vector3.zero) + (Vector3)buffer.position;
        var topRight = cam.ScreenToWorldPoint(new Vector3(
            cam.pixelWidth, cam.pixelHeight)) - (Vector3)buffer.size;

        var cameraRect = new Rect(
            bottomLeft.x,
            bottomLeft.y,
            topRight.x - bottomLeft.x,
            topRight.y - bottomLeft.y);



        var new_pos = new Vector3(
            Mathf.Clamp(transform.position.x, cameraRect.xMin, cameraRect.xMax),
            Mathf.Clamp(transform.position.y, cameraRect.yMin, cameraRect.yMax),
            transform.position.z);
        if (!new_pos.Equals(transform.position))
        {
            Destroy(gameObject);
        }

    }

}
