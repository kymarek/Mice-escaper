using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGame : MonoBehaviour
{

    public GameObject text;
    public Transform mouse;
    public GameObject newGameButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(mouse.position, transform.position) < 3) 
        {
            text.GetComponent<Text>().text = "Вы победили, мышь достигла сыра";
            text.SetActive(true);

            newGameButton.SetActive(true);
        }
        
    }

}
