using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }
    //Одиночка (Singleton, Синглтон) - порождающий паттерн, который гарантирует, что для определенного класса будет создан 
    //только один объект, а также предоставит к этому объекту точку доступа.
    #endregion

    public Dictionary<GameObject, Health> healthContainer;
    public Dictionary<GameObject, Coin> coinContainer;
    public Dictionary<GameObject, BuffReciever> buffRecieverContainer;

    private void Awake()
    {
        Instance = this;
        healthContainer = new Dictionary<GameObject, Health>();
        coinContainer = new Dictionary<GameObject, Coin>();
        buffRecieverContainer = new Dictionary<GameObject, BuffReciever>();
    }

    private void Start()
    {

        #region LoadAllObjs Test
        /*
        var healthObjects = FindObjectsOfType<Health>();
        foreach (var health in healthObjects)
        {
            healthContainer.Add(health.gameObject, health);
        }
        */
        #endregion
    }
}
