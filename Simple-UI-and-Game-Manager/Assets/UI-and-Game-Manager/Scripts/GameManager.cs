using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
  // Game states. Set by UI and PlayerController.
  public bool gameIdle = true;      // Waiting to start.
  public bool gameEnding = false;   // Player has "lost," cleaning up.
  public bool gameQuitting = false; // User wants to exit.

  private int score = 0;
  // Set from uiManager.gameInfo.initialHealth in StartGame().
  private int health = -1;

  // UI interface
  public GameObject uiPrefab;
  private UIManager uiManager;

  [DllImport("__Internal")]
  private static extern void WebGLQuit(string url);

  private float savedTimeScale;

  // Public interface.
  public void AddPoints(int points) { score += points; }
  public void IncreaseHealth(int amt) { health += amt; }
  public void DecreaseHealth(int amt)
  {
    health -= amt;
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
    if (!gameIdle) { StartGame(); }

    if (gameEnding) { EndGame(); }

    if (gameQuitting) { QuitGame(); }
  }

  void StartGame()
  {
    Time.timeScale = savedTimeScale;
    uiManager.DisplayScore(score);
    health = uiManager.gameInfo.initialHealth;
    uiManager.DisplayHealth(health);
  }

  // Called when the user has "lost" this round. For example when the
  // player dies -- the game will be reset and the user will be able
  // to start another round. To "quit" and stop playing the game we call
  // QuitGame().
  void EndGame()
  {
    Time.timeScale = 0;
    gameEnding = false;
    gameIdle = true;
    // Reload scene.
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    uiManager.DisplayGameOverMessage();
    score = 0;
    health = uiManager.gameInfo.initialHealth;
  }

  void QuitGame()
  {    
    #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
      var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
      Debug.Log($"{this.name}: {this.GetType()}: {methodName}()");
      if (uiManager.debug)
        Debug.Log($"{methodName}(): url = {uiManager.gameInfo.url}");
    #endif

    #if (UNITY_EDITOR)
      // Application.Quit is ignored in the editor,
      // but this will get us out.
      UnityEditor.EditorApplication.isPlaying = false;
    #elif (UNITY_WEBGL)
      // Exit from web player by redirecting to another page.
      // Application.OpenURL(uiManager.gameInfo.page); // Loops, runs afoul of popup blocker...
      // Found this: https://forum.unity.com/threads/quit-and-memory-cleanup.571210/page-2
      WebGLQuit(uiManager.gameInfo.url);
      //Application.ExternalEval($"window.open('{uiManager.gameInfo.page}', '_self')");
    #else
      // Standalone (macOS, Linux, Windows).
      Application.Quit();
    #endif
  }
}
