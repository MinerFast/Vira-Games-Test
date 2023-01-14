using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject diamond;
    private Vector3 lastPosition;
    private float size;
    [SerializeField] private int chanceSpawnDiamond = 5;
    [SerializeField] private Transform firstDotSpawn;

    #region MonoBehaviour
    private void OnValidate()
    {
        if (chanceSpawnDiamond < 0)
        {
            chanceSpawnDiamond = 1;
        }
    }

    void Start()
    {
        lastPosition = firstDotSpawn.position;
        size = platform.transform.localScale.x;

        for (int i = 0; i < 20; i++)
        {
            SpawnPlatform();
        }
    }
    #endregion


    #region SpawnObjects
    public void StartSpawningPlatforms()
    {
        InvokeRepeating("SpawnPlatform", 0.13f, 0.26f);
    }
    void SpawnPlatform()
    {
        int rand = Random.Range(0, 10); 
        if (rand < 5)
        {
            SpawnX();

        }
        else if (rand >= 5)
        {
            SpawnZ();
        }
    }

    void SpawnX()
    {
        Vector3 pos = lastPosition;
        pos.x += size;
        Instantiate(platform, pos, Quaternion.identity);
        lastPosition = pos;

        SpawnDiamond(pos);
    }

    void SpawnZ()
    {
        Vector3 pos = lastPosition;
        pos.z += size;
        Instantiate(platform, pos, Quaternion.identity);
        lastPosition = pos;

        SpawnDiamond(pos);
    }
    void SpawnDiamond(Vector3 pos)
    {
        int rand = Random.Range(0, chanceSpawnDiamond);
        if (rand < 1)
        {
            Instantiate(diamond, new Vector3(pos.x, pos.y + 3.0f, pos.z), diamond.transform.rotation);
        }
    }
    #endregion
}
