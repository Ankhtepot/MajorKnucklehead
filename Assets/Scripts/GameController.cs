using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Interface;
using UnityEngine;
using Utilities;

//Fireball Games * * * PetrZavodny.com

public class GameController : MonoBehaviour
{
#pragma warning disable 649
    
#pragma warning restore 649

    [SerializeField] private List<ConfigurationBase> GameIniConfigurations = new List<ConfigurationBase>();
    
    void Start()
    {
        initialize();
    }

    public void StartGameSession()
    {
        EventBroker.TriggerOnGameSessionStarted();
    }
    
    private void initialize()
    {
        GameIniConfigurations.ForEach(configuration => configuration.ActivateConfiguration());
    }
}
