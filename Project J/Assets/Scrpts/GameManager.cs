using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    private static bool isCreate = false;

    #region Scene
    private const string LOGIN = "0. Login";
    private const string LOBBY = "1. Lobby";
    private const string MATCHLOBBY = "2. MatchLobby";
    private const string READY = "3. LoadRoom";
    private const string INGAME = "4. InGame";
    #endregion


    #region Actions-Events
    public static event Action OnRobby = delegate { };
    public static event Action OnGameReady = delegate { };
    //public static event Action OnGameStart = delegate { };
    public static event Action InGame = delegate { };
    public static event Action AfterInGame = delegate { };
    public static event Action OnGameOver = delegate { };
    public static event Action OnGameResult = delegate { };
    public static event Action OnGameReconnect = delegate { };

    private string asyncSceneName = string.Empty;
    private IEnumerator InGameUpdateCoroutine;

    public enum GameState { Login, Lobby, MatchLobby, Ready, Start, InGame, Over, Result, Reconnect };
    private GameState gameState;
    #endregion

    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("GameManager �ν��Ͻ��� �������� �ʽ��ϴ�.");
            return null;
        }
        return instance;
    }
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        // 60������ ����
        Application.targetFrameRate = 60;
        // ������ ������� ����
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        InGameUpdateCoroutine = InGameUpdate();

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (isCreate)
        {
            DestroyImmediate(gameObject, true);
            return;
        }
        gameState = GameState.Login;
        isCreate = true;
    }
    IEnumerator InGameUpdate()
    {
        while (true)
        {
            if (gameState != GameState.InGame)
            {
                StopCoroutine(InGameUpdateCoroutine);
                yield return null;
            }
            InGame();
            AfterInGame();
            yield return new WaitForSeconds(.1f); //1�� ����
        }
    }

    private void Login()
    {
        // OnLogin();
        // ChangeScene(LOGIN);
    }
    private void MatchLobby(Action<bool> func)
    {
        if (func != null)
        {
            ChangeSceneAsync(MATCHLOBBY, func);
        }
        else
        {
            ChangeScene(MATCHLOBBY);
        }
    }

    private void GameReady()
    {
        Debug.Log("���� ���� ���� ����");
        ChangeScene(READY);
        OnGameReady();
    }

    private void GameStart()
    {
        //delegate �ʱ�ȭ
        InGame = delegate { };
        AfterInGame = delegate { };
        OnGameOver = delegate { };
        OnGameResult = delegate { };

        //OnGameStart();
        // ���Ӿ��� �ε�Ǹ� Start���� OnGameStart ȣ��
        ChangeScene(INGAME);
    }

    private void GameOver()
    {
        OnGameOver();
    }

    private void GameResult()
    {
        OnGameResult();
    }

    private void GameReconnect()
    {
        //delegate �ʱ�ȭ
        InGame = delegate { };
        AfterInGame = delegate { };
        OnGameOver = delegate { };
        OnGameResult = delegate { };

        OnGameReconnect();
        ChangeScene(INGAME);
        ChangeState(GameManager.GameState.InGame);
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public void ChangeState(GameState state, Action<bool> func = null)
    {
        gameState = state;
        switch (gameState)
        {
            case GameState.Login:
                Login();
                break;
            case GameState.MatchLobby:
                MatchLobby(func);
                break;
            case GameState.Ready:
                GameReady();
                break;
            case GameState.Start:
                GameStart();
                break;
            case GameState.Over:
                GameOver();
                break;
            case GameState.Result:
                GameResult();
                break;
            case GameState.InGame:
                // �ڷ�ƾ ����
                StartCoroutine(InGameUpdateCoroutine);
                break;
            case GameState.Reconnect:
                GameReconnect();
                break;
            default:
                Debug.Log("�˼����� ������Ʈ�Դϴ�. Ȯ�����ּ���.");
                break;
        }
    }

    public bool IsLobbyScene()
    {
        return SceneManager.GetActiveScene().name == MATCHLOBBY;
    }

    private void ChangeScene(string scene)
    {
        if (scene != LOGIN && scene != INGAME && scene != LOBBY && scene != MATCHLOBBY && scene != READY)
        {
            Debug.Log("�˼����� �� �Դϴ�.");
            return;
        }

        SceneManager.LoadScene(scene);
    }

    private void ChangeSceneAsync(string scene, Action<bool> func)
    {
        asyncSceneName = string.Empty;
        if (scene != LOGIN && scene != INGAME && scene != LOBBY && scene != MATCHLOBBY && scene != READY)
        {
            Debug.Log("�˼����� �� �Դϴ�.");
            return;
        }
        asyncSceneName = scene;

        StartCoroutine("LoadScene", func);
    }

    private IEnumerator LoadScene(Action<bool> func)
    {
        var asyncScene = SceneManager.LoadSceneAsync(asyncSceneName);
        asyncScene.allowSceneActivation = true;

        bool isCallFunc = false;
        while (asyncScene.isDone == false)
        {
            if (asyncScene.progress <= 0.9f)
            {
                func(false);
            }
            else if (isCallFunc == false)
            {
                isCallFunc = true;
                func(true);
            }
            yield return null;
        }
    }
}
