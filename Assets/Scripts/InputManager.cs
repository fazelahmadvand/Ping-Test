using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singlton<InputManager>
{
    [SerializeField] private Collider ground;



    [SerializeField] private Camera cam;

    private Camera Cam
    {
        get
        {
            if (cam == null)
                cam = Camera.main;
            return cam;
        }
    }

    public Vector3 MousePos
    {
        get
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool isHit = ground.Raycast(ray, out var hit, Mathf.Infinity);
            if (isHit)
                return hit.point;
            return Vector3.zero;

        }
    }





}
