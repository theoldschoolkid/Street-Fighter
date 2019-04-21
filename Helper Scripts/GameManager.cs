using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class GameManager : MonoBehaviour {

    public static GameManager instance;

    [SerializeField]
     GameObject enemyPrefab;

    [SerializeField]
    GameObject[] spawnPoints;
    int spawnTimer = 10;

    const int  spawnEnemyCount = 4;
    int Level = 2;
    int enemyCount = 1;
    bool levelSet = false;
    int i = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start() {
        SpawnEnemy(spawnPoints[0]); // spawn one enemy at spawn point [0] at start of the game
    }

    // RefreshLevel is called everytime an enemy is killed
    public void RefreshLevel()
    {
        enemyCount--;     
        Spawn();

        if (enemyCount == 0) 
        {           
            levelSet = false;
            Spawn();
        }
    }


   void Spawn()
    {
        if (levelSet == false)
        {
            if (Level == 10)
            {
                HUD.HUDinstance.Victory();
                Invoke("MainMenu", 7f);
                return;
            }

            levelSet = true;
            HUD.HUDinstance.DisplayLevel(Level);
            for (i = 0; i < Level; i++)  //After i = 1, i will be increased to 2 then it checks with level and gets out of loop, by the time i gets out of loop its value will be 2
            {
                if(i == 3)
                {
                    enemyCount = Level;
                    Invoke("LateSpawn", spawnTimer++);  // There are only 3 points to spawn stored in spawnpoints, if it exceeds 3, it will be reused and enemies will be spawned 10 seconds later.
                    return;
                }
                SpawnEnemy(spawnPoints[i]);
            }
            Level++;
           
            enemyCount = i ;
        }
    }

     void SpawnEnemy(GameObject spawnLocation) {
        Instantiate(enemyPrefab, spawnLocation.transform.position, Quaternion.identity); 
    }

    void LateSpawn()
    {
        int k = 0;
        int j = Level - i;

        for (k = 0; k < j ; k++)  
        {
            if (k == 3)
            {
                i = i + k;
                enemyCount = Level;
                Invoke("LateSpawn2", spawnTimer++);  // There are only 3 points to spawn stored in spawnpoints, if it exceeds 3, it will be reused and enemies will be spawned 10 seconds later.
                return;
            }

            SpawnEnemy(spawnPoints[k]);
        }
        Level++;
    }

    void LateSpawn2()
    {
        int k = 0;
        int j = Level - i;

        for (k = 0; k < j; k++)
        {
            SpawnEnemy(spawnPoints[k]);
        }
        Level++;
    }


    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }


} // class


