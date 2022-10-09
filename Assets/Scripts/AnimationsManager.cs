using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavedButton()
    {
        SceneManager.LoadScene(SceneConstants.SavedScene);
    }

    public void CharacterButton()
    {
        SceneManager.LoadSceneAsync(SceneConstants.CharacterScene);
    }

    public void GameButton()
    {
        SceneManager.LoadScene(SceneConstants.GameScene);
    }
}
