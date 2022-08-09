using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singlton<CameraController>
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(0, 10, 0);


    private void Update()
    {
        if (player == null) return;
        transform.position = player.position + offset;
    }


    public void SetPlayer(Transform p) => player = p;
}
