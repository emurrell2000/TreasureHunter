using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMover : MonoBehaviour
{
    public Transform startMarker;
    public Transform endMarker;
    public float startX;
    public float startY;
    public float endX;
    public float endY;

    // starting value for the Lerp
    static float t = 0.0f;
    void Start()
    {
        startX = startMarker.position.x;
        startY = startMarker.position.y;
        endX = endMarker.position.x;
        endY = endMarker.position.y;
    }

    void Update()
    {
        // animate the position of the game object...
        transform.position = new Vector3(Mathf.Lerp(startX, endX, t), Mathf.Lerp(startY, endY, t), 0);

        // .. and increase the t interpolater
        t += 0.5f * Time.deltaTime;

        // now check if the interpolator has reached 1.0
        // and swap end and start so game object moves
        // in the opposite direction.
        if (t > 1.0f)
        {
            float temp = endX;
            endX = startX;
            startX = temp;

            temp = endY;
            endY = startY;
            startY = temp;
            t = 0.0f;
        }
    }
}