using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;
using System.Globalization;
using UnityEngine.Networking;

public class CargarImagen : MonoBehaviour
{
    string url = "http://commons.wikimedia.org/wiki/Special:FilePath/Flag_of_Ecuador.svg?width=300";
    //public Renderer thisRenderer;
    public SpriteRenderer spriteCargar;

    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CorutinaCargarImagen());
    }

    private IEnumerator CorutinaCargarImagen()
    {
        WWW wwwLoader = new WWW(url);
        yield return wwwLoader;
        //spriteCargar.sprite. = wwwLoader.texture;
        //spriteCargar.sprite = wwwLoader.

        //thisRenderer.material.mainTexture = wwwLoader.texture;
    }
   
   
}
