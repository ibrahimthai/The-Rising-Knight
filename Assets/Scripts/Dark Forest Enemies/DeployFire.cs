﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployFire : MonoBehaviour
{
    public GameObject firePrefab;
    public float respawnTime = 3.0f;
    private Vector2 screenbounds;

    void Start()
    {
        screenbounds = Camera.main.ScreenToViewportPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(FireWave());
    }

    /* Spawns more eagles for users to kill */
    private void SpawnFire()
    {
        GameObject a = Instantiate(firePrefab) as GameObject;
        //a.transform.position = new Vector2(screenbounds.x * 4, Random.Range(0, 0));
    }

    /* Creates a bunch of frogs depending on the spawn time */
    IEnumerator FireWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnFire();
        }
    }
}
