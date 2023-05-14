using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MousePosition : MonoBehaviour
{
    public static Vector3 MouseWorldPosition()// Vector2 MousePosition2D)
    {
        Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = Camera.main.nearClipPlane;
        Vector3 WorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 WorldPosition2D = new Vector3(WorldPosition.x, WorldPosition.y, 0.0f);
        //Debug.Log(WorldPosition2D);
        return WorldPosition2D;
    }
}
