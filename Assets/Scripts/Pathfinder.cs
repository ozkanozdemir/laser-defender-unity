using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private WaveConfigSO _waveConfig;
    private List<Transform> _waypoints;
    private int _waypointIndex = 0;
    private EnemySpawner _enemySpawner;

    private void Awake()
    {
        _enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _waveConfig = _enemySpawner.GetCurrentWave();
        _waypoints = _waveConfig.GetWaypoints();
        transform.position = _waypoints[_waypointIndex].position; 
    }

    // Update is called once per frame
    private void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (_waypointIndex < _waypoints.Count)
        {
            Vector3 targetPosition = _waypoints[_waypointIndex].position;
            var delta = _waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            
            if (transform.position == targetPosition)
            {
                _waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
