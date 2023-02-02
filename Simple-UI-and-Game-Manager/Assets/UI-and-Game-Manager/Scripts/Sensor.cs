using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
  // What to do on a collision.
  public enum CollisionAction
  {
    endGame,
    addPoints,
    subPoints,
    addHealth,
    subHealth
  }

  public enum CollisionEvent
  {
    onEnter,
    onExit
  }
  public CollisionAction action = CollisionAction.endGame;
  public int amount = 1;
  public CollisionEvent collisionEvent = CollisionEvent.onEnter;
  private GameManager gameManager;

  // Start is called before the first frame update
  void Start()
  {
    gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); 
  }

  void OnTriggerEnter()
  {
    if (collisionEvent == CollisionEvent.onEnter)
    {
      TriggerAction();
    }
    else
    {
      return;
    }
  }

  void OnTriggerExit()
  {
    if (collisionEvent == CollisionEvent.onExit)
    {
      TriggerAction();
    }
    else
    {
      return;
    }
  }

  void TriggerAction()
  {
    switch (action)
    {
      case CollisionAction.endGame:
        gameManager.gameEnding = true;
        break;
      case CollisionAction.addPoints:
        gameManager.AddPoints(amount);
        break;
      case CollisionAction.subPoints:
        gameManager.AddPoints(-amount);
        break;
      case CollisionAction.addHealth:
        gameManager.IncreaseHealth(amount);
        break;
      case CollisionAction.subHealth:
        gameManager.DecreaseHealth(amount);
        break;
      default:
        Debug.Log($"unhandled CollisionAction: {action}");
        break;
    }
  }
}
