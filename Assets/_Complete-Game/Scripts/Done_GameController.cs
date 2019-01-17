using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Done_GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float LenghtLevel;
    public float waveWait;

    private bool finished = false;
    private GameObject gameSwitcher;
    void Start()
    {
        gameSwitcher = GameObject.Find("GameSwitcher");
    }

    public void StartMiniGame()
    {
        finished = false;
        StartCoroutine(SpawnWaves());
        StartCoroutine(Timer());
    }


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (!finished)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

        }
        gameSwitcher.GetComponent<GameSwitcher>().ReturnToSpace();
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(LenghtLevel);
        finished = true;
    }

    public void GameOver()
    {
        finished = true;
        gameSwitcher.GetComponent<GameSwitcher>().ReturnToSpace();
    }
}