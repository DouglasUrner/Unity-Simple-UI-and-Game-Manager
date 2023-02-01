using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneManager : MonoBehaviour
{
  void Awake()
  {
    #if !UNITY_EDITOR
      // Only need to load scenes manually in built game versions.
      // The editor handles loading for us. The assumption here is
      // that the UI-and-Game-Manager scene is at index 0, other
      // scenes are at higher indexes. This is kind of a hack... (XXX)
      SceneManager.LoadScene(1, LoadSceneMode.Additive);
    #endif
  }
}
