using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int hitsToKill;
    private int _hitsTaken = 0;
    private GameLogic _gameLogic;
    void Start()
    {
        _gameLogic = GameObject.FindGameObjectsWithTag("Logic")[0].GetComponent<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_hitsTaken >= hitsToKill)
        {
            _gameLogic.AddKillCounter();
            Destroy(transform.parent.gameObject);
        }
    }

    public void Hit()
    {
        _hitsTaken++;
    }
}
