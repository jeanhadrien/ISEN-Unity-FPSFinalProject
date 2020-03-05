using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameLogic : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject spawner;
    public GameObject enemyPrefab;
    [NonSerialized] public int nbEnemiesToKill = 10;
    private float spawnRate = 1f;
    private int nbEnemiesKilled;
    [NonSerialized] public float timeSinceSpawn;
    private bool canStartSpawnRoutine = false;
    private bool started;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        if (started) timeSinceSpawn += Time.fixedDeltaTime;
    }

    private void Update()
    {
        if (spawner == null)
        {
            spawner = GameObject.Find("spawnPlane");
            
            
        }
        else
        {
            if(!started) canStartSpawnRoutine = true;
        }
        if (canStartSpawnRoutine)
        {
            StartCoroutine(Spawn(spawnRate));
            canStartSpawnRoutine = false;
            started = true;
        }

        if (started)
        {
            if (nbEnemiesKilled >= nbEnemiesToKill)
            {
                SceneManager.LoadScene("End");
            }
            MyUiManager.SetTextUpR(ToTimeString(timeSinceSpawn));
            MyUiManager.SetTextCenter(nbEnemiesKilled.ToString());
        }

    }

    IEnumerator Spawn(float rate)
    {
        while (true)
        {
            yield return new WaitForSeconds(rate);
            Instantiate(enemyPrefab, randomPointOnPlane(spawner), Quaternion.identity);
        }

    }
    
    /// <summary>
    /// returns random point in gameobject
    /// </summary>
    /// <param name="plane"></param>
    /// <returns></returns>
    private Vector3 randomPointOnPlane(GameObject plane) {
        var position = plane.transform.position;
        var localScale = plane.transform.localScale;

        var randomX = Random.Range(position.x - localScale.x / 2, position.x + localScale.x / 2);
        var randomY = Random.Range(position.y - localScale.y / 2, position.y + localScale.y / 2);
        var randomZ = Random.Range(position.z - localScale.z / 2, position.z + localScale.z / 2);
        var y = new Vector3(randomX, randomY, randomZ);
        return y;
    }
    /// <summary>
    /// This method returns a time string in format MM:ss:mm from a time float
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static string ToTimeString(float t)
        
    {
        int minutes = 0, seconds = 0; 
        float milliseconds = 0;
        
        while (t > 60)
        {
            minutes++;
            t -= 60;
        }

        while (t > 1)
        {
            seconds++;
            t -= 1;
        }

        milliseconds = t * 1000;

        return $"{minutes:00}" + ":" + $"{seconds:00}" + ":" + $"{milliseconds:000}";
    }
    public void AddKillCounter()
    {
        nbEnemiesKilled++;
    }

    public void	SetNbKill(int nbkill)
    {
        nbEnemiesToKill = nbkill;
    }

    public void	SetRate(float rate)
    {
        spawnRate = rate;
    }
}
