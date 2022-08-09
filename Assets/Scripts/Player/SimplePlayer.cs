using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;


public class SimplePlayer : NetworkBehaviour
{
    [SerializeField] [SyncVar(hook = nameof(OnPositionChange))] private Vector3 pos;
    [SerializeField] [SyncVar(hook = nameof(OnRotationChange))] private Vector3 rot;


    [Space]
    [Header("Player Info")]
    [SerializeField] private float speed;
    [SerializeField] private NavMeshAgent agent;


    [Space]
    [Header("Shoot Stuff")]
    [SerializeField] private Transform shootHole;
    [SerializeField] private Transform tankShootHole;
    [SerializeField] private SimpleBullet bulletPre;

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        agent.speed = speed;
        if (hasAuthority)
            CameraController.Instance.SetPlayer(transform);

    }


    private void Update()
    {
        if (!hasAuthority) return;

        if (Input.GetMouseButtonDown(0))
            CmdChangePosition();
        
        CmdChangeRotation();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdShoot();
        }

    }


    private void OnPositionChange(Vector3 oldVector, Vector3 newVector)
    {
        agent.SetDestination(newVector);
    }

    private void OnRotationChange(Vector3 oldVector, Vector3 newVector)
    {
        var diff = newVector - transform.position;
        diff.y = 0;
        var rot = Quaternion.LookRotation(diff);
        tankShootHole.rotation = rot;
    }

    [Command]
    private void CmdChangePosition()
    {
        pos = InputManager.Instance.MousePos;
    }

    [Command]
    private void CmdChangeRotation()
    {
        rot = InputManager.Instance.MousePos;
    }

    [Command]
    private void CmdShoot()
    {
        var bullet = Instantiate(bulletPre);
        bullet.transform.SetPositionAndRotation(shootHole.position, shootHole.rotation);

    }

    [ClientRpc]
    private void RpcShoot()
    {
        var bullet = Instantiate(bulletPre);
        bullet.transform.SetPositionAndRotation(shootHole.position, shootHole.rotation);

    }

}
