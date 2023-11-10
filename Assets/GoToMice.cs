using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GoToMice : MonoBehaviour
{
    public GameObject mice;
    private NavMeshAgent catAgent;
    [SerializeField] private List<GameObject> furniture;
    Animator catAnimator;
    private bool isSitting;
    public GameObject text;

    public GameObject newGameButton;



    // Start is called before the first frame update
    void Start()
    {
        catAgent = GetComponent<NavMeshAgent>();
        furniture = new List<GameObject>(GameObject.FindGameObjectsWithTag("Furniture"));
        catAnimator = GetComponent<Animator>();


       
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < furniture.Count; i++) {
            if (Vector3.Distance(new Vector3(mice.transform.position.x, 1, mice.transform.position.z), new Vector3(furniture[i].transform.position.x, 1, furniture[i].transform.position.z)) <= 10)
            {
                catAgent.speed = 0;           
                break;
            }
            else 
            {
                catAgent.speed = 10;
            }
        }

        if (catAgent.speed != 0)
        {
            catAnimator.SetTrigger("isRunning");
            catAnimator.ResetTrigger("isSitting");
            isSitting = false;
        }
        else 
        {
            if (isSitting == false)
            {
                catAnimator.SetTrigger("isSitting");
                catAnimator.ResetTrigger("isRunning");
                isSitting = true;
                new WaitForSeconds(1.5f);
                
           }
        }

        catAgent.destination = mice.transform.position;

        if (Vector3.Distance(new Vector3(mice.transform.position.x, 1, mice.transform.position.z), new Vector3(transform.position.x, 1, transform.position.z)) <= 8)
            {
            catAnimator.SetTrigger("isSitting");
            catAgent.speed = 0;
            catAgent.angularSpeed = 0;

            if (isSitting == false)
            {
                catAnimator.SetTrigger("isSitting");
                catAnimator.ResetTrigger("isRunning");
                isSitting = true;
                new WaitForSeconds(1.5f);

            }

            NavMeshAgent miceAgent;
            miceAgent = mice.GetComponent<NavMeshAgent>();
            mice.GetComponent<GoToTargetV2>().enabled = false;
            miceAgent.speed= 0;
            miceAgent.angularSpeed= 0;
            miceAgent.enabled = false;
            mice.GetComponent<Animator>().SetTrigger("dead");
            GetComponent<GoToMice>().enabled = false;

            text.GetComponent<Text>().text = "Вы проиграли, мышь была съедена";
            text.SetActive(true);

            newGameButton.SetActive(true);


        }
    }

}
