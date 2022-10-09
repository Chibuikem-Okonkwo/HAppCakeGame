using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavedManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimationsButton()
    {
        SceneManager.LoadScene(SceneConstants.AnimationsScene);
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
