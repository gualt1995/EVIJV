using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class SgameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject objective;
    public Transform SpawnPosition;
    public Vector3 spawnValues;
    public float sacling;
    public int ObstacleCount;
    private bool finished = false;
    private GameObject gameSwitcher;
    void Start()
    {
        gameSwitcher = GameObject.Find("GameSwitcher");
    }

    public void StartMiniGame()
    {
        SpawnObstacles();
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x) + SpawnPosition.position.x, SpawnPosition.position.y, Random.Range(-spawnValues.z, spawnValues.z) + SpawnPosition.position.z);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(objective, spawnPosition, spawnRotation);

    }

    private void SpawnObstacles()
    {
        for (int i = 0; i < ObstacleCount; i++)
        {
            GameObject hazard = hazards[Random.Range(0, hazards.Length)];
            Vector3 spawnPosition = new Vector3( SpawnPosition.position.x + Random.Range(-spawnValues.x, spawnValues.x), SpawnPosition.position.y,  SpawnPosition.position.z + Random.Range(-spawnValues.z, spawnValues.z));
            Quaternion spawnRotation = Quaternion.identity;
            GameObject tmp = Instantiate(hazard, spawnPosition, spawnRotation);
            float scl = Random.Range(5, sacling);
            tmp.transform.localScale = new Vector3(scl, scl, scl); // change its local scale in x y z format

        }
    }
}