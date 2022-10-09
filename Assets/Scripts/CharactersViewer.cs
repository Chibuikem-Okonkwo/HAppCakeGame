using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;
using System;
//using AngleSharp;
//using AngleSharp.Dom;
//using AngleSharp.Html.Parser;
//using AngleSharp.Html.Dom;
using System.Threading.Tasks;

public class CharactersViewer : MonoBehaviour
{

    private List<Texture2D> characterList = new List<Texture2D>();
    public Transform content;
    public Transform characterBox;
    public Transform rawImage;

    private void Awake()
    {
        characterBox.gameObject.SetActive(false);
    }

    private void Start()
    {
        DownloadMixamoShowcase();
    }

    private void PrintThumbnails()
    {
        // Clear Previous Thumbnails
        foreach (Transform child in content)
        {
            if (child == characterBox) continue;
            Destroy(child.gameObject);
        }

        // Create Thumbnails
        float totalWidth = characterList.Count * 108;
        for (int i = 0; i < characterList.Count; i++)
        {
            Transform contentTransform = Instantiate(characterBox, content);
            Transform characterTransform = Instantiate(rawImage, characterBox);
            contentTransform.gameObject.SetActive(true);
            characterTransform.gameObject.SetActive(true);

         
            characterTransform.GetComponent<RawImage>().texture = characterList[i];
        }
    }

    private void DownloadMixamoShowcase()
    {
       // var config = Configuration.Default.WithDefaultLoader();
        //using var context = BrowsingContext.New(config);
        string url = "https://www.mixamo.com/#/?page=1&type=Character"; // Character URL

     /*   using var doc = await context.OpenAsync("https://www.mixamo.com/#/?page=1&type=Character");

        var pars = doc.QuerySelectorAll("product-image");

        List<String> img = new List<String>();
        foreach(var par in pars)
        {
            img.Add(par.QuerySelectorAll<IHtmlImageElement>("img").Attr("src").ToString());
        }

        print(img); */


       Debug.Log("Downloading Mixamo Character Showcase...");
        Get(url, (string error) => {
            Debug.Log("Could not contact Mixamo Character Showcase");
            Debug.Log("Error: " + error);
        }, (string htmlCode) => {
            Debug.Log("Mixamo Character Showcase downloaded");
            // Download images
            string textToFind;
            int cycleProtection = 0;
            while (htmlCode.IndexOf("<div class=\"product-image> <img") != -1 && cycleProtection < 100)
            {
                cycleProtection++;
                textToFind = "<div class=\"product-image> <img";
                htmlCode = htmlCode.Substring(htmlCode.IndexOf(textToFind) + textToFind.Length);
                textToFind = "src=\"";
                htmlCode = htmlCode.Substring(htmlCode.IndexOf(textToFind) + textToFind.Length);
                string imageUrl = htmlCode.Substring(0, htmlCode.IndexOf("\""));

                GetTexture(imageUrl, (string error) => {
                    Debug.Log("Failed to download thumbnail");
                    Debug.Log("Error: " + error);
                }, (Texture2D texture) => {
                    characterList.Add(texture);
                    PrintThumbnails();
                    Debug.Log("Character showcase amount: " + characterList.Count);
                });
            }
        });
    }

   public void Get(string url, Action<string> onError, Action<string> onSuccess)
    {
        WebRequests.Get(url, onError, onSuccess);
    }

    public void GetTexture(string url, Action<string> onError, Action<Texture2D> onSuccess)
    {
        WebRequests.GetTexture(url, onError, onSuccess);
    }

    public void AnimationsButton()
    {
        SceneManager.LoadScene(SceneConstants.AnimationsScene);
    }
    
    public void SavedButton()
    {
        SceneManager.LoadScene(SceneConstants.SavedScene);
    }

    public void CharacterButton()
    {
        SceneManager.LoadSceneAsync(SceneConstants.CharacterScene);
    }
}
