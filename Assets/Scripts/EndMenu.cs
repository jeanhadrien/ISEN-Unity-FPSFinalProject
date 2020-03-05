using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    private GameLogic _gameLogic;
    public GameObject text;
    void Start()
    {
        _gameLogic = GameObject.FindWithTag("Logic").GetComponent<GameLogic>();
        var nb = (1f/(_gameLogic.timeSinceSpawn / _gameLogic.nbEnemiesToKill)).ToString();
        text.GetComponent<TextMeshProUGUI>().text = "Kill/s : " + nb + "\nTotal time : " +  GameLogic.ToTimeString(_gameLogic.timeSinceSpawn);
        Destroy	(_gameLogic	);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
