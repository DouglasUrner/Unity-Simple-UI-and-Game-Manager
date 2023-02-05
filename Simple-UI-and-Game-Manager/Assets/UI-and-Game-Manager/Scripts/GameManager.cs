using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
  // Game states. Set by UI and PlayerController.
  public enum GameState
  {
    gameIdle,     // Waiting to start.
    gameStart,    // Start button clicked, but GameStart not yet called.
    gameRunning,  // Actively playing.
    gameEnding,   // Player has "lost," cleaning up.
    gameQuitting  // User wants to exit.
  }

  public GameState gameState = GameState.gameIdle;

  public static bool globalDebug = true;   // Enable all debugging.

  [SerializeField]
  private int score = 0;
  // Health is set from gameInfo.initialHealth in StartGame().
  [SerializeField]
  private int health = -1;

  // Configuration file.
  public TextAsset gameInfoJSON;
  // Configuration info.
  public GameInfo gameInfo;

  // UI interface
  public GameObject uiPrefab;
  private UIManager uiManager;

  [DllImport("__Internal")]
  private static extern void WebGLQuit(string url);

  private float savedTimeScale;

  // Public interface.
  public void AddPoints(int points)
  { score += points; uiManager.DisplayScore(score); }
  public void IncreaseHealth(int amt)
  { health += amt; uiManager.DisplayHealth(health); }
  public void DecreaseHealth(int amt)
  {
    if (globalDebug) { Trace(health); }
    health -= amt;
    uiManager.DisplayHealth(health);
    if (health <= 0)
    {
      EndGame();
    }
  }
  // Start is called before the first frame.
  void Start()
  {
    savedTimeScale = Time.timeScale;
    Time.timeScale = 0;

    gameInfo = InitializeGameInfo(gameInfoJSON);

    // Instantiate the UI prefab if it is not found.
    if ((uiManager = GameObject.Find("UI").GetComponent<UIManager>()) == null)
    {
      var ui = Instantiate(uiPrefab);
      ui.name = "UI";
      uiManager = ui.GetComponent<UIManager>();
    }
    // Make sure we have an event system.
    if (FindObjectOfType<EventSystem>() == null)
    {
      var eventSystem = new GameObject("EventSystem",
        typeof(EventSystem),
        typeof(StandaloneInputModule)
      );
      DontDestroyOnLoad(eventSystem);
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (gameState == GameState.gameStart){ StartGame(); }

    if (gameState == GameState.gameEnding) { EndGame(); }

    if (gameState == GameState.gameQuitting) { QuitGame(); }
  }

  void StartGame()
  {
    if (globalDebug) { Debug.Log($"StartGame()"); }
    Time.timeScale = savedTimeScale;
    uiManager.DisplayScore(score);
    health = gameInfo.initialHealth;
    uiManager.DisplayHealth(health);
    gameState = GameManager.GameState.gameRunning;
  }

  // Called when the user has "lost" this round. For example when the
  // player dies -- the game will be reset and the user will be able
  // to start another round. To "quit" and stop playing the game we call
  // QuitGame().
  void EndGame()
  {
    Time.timeScale = 0;
    gameState = GameState.gameIdle;
    // Reload scene.
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    uiManager.DisplayGameOverMessage();
    score = 0;
    health = gameInfo.initialHealth;
  }

  void QuitGame()
  {    
    #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
      var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
      Debug.Log($"{this.name}: {this.GetType()}: {methodName}()");
      if (globalDebug)
        Debug.Log($"{methodName}(): url = {gameInfo.url}");
    #endif

    #if (UNITY_EDITOR)
      // Application.Quit is ignored in the editor,
      // but this will get us out.
      UnityEditor.EditorApplication.isPlaying = false;
    #elif (UNITY_WEBGL)
      // Exit from web player by redirecting to another page -- call goes
      // through JavaScript interface. Docs at:
      // https://docs.unity3d.com/Manual/webgl-interactingwithbrowserscripting
      WebGLQuit(gameInfo.url);
    #endif
    // General cleanup and standalone (macOS, Linux, Windows).
    Application.Quit();
  }

  static GameInfo InitializeGameInfo(TextAsset json)
  {
    GameInfo gameInfo = new GameInfo();

    gameInfo = JsonUtility.FromJson<GameInfo>(json.text);
    if (globalDebug)
    {
      Debug.Log($"GameInfo (from {json.name}):\n{json.text}");
    }

    return gameInfo;
  }

  public void Trace(int info)
  {
    Debug.Log($"{info}");
  }
}
