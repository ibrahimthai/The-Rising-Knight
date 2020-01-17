using System.Collections;
using UnityEngine;

/* This class deploys the eagles as they move from the right side of the screen */
public class deployEagle : MonoBehaviour
{
    public GameObject eaglePrefab;
    public float respawnTime = 1.0f;
    private Vector2 screenbounds;

    void Start()
    {
        screenbounds = Camera.main.ScreenToViewportPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(EagleWave());
    }

    /* Spawns more eagles for users to kill */
    private void SpawnEagle()
    {
        GameObject a = Instantiate(eaglePrefab) as GameObject;
        a.transform.position = new Vector2(screenbounds.x * 4, Random.Range(0, screenbounds.y));
    }

    /* Creates a bunch of eagles depending on the spawn time */
    IEnumerator EagleWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnEagle();
        }
    }

}
