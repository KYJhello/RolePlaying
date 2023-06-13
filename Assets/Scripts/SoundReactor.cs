using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoundReactor : MonoBehaviour, IListenable
{
    Transform playerTs;
    private NavMeshAgent agent;
    private Coroutine moveRoutine;
    Vector3 dir;
    [SerializeField] float moveSpeed;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Listen(Transform trans)
    {
        playerTs = trans;
        StartCoroutine(LookAtRoutine());
        StartMove();
    }

    IEnumerator LookAtRoutine()
    {
        while (true)
        {
            dir = (playerTs.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
            

            yield return null;
        }
    }

    public void StartMove()
    {
        agent.destination = playerTs.position;
        moveRoutine = StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            if(Vector3.Distance(transform.position, playerTs.position) > 5f)
            {
                StopCoroutine(moveRoutine);
                yield return new WaitForSeconds(0.2f);
            }
            transform.Translate(dir * moveSpeed * Time.deltaTime);

            yield return new WaitForSeconds(0.2f);
        }
    }
}
