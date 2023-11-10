using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToTargetV2 : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;

    public List<GameObject> targets;
    public GameObject goTo; 

    // Start is called before the first frame update
    void Start()
    {
    
        navMeshAgent = GetComponent<NavMeshAgent>();
        targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("targets"));
        goTo = targets[0];
    }

   
    void Update()
    {


        foreach (GameObject target in targets)
        {
            if ((target.GetComponent<TargetProperties>().isChecked) || (target.GetComponent<TargetProperties>().safe == false))
            {
                targets.Remove(target);
                goTo = targets[0];
                break;
            }

            if (Vector3.Distance(goTo.transform.position, transform.position) > Vector3.Distance(transform.position, target.transform.position))
            {
                goTo = target;
            }

        }
        
        navMeshAgent.destination = goTo.transform.position;


    }

   
}
