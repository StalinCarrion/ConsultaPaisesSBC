using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Globalization;
using UnityEngine.Networking;
using TMPro;


public class ImageLoader : MonoBehaviour
{
    public string url = "http://commons.wikimedia.org/wiki/Special:FilePath/Escudo_nacional_del_Per\u00FA.svg?width=300";
    //public Renderer thisRenderer;
    public RawImage picture;

    // automatically called when game started
    void Start()
    {
        StartCoroutine(LoadFromLikeCoroutine()); // execute the section independently
    }


    // this section will be run independently
    private IEnumerator LoadFromLikeCoroutine()
    {
        Debug.Log("Loading ....");
        WWW wwwLoader = new WWW(url);   // create WWW object pointing to the url
        yield return wwwLoader;         // start loading whatever in that url ( delay happens here )
        picture.texture = wwwLoader.texture;
        Debug.Log("Loaded");
    }
}