using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToTarget : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;

    public List<GameObject> sortTargets;
    public List<GameObject> safeTargets;
    public Dictionary<string, GameObject> targetsDict;
    private List<GameObject> array;

    // Start is called before the first frame update
    void Start()
    {
        array = new List<GameObject>(GameObject.FindGameObjectsWithTag("targets"));
        navMeshAgent = GetComponent<NavMeshAgent>();
        sortTargets = QuickSort(0, array.Count - 1);
        for (int i = 0; i < sortTargets.Count; i++)
        {
            if (sortTargets[i].GetComponent<TargetProperties>().safe == true)
            {
                safeTargets.Add(sortTargets[i]); 
            }
        }
    }

    /*public void DeleteTarget(string key)
    {
        targetsDict.Remove(key);
    }

    public void AddTarget(string key, GameObject newTarget)
    {
        targetsDict.Add(key, newTarget);
    } */

    // Update is called once per frame
    void Update()
    {
        
       
            if (safeTargets[0].GetComponent<TargetProperties>().isChecked == true) 
            {
                safeTargets.Clear();
                sortTargets = QuickSort(0, array.Count - 1);
                for (int i = 0; i < sortTargets.Count; i++)
                {
                    if ((sortTargets[i].GetComponent<TargetProperties>().safe == true) && (sortTargets[i].GetComponent<TargetProperties>().isChecked == false))
                    {
                        safeTargets.Add(sortTargets[i]);
                    }
                }

        }     
        
        navMeshAgent.destination = safeTargets[0].transform.position;

    }

    private List<GameObject> QuickSort( int minIndex, int maxIndex)
    {
        if (minIndex >= maxIndex)
        {
            return array;
        }

        int pivotIndex = GetPivotIndex(minIndex, maxIndex);

        QuickSort( minIndex, pivotIndex - 1);

        QuickSort( pivotIndex + 1, maxIndex);

        return array;
    } 

    private int GetPivotIndex(int minIndex, int maxIndex)
    {
        int pivot = minIndex - 1;

        for (int i = minIndex; i <= maxIndex; i++)
        {
            if (Vector3.Distance(new Vector3(transform.position.x, 1, transform.position.z), new Vector3(array[i].transform.position.x, 1, array[i].transform.position.z)) < Vector3.Distance(new Vector3(transform.position.x, 1, transform.position.z), new Vector3(array[maxIndex].transform.position.x, 1, array[maxIndex].transform.position.z)))
            {
                pivot++;
                Swap( pivot,   i);
            }
        }

        pivot++;
        Swap( pivot, maxIndex);

        return pivot;
    }

    private void Swap(int leftItem, int rightItem)
    {
        GameObject temp = array[leftItem];

        array[leftItem] = array[rightItem];

        array[rightItem] = temp;
    }
}
