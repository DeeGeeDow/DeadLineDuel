using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerSkill : MonoBehaviour, Skill
{
    Queue<ObstacleController> _myWalls = new Queue<ObstacleController>();
    [Header("References")]
    [SerializeField] ObstacleController _wallPrefab;
    [SerializeField] GameEvent skillStarted;
    [SerializeField] GameEvent skillFinished;
    [Header("Skill Values")]
    [SerializeField] int _wallWidth;
    [SerializeField] float _offSet;
    PlayerController myPlayerController;

    private void Awake()
    {
        myPlayerController = GetComponentInChildren<PlayerController>();
    }

    public void onCast()
    {
        skillStarted.Raise(this, null);
        Vector3 castDirection = transform.position + new Vector3(_offSet, 0, 0) * ((myPlayerController.isPlayer1) ? 1 : -1);
        for (int i = 0; i < _wallWidth; i++)
        {
            tryInstantiate(castDirection + Vector3.forward * (i + 0.5f - _wallWidth / 2f));
        }
        skillFinished.Raise(this, true);
    }

    private ObstacleController tryInstantiate(Vector3 position)
    {
        ObstacleController newWall;
        if (_myWalls.Count > 0)
        {
            newWall = _myWalls.Dequeue();
            newWall.gameObject.SetActive(true);
            newWall.transform.position = position;
            newWall.hp = 2;
            return newWall;
        }
        newWall = Instantiate(_wallPrefab, position, Quaternion.identity);
        newWall.IsPooled = true;
        newWall.OnObstacleDestroyed += NewWall_OnObstacleDestroyed;
        newWall.hp = 2;
        return newWall;
    }

    private void NewWall_OnObstacleDestroyed(ObstacleController obstacle)
    {
        _myWalls.Enqueue(obstacle);
    }
}
