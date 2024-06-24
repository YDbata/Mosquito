using System;
using System.Collections;
using Mosquito.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum GameState
{
    None,
    LoadResources,
    WaitUntilResourcesLoaded,
    Home,
    StageLoaded,
    Stage,
}

public class GameManager : SingletonMonoBase<GameManager>
{
    [SerializeField] public bool isTesting { get; private set; }

    public GameState state
    {
        get => _state;
        set
        {
            if (value == _state)
                return;

            _state = value;
        }
    }

    [Header("Current State")] [SerializeField]
    private GameState _state;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        Workflow();
    }

    private void Workflow(string SceneName="")
    {
        switch (_state)
        {
            case GameState.None:
                break;
            case GameState.LoadResources:
                _state++;
                break;
            case GameState.WaitUntilResourcesLoaded:
                SceneManager.LoadScene("Home");
                _state++;
                break;
            case GameState.Home:
                break;
            case GameState.StageLoaded:
                SceneManager.LoadScene(SceneName);
                // 페이드 효과
                
                _state++;
                break;
            case GameState.Stage:
                break;
        }
    }
}
