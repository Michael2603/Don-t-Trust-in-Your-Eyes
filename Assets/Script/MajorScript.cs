using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MajorScript : MonoBehaviour
{
    bool endedGame;

    public GameObject Map;
    public GameObject ressonance;
    public GameObject Ela;
    public GameObject Ela_UD;

    public GameObject[] thunderList;
    public GameObject portal;

    public float thunderTimer;
    public float portalTimerOpen;
    public float portalTimerExit;

    public int pointed = 0;

    public GameObject[] menus;

    void Start()
    {
        thunderTimer = Random.Range(4,8);
        endedGame = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);

        if (pointed == 2)
        {
            Victory();
        }
    }

    void FixedUpdate()
    {
        if (endedGame)
            return;

        thunderTimer -= Time.deltaTime;

        if (thunderTimer <= 0)
            SpawnThunder();

        portalTimerOpen -= Time.deltaTime;

        if (portalTimerOpen <= 0)
        {
            SpawnPortal();
        }
    }

    void SpawnThunder()
    {
        thunderTimer = Random.Range(2, 5);

        Vector3 spawnPosition = new Vector3(0,0,0);
        spawnPosition.x = Random.Range(-3.25f, 4.44f);
        
        GameObject tempThunder;
        
        switch(Random.Range(0,2))
        {
            case 0: // Sets the thunder on the upside world
                spawnPosition.y = .21f;
                tempThunder = Instantiate(thunderList[Random.Range(0,3)], spawnPosition, transform.rotation);
            break;
            case 1:
                spawnPosition.y = -.21f;
                tempThunder = Instantiate(thunderList[Random.Range(0,3)], spawnPosition, transform.rotation);
                tempThunder.transform.localScale = new Vector3(tempThunder.transform.localScale.x, -tempThunder.transform.localScale.y, 0);
            break;
        }
    }

    void SpawnPortal()
    {
        portalTimerOpen = Random.Range(5,11);

        Vector3 spawnPosition = new Vector3(0,0,0);
        spawnPosition.x = Random.Range(0.5f, 2.8f);
        spawnPosition.y = Random.Range(0.42f, 1.47f);
        
        GameObject tempPortal;
        
        switch(Random.Range(0,2))
        {
            case 0: // Sets the thunder on the upside world
                tempPortal = Instantiate(portal, spawnPosition, transform.rotation);
            break;
            case 1:
                spawnPosition.y = -spawnPosition.y;
                tempPortal = Instantiate(portal, spawnPosition, transform.rotation);
                tempPortal.transform.localScale = new Vector3(tempPortal.transform.localScale.x, -tempPortal.transform.localScale.y, 0);
            break;
        }
    }

    void Victory()
    {
        menus[0].SetActive(true);
        
        menus[1].SetActive(false);
        Ela.SetActive(false);
        Ela_UD.SetActive(false);
        Map.SetActive(false);
        ressonance.SetActive(false);

        endedGame = true;
    }

    public void Defeat()
    {
        menus[1].SetActive(true);
        
        menus[0].SetActive(false);
        Ela.SetActive(false);
        Ela_UD.SetActive(false);
        Map.SetActive(false);
        ressonance.SetActive(false);

        endedGame = true;
    }
}