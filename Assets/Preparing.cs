using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class Preparing : MonoBehaviour
{
    public Transform mice;
    public Transform cat;
    public Transform cheese;
    private int placed = 0;
    public Text text;
    public GameObject button;
    public List<GameObject> furnitures;
    public GameObject target;


    // Start is called before the first frame update
    void Start()
    {
        furnitures = new List<GameObject>(GameObject.FindGameObjectsWithTag("Furniture"));

    }

    // Update is called once per frame
    void Update()
    {
        switch (placed)
        {
            case 0:
                {
                    text.text = "Кликните на место, где будет находиться мышь";

                    if (Input.GetMouseButtonUp(0))
                    {
                        Placing(mice);
                    }
                    break;
                }

            case 1:
                {
                    
                    text.text = "Кликните на место, где будет находиться кот";

                    if (Input.GetMouseButtonUp(0))
                    {
                        if (EventSystem.current.IsPointerOverGameObject())
                        {
                            return;
                        }
                            Placing(cat);
                    }
                    break;
                }
            case 2:
                {
                    text.text = "Кликните на место, где будет находиться сыр";

                    if (Input.GetMouseButtonUp(0))
                    {
                        if (EventSystem.current.IsPointerOverGameObject())
                        {
                            return;
                        }
                        Placing(cheese);
                    }
                    break;
                }
            case 3:
                {
                    text.text = "Кликните на места, где будет находиться мебель";

                    if (Input.GetMouseButtonUp(0))
                    {
                        if (EventSystem.current.IsPointerOverGameObject())
                        {
                            return;
                        }
                        CreateFurniture();
                    }
                    
                    break;
                }
            case 4:
                {
                    text.text = "Кликните на места, где будут находиться точки маршрута";

                    if (Input.GetMouseButtonUp(0))
                    {
                        if (EventSystem.current.IsPointerOverGameObject())
                        {
                            return;
                        }
                        CreateTargets();
                    }

                    break;
                }
        }

        if ((placed > 2) && (placed != 5))
        {
            button.SetActive(true);
        }
    
}

    public void Inc_placed() 
    {
        placed++;
        button.SetActive(false);
        Debug.Log("Clicked");

        if (placed == 4)
        {
            
            button.GetComponentInChildren<Text>().text = "Старт";
        }

        if (placed == 5)
        {
            button.SetActive(false);
            text.gameObject.SetActive(false);
            List<GameObject> targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("targets"));
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].GetComponent<TargetProperties>().enabled = true;
            }
            cat.gameObject.GetComponent<GoToMice>().enabled = true;
            mice.gameObject.GetComponent<Animator>().enabled = true;
            mice.gameObject.GetComponent<GoToTargetV2>().enabled = true;
            
            

        }
    }

    private void Placing(Transform gameObj)
    { 
        gameObj.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 1, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);
        button.SetActive(true);
    }

    private void CreateFurniture() 
    {
        
        int furnitureIndex = Random.Range(0, furnitures.Count);
        GameObject newFurniture = Instantiate(furnitures[furnitureIndex], new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 1, Camera.main.ScreenToWorldPoint(Input.mousePosition).z), Quaternion.identity);
    }

    private void CreateTargets()
    {
        GameObject newTarget = Instantiate(target, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 1, Camera.main.ScreenToWorldPoint(Input.mousePosition).z), Quaternion.Euler(90, 0,0));

    }

    public void NewGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
