using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    [SerializeField] private float speed = 6f;


    private void Update()
    {
        Movement(Vector3.forward);
    }


    private void Movement(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

}
