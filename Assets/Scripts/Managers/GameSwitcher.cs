using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameSwitcher : MonoBehaviour
{

    public float pMiniGame = 0.01f;
    private GameObject Mcam;
    private GameObject Scam;
    private GameObject Ecam;
    private GameObject Acam;

    private CameraScroll cameraScroll;

    private GameObject PlayerAsteroids;
    private Done_GameController Adgc;

    private GameObject PlayerSearch;
    private SgameController Sdgc;


    private GameObject PlayerAnomaly;

    private GameObject BannerText;


    void Start()
    {
        BannerText = GameObject.Find("TextWarining");

        Mcam = GameObject.Find("MapCamera");
        Scam = GameObject.Find("ShipCamera");
        Ecam = GameObject.Find("SearchCamera");
        Acam = GameObject.Find("AnomalyCamera");

        cameraScroll = Mcam.GetComponent<CameraScroll>();
        PlayerAsteroids = GameObject.Find("PlayerAsteroids");
        PlayerSearch = GameObject.Find("PlayerSearch");

        Adgc = GameObject.Find("AGameController").GetComponent<Done_GameController>();
        Sdgc = GameObject.Find("SgameController").GetComponent<SgameController>();


        ReturnToSpace();
        Search();
    }

    // Start is called before the first frame update
    public void TestGameSwitch()
    {
        if(Random.Range(0,1)< pMiniGame)
        {
            //TODO add other games
            int choiceGame = Random.Range(0, 1);
            if(choiceGame == 0)
            {
                BannerText.GetComponent<TextWarningBehaviour>().SetText(" WARNING : ASTEROID BELT INCOMING", Color.red);
                StartCoroutine(WaitOneSecond(0));
            }
        }
    }

    public void ReturnToSpace()
    {
        PlayerAsteroids.GetComponent<PlayerAsteroids>().enabled = false;
        PlayerSearch.GetComponent<PlayerSearch>().enabled = false;
        

        Scam.GetComponent<Camera>().enabled = false;
        Scam.GetComponent<AudioListener>().enabled = false;
        Ecam.GetComponent<Camera>().enabled = false;
        Ecam.GetComponent<AudioListener>().enabled = false;
        Acam.GetComponent<Camera>().enabled = false;
        Acam.GetComponent<AudioListener>().enabled = false;

        cameraScroll.enabled = true;
        Mcam.GetComponent<Camera>().enabled = true;
        Mcam.GetComponent<AudioListener>().enabled = true;
        //TODO disable all other cams
    }

    private void Asteroids()
    {
        Adgc.StartMiniGame();
        Mcam.GetComponent<Camera>().enabled = false;
        Mcam.GetComponent<AudioListener>().enabled = false;
        cameraScroll.enabled = false;

        Scam.GetComponent<Camera>().enabled = true;
        Scam.GetComponent<AudioListener>().enabled = true;
        PlayerAsteroids.GetComponent<PlayerAsteroids>().enabled = true;
    }

    private void Search()
    {
        Sdgc.StartMiniGame();
        Mcam.GetComponent<Camera>().enabled = false;
        Mcam.GetComponent<AudioListener>().enabled = false;
        cameraScroll.enabled = false;

        Ecam.GetComponent<Camera>().enabled = true;
        Ecam.GetComponent<AudioListener>().enabled = true;
        PlayerSearch.GetComponent<PlayerSearch>().enabled = true;
    }
    IEnumerator WaitOneSecond(int game)
    {
        if(game == 0)
        {
            yield return new WaitForSeconds(1);
        }
        Asteroids();
    }
}
