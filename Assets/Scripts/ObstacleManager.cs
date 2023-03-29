using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public List<float> probability = new List<float>();
    public GameObject obstaclePrefab;
    public List<GameObject> obstacles = new List<GameObject>();

    private void Start()
    {
        for(int i=0; i<3; i++)
        {
            spawnObstacle();
        }
    }

    void spawnObstacle()
    {
        Vector3 obstacle_position = new Vector3();
        obstacle_position.x = Random.Range(Boundaries.HACKER_BOUND[2]+1, -Boundaries.HACKER_BOUND[2]-1);
        obstacle_position.z = Random.Range(Boundaries.HACKER_BOUND[0], Boundaries.HACKER_BOUND[1]);
        obstacles.Add(Instantiate(obstaclePrefab, obstacle_position, new Quaternion()));
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
