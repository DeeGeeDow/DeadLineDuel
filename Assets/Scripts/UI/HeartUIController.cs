using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUIController : MonoBehaviour
{
    // Start is called before the first frame update
    public int numberOfHeart;
    public bool isPlayer1;
    public GameObject heartPrefab;
    private Stack<GameObject> hearts;
    void Start()
    {
        clearHeart();
        for(int i=0; i<numberOfHeart; i++)
        {
            createHeart();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void clearHeart()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new Stack<GameObject>();
    }
    void createHeart()
    {
        GameObject heart = Instantiate(heartPrefab);
        heart.transform.SetParent(transform);
        hearts.Push(heart);
    }

    public void updateHeart(Component sender, object data)
    {
        if (sender is PlayerHeartController && sender.gameObject.GetComponent<PlayerController>().isPlayer1 == isPlayer1)
        {
            if(data is int)
            {
                int heartAfterChanged = (int) data;

                if(numberOfHeart > heartAfterChanged)
                {
                    for(int i=numberOfHeart; i>heartAfterChanged; i--){
                        GameObject heartToRemove = hearts.Pop();
                        Destroy(heartToRemove);
                    }
                }
                else if (numberOfHeart < heartAfterChanged)
                {
                    for(int i=numberOfHeart; i<heartAfterChanged; i++)
                    {
                        hearts.Push(Instantiate(heartPrefab));
                    }
                }

                numberOfHeart = heartAfterChanged;
                //temp
                if (isPlayer1)
                {
                    Debug.Log("Player 1's heart : " + numberOfHeart);
                }
                else
                {
                    Debug.Log("Player 2's heart : " + numberOfHeart);
                }
            }
        }
    }
}