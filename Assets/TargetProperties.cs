using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetProperties : MonoBehaviour
{
    
    public bool isChecked;
    public bool safe;

    [SerializeField] private List<GameObject> furniture;

    // Start is called before the first frame update
    void Start()
    {
        furniture = new List<GameObject>(GameObject.FindGameObjectsWithTag("Furniture"));

        

        for (int i = 0; i < furniture.Count; i++)
        {
          
            if (Vector3.Distance(new Vector3(transform.position.x,1, transform.position.z), new Vector3(furniture[i].transform.position.x, 1, furniture[i].transform.position.z)) <= 10)
            {
                safe = true;
            }
        }

        if (safe)
        {
            GetComponent<MeshRenderer>().materials[0].color = Color.green;
        }
        else
        {
            GetComponent<MeshRenderer>().materials[0].color = Color.red;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        isChecked = true;
    }
}
