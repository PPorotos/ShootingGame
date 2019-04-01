using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMoveAgent : MonoBehaviour
{
    public List<Transform> wayPoints;
    public int nextIdx;

    private readonly float patrolSpeed = 1.5f;
    private readonly float traceSpeed = 4.0f;
    //회전할때의 속도를 조절하는 계수
    private float damping = 1.0f;

    //NavMeshAgent 컴포넌트를 저장할 변수
    private NavMeshAgent agent;
    private Transform enemyTransform;

    //순찰여부를 판단하는 변수
    private bool _patrolling;
    public bool patrolling
    {
        get { return _patrolling; }
        set
        {
            _patrolling = value;
            if (_patrolling)
            {
                agent.speed = patrolSpeed;
                //순찰 상태의 회전계수
                damping = 1.0f;
                MoveWayPoint();
            }
        }
    }
    private Vector3 _traceTarget;
    public Vector3 traceTarget
    {
        get { return _traceTarget; }
        set
        {
            _traceTarget = value;
            agent.speed = traceSpeed;
            //추적 상태의 회전계수
            damping = 7.0f;
            TraceTarget(_traceTarget);
        }
    }
    public float speed
    {
        get { return agent.velocity.magnitude; }
    }
    private void Start()
    {
        enemyTransform = transform;
        agent = GetComponent<NavMeshAgent>();
        //목적지에 가가워질수록 속도를 줄이는 옵션을 비활성화
        agent.autoBraking = false;
        agent.updateRotation = false;
        agent.speed = patrolSpeed;

        var group = GameObject.Find("WayPointGroup");
        if(group != null)
        {
            group.GetComponentsInChildren<Transform>(wayPoints);
            wayPoints.RemoveAt(0);
        }
        MoveWayPoint();
    }

    void MoveWayPoint()
    {
        //최단거리 경로 계산이 끝나지 않았으면 다음을 수행하지 않음
        if (agent.isPathStale) return;

        //다음 목적지를 wayPoints 배열에서 추출한 위치로 다음 목적지를 지정
        agent.destination = wayPoints[nextIdx].position;
        //내비게이션 기능을 활성화해서 이동을 시작함
        agent.isStopped = false;
    }

    //주인공을 추적할대 이동시키는 함수
    void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale) return;
        agent.destination = pos;
        agent.isStopped = false;
    }

    public void Stop()
    {
        agent.isStopped = true;
        //바로 정지하기 위해 속도를 0으로 설정
        agent.velocity = Vector3.zero;
        _patrolling = false;
    }
    private void Update()
    {
        if(agent.isStopped == false)
        {
            Quaternion enemyRotation = Quaternion.LookRotation(agent.desiredVelocity);
            enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, enemyRotation, Time.deltaTime * damping);
        }
        if (!_patrolling) return;
        //NavMeshAgent 가 이동하고 있고 목적지에 도착했는지 여부를 계산
        if(agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
        {
            //다음 목적지의 배열 첨자를 계산
            nextIdx = ++nextIdx % wayPoints.Count;
            //다음목적지로 이동 멸령을 수행 
            MoveWayPoint();
        }

    }
}
