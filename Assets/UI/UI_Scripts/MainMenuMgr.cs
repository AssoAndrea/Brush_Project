using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMgr : MonoBehaviour
{
    public void PlayButton() => SceneManager.LoadScene(1);
}
