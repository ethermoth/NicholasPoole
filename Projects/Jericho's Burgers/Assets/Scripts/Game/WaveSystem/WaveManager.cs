using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pathfinding))]
public class WaveManager : MonoBehaviour
{

    [System.Serializable]
    public class Wave
    {
        [Header("Patron Settings")]
        public GameObject patronPrefab;
        public float numPatronsToSpawn;
        public float minPatronWaitTime;
        public float maxPatronWaitTime;
        public float patronWaitTime;
        public float patronWaitTimeLeft;
        public Action onPatronSpawned;

        [Header("Raider Settings")]
        public GameObject raiderPrefab;
        public float numRaidersToSpawn;
        public float minRaiderWaitTime;
        public float maxRaiderWaitTime;
        public float raiderWaitTime;
        public float raiderWaitTimeLeft;
        public Action onRaiderSpawned;
    }
    public List<Wave> waves;
    public Wave activeWave;

    // Use this for initialization
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        // Timers
        activeWave.patronWaitTimeLeft -= Time.deltaTime;
        activeWave.raiderWaitTimeLeft -= Time.deltaTime;

        if (activeWave.patronWaitTimeLeft <= 0f)
        {
            GameObject go = AISpawner.Spawn(activeWave.patronPrefab, AISpawner.SpawnType.Patron);
            // Setup pathfinding
            // Do other stuff
            activeWave.onPatronSpawned();
        }

        if (activeWave.raiderWaitTimeLeft <= 0f)
        {
            GameObject go = AISpawner.Spawn(activeWave.raiderPrefab, AISpawner.SpawnType.Raider);
            // Setup pathfinding
            // Do other stuff
            activeWave.onRaiderSpawned();
        }
    }

    void ResetTimer(Wave _w)
    {
        _w.patronWaitTime = UnityEngine.Random.Range(_w.minPatronWaitTime, _w.maxPatronWaitTime);
        _w.raiderWaitTime = UnityEngine.Random.Range(_w.minRaiderWaitTime, _w.maxRaiderWaitTime);

        _w.patronWaitTimeLeft = _w.patronWaitTime;
        _w.raiderWaitTimeLeft = _w.raiderWaitTime;
    }

    void ResetTimers()
    {
        foreach (Wave w in waves)
        {
            ResetTimer(w);
        }
    }

    void ResetActiveTimer()
    {
        ResetTimer(activeWave);
    }

    void GoToNextWave()
    {
        if (waves.IndexOf(activeWave) != waves.Count - 1)
        {
            activeWave = waves[waves.IndexOf(activeWave) + 1];
        }
    }
}