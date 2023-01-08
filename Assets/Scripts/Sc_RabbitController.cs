using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sc_RabbitController : MonoBehaviour
{
    private Sc_RabbitData rabbit;
    private NavMeshAgent agent;
    [SerializeField]
    private Vector3 destination;
    private const float F_MIN_DISTANCE_TOFOLLOW = 0.83f;
    private const float F_MIN_DISTANCE_TOMOVEORDER = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        rabbit = GetComponent<Sc_RabbitData>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rabbit.state == Sc_RabbitData.RabbitState.Lost) return;
        if (rabbit.state == Sc_RabbitData.RabbitState.Following)
        {
            destination = Sc_Player.Instance.transform.position;
            if (agent.isStopped && Vector3.Distance(this.transform.position, Sc_Player.Instance.transform.position) > F_MIN_DISTANCE_TOFOLLOW)
            {
                FollowPlayer();
            }
            else if (!agent.isStopped && Vector3.Distance(this.transform.position, Sc_Player.Instance.transform.position) < F_MIN_DISTANCE_TOFOLLOW)
            {
                StopMovement();
            }
            //Debug.Log(Vector3.Distance(this.transform.position, Sc_Player.Instance.transform.position));
        }
        if (rabbit.state == Sc_RabbitData.RabbitState.MovingTo && Vector3.Distance(this.transform.position, destination) < F_MIN_DISTANCE_TOMOVEORDER)
        {
            rabbit.SetState( Sc_RabbitData.RabbitState.Waiting);
            StopMovement();
        }
    }

    public void FollowPlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(Sc_Player.Instance.transform.position);
    }


    public void MoveTo(Vector3 position)
    {
        destination = position;
        agent.isStopped = false;
        agent.SetDestination(destination);
    }
    public void StopMovement()
    {
        agent.isStopped = true;
        destination = Vector3.zero;
    }
}
