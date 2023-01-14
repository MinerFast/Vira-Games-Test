using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlatformTrigger : MonoBehaviour
{
    private NavMeshAgent agent;
    private NavMeshSurface surface;


    private const string playerName = "Ball";
    private const string surfaceName = "Surface";
    #region MonoBehaviour
    private void Awake()
    {
        if (GameManager.isCheatOn)
        {
            surface = GameObject.Find(surfaceName).GetComponent<NavMeshSurface>();
            agent = GameObject.Find(playerName).GetComponent<NavMeshAgent>();
            agent.SetDestination(this.transform.position);
            surface.BuildNavMesh();
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<BallController>())
        {
            Invoke("DropPlatform", 0.9f);
        }
    }
    #endregion
    #region DropPlatform

    void DropPlatform()
    {
        GetComponentInParent<Rigidbody>().useGravity = true;
        GetComponentInParent<Rigidbody>().isKinematic = false;
        Destroy(transform.parent.gameObject, 2.0f);
    }
    #endregion
}
