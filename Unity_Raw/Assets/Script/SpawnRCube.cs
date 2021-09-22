using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRCube : MonoBehaviour
{
    public GameObject cube;
    public Transform SpawnPointRCube;
    public float maxSpawnPointY;

    bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && !gameStarted)
        {

            StartCoroutine("SpawnCube");
            gameStarted = true;
        }
    }

    IEnumerator SpawnCube()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);
            Spawnrcube();

        }
    }

    public void Spawnrcube()
    {
        float randomSpawnY = Random.Range(1, maxSpawnPointY);
        Vector3 cubeSpawnPos = SpawnPointRCube.position;
        cubeSpawnPos.y = randomSpawnY;

        Instantiate(cube, cubeSpawnPos, Quaternion.identity);

    }
}
