using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public List<float> probability = new List<float>();
    public GameObject obstaclePrefab;
    public List<GameObject> obstacles = new List<GameObject>();
    public int numOfObstacleStart = 7;

    private void Awake()
    {
        for(int i=0; i<this.transform.childCount; i++)
        {
            GameObject child = this.transform.GetChild(i).gameObject;
            obstacles.Add(child);
        }
    }
    private void Start()
    {
        //startSpawnObstacle();
    }

    void spawnObstacle()
    {
        /*
        Vector3 obstacle_position = new Vector3();
        obstacle_position.x = Random.Range(Boundaries.HACKER_BOUND[2]+1, -Boundaries.HACKER_BOUND[2]-1);
        obstacle_position.z = Random.Range(Boundaries.HACKER_BOUND[0], Boundaries.HACKER_BOUND[1]);
        obstacles.Add(Instantiate(obstaclePrefab, obstacle_position, new Quaternion()));
        */
    }

    void startSpawnObstacle()
    {
        for(int i= (int) Boundaries.HACKER_BOUND[0]; i>= Boundaries.HACKER_BOUND[1]; i--)
        {
            Vector3 obstacle1_position = new Vector3(1, 0, i);
            Vector3 obstacle2_position = new Vector3(-1, 0, i);
            obstacles.Add(Instantiate(obstaclePrefab, obstacle1_position, new Quaternion()));
            obstacles.Add(Instantiate(obstaclePrefab, obstacle2_position, new Quaternion()));
        }
    }

    public void DestroyObstacle(Component sender, object data)
    {
        if (sender is ObstacleController)
        {
            Debug.Log("Event called");
            obstacles.Remove(sender.gameObject);
        }
    }
}
