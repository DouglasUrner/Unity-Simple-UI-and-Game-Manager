using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
  public bool debug = true;

  // Configuration file.
  public TextAsset gameInfoJSON;
  // Configuration info.
  public GameInfo gameInfo = new GameInfo();
  
  // UI elements.
  private GameObject leftUIPanel;
  private TextMeshProUGUI titleText;
  private TextMeshProUGUI descText;
  private Button startButton;
  private TextMeshProUGUI startButtonText;
  private Button quitButton;
  private TextMeshProUGUI quitButtonText;

  private GameObject scorePanel;
  private TextMeshProUGUI scoreText;
  private GameObject healthPanel;
  private TextMeshProUGUI healthText;

  private GameManager gameManager;

  void Start()
  {
    if (debug) Debug.Log("UIManager.Start()");
    gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    gameInfo = JsonUtility.FromJson<GameInfo>(gameInfoJSON.text);
    if (debug)
    {
      Debug.Log($"GameInfo (from {gameInfoJSON.name}):\n{gameInfoJSON.text}");
    }

    GetUIElementReferences();

    ResetUI();
  }

  public void DisplayScore(int value)
  {
    scoreText.text = $"{gameInfo.scoreLabel}: {value}";
  }

  public void DisplayHealth(int value)
  {
    healthText.text = $"{gameInfo.healthLabel}: {value}";
  }

  public void ResetUI()
  {
    leftUIPanel.SetActive(true);
    scorePanel.SetActive(false);
    healthPanel.SetActive(false);
    titleText.text = gameInfo.title;
    descText.text = gameInfo.description;
    startButtonText.text = gameInfo.startButtonLabel;
    quitButtonText.text = gameInfo.quitButtonLabel;
    quitButton.gameObject.SetActive(gameInfo.displayQuitButton);
  }

  public void StartButtonClicked()
  {
    leftUIPanel.gameObject.SetActive(false);
    quitButton.gameObject.SetActive(gameInfo.displayQuitButton);
    scorePanel.SetActive(gameInfo.displayScore);
    healthPanel.SetActive(gameInfo.displayHealth);
    gameManager.gameIdle = false;
  }

  public void QuitButtonClicked()
  {
    Debug.Log("QuitButtonClicked(): time to figure out what to do...");
    // XXX - at a minimum set a flag in the game manager.
  }

  public void DisplayGameOverMessage()
  {
    gameInfo.description = gameInfo.gameOverMessage;
    ResetUI();
  }

  // GetUIElementReferences: set references to the UI elements.
  //
  // The UI elements are found by name, if you change
  // the names of any of the elements must match here
  // and in the Inspector.
  void GetUIElementReferences()
  {
    // Left UI Panel
    leftUIPanel = FindAndCheck("Left UI Panel");

    var title = FindAndCheck("Title");
    titleText = GetTextAndCheck(title);

    var desc = FindAndCheck("Description");
    descText = GetTextAndCheck(desc);

    var sbgo = FindAndCheck("Start Button");
    startButton = sbgo.GetComponent<Button>();
    startButtonText = GetTextAndCheck(startButton.gameObject);

    var qbgo = FindAndCheck("Quit Button");
    quitButton = qbgo.GetComponent<Button>();
    quitButtonText = GetTextAndCheck(quitButton.gameObject);

    // Score Panel
    scorePanel = FindAndCheck("Score Panel");
    scoreText = GetTextAndCheck(scorePanel);

    // Health Panel
    healthPanel = FindAndCheck("Health Panel");
    healthText = GetTextAndCheck(healthPanel);
  }

  // FindAndCheck: call GameObject.Find and report errors.
  GameObject FindAndCheck(string name)
  {
    var advice = $"Make sure that the '{name}' UI element is in the Hierarchy and that its spelling and capitalization matches '{name}' exactly.";

    var go = GameObject.Find(name);
    if (go == null)
    {
      Debug.Log(
        $"FindAndCheck(): unable to find the UI element '{name}'.\n{advice}"
      );
      // XXX - throw an exception?
    }

    return go;
  }

  // GetTextAndCheck: call GetComponentInChildren and report errors.
  TextMeshProUGUI GetTextAndCheck(GameObject go)
  {
    var advice = $"Make sure that the '{go.name}' UI element has a TextMeshPro Text component.";

    var text = go.GetComponentInChildren<TextMeshProUGUI>();
    if (text == null)
    {
      Debug.Log(
        $"GetTextAndCheck(): the UI element '{go.name}' does not contain a TextMeshPro Text component.\n{advice}"
      );
      // XXX - throw an exception?
    }

    return text;    
  }
}
