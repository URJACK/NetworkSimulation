using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
    public float moveSpeed;
    public float rotateRate;
    private Vector2 first, second;
    private void OnGUI()
    {
        if (Event.current.type == EventType.MouseDown)
        {
            first = Event.current.mousePosition;
        }
        else if (Event.current.type == EventType.MouseDrag)
        {
            second = Event.current.mousePosition;
            this.transform.Rotate((second.y - first.y) * rotateRate, (second.x - first.x) * rotateRate, 0);
            first = second;
        }
    }
    void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            transform.Translate(0, 0, moveSpeed);
        }
        else if (Input.GetKey("s"))
        {
            transform.Translate(0, 0, -moveSpeed);
        }
        else if (Input.GetKey("a"))
        {

            transform.Translate(-moveSpeed, 0, 0);
        }
        else if (Input.GetKey("d"))
        {

            transform.Translate(moveSpeed, 0, 0);
        }
        else if (Input.GetKey("q"))
        {

            transform.Translate(0, -moveSpeed, 0);
        }
        else if (Input.GetKey("e"))
        {
            transform.Translate(0, moveSpeed, 0);
        }
    }
}