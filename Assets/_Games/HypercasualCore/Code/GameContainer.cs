using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContainer : MonoBehaviour
{
    public static GameContainer instance;
    
    public IGameManager gameManager;
    
    private void Awake()
    {
        instance = this;
    }
}
