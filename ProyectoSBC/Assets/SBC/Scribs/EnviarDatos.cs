using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;
using System.Globalization;
using UnityEngine.Networking;
using TMPro;

public class EnviarDatos : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panelAbout;
    public InputField inputPais;
    //public InputField inputDbo;
    public TextMeshProUGUI textoDBO;
    public TextMeshProUGUI sujeto;
    public TextMeshProUGUI predicado;
    public TextMeshProUGUI objeto;
    public TextMeshProUGUI nombre;
    public Text consulta;
    public RawImage picture;

    public void Ejecutar()
    {
        StartCoroutine(CorutineConsultaLabel());
        StartCoroutine(CorutineEjecutar());
        StartCoroutine(CorutineLoadImagen());
              
        panel2.SetActive(true);
        panel1.SetActive(false);
        sujeto.text = inputPais.text;
        predicado.text = "dbo:"+textoDBO.text;
        ShowInfo(sujeto.text,predicado.text);


    }
    IEnumerator CorutineEjecutar()
    {
        WWW www = new WWW("https://dbpedia.org/sparql?default-graph-uri=http%3A%2F%2Fdbpedia.org&quer" +
            "y=%0D%0Aselect+%3Fresultado%0D%0AWHERE+%7B%0D%0A%3Chttp%3A%2F%2Fdbpedia.org" +
            "%2Fresource%2F"+inputPais.text+"%3E+dbo%3A"+textoDBO.text+"+%3Fresultado.%0D%0A%7D%0D%0A&format=a" +
            "pplication%2Fsparql-results%2Bjson&CXML_redir_for_subjs=121&CXML_redir_for_" +
            "hrefs=&timeout=30000&debug=on&run=+Run+Query+");
        yield return www;
        //print(www.text);
        JsonData data = JsonMapper.ToObject(www.text);
        Nombres nom = new Nombres();
        nom.Sujeto = data["results"]["bindings"][0]["resultado"]["value"].ToString();
        string strSujeto;
        strSujeto = nom.Sujeto;
        PlayerPrefs.SetString("objeto", strSujeto);
        objeto.text = PlayerPrefs.GetString("objeto");
        //Debug.Log("strUrl: " + strSujeto);
    }
    IEnumerator CorutineConsultaLabel()
    {
        WWW www = new WWW("https://dbpedia.org/sparql?default-graph-uri=http%3A%2F%2Fdbpedia.org&query=SELECT" +
            "+str%28%3Fnombre%29+AS+%3Fnombre%0D%0AWHERE+%7B%0D%0A%3Chttp%3A%2F%2Fdbpedia.org%2Fresourc" +
            "e%2F"+ inputPais.text + "%3E+dbo%3A"+ textoDBO.text + "+%3Fresultado+.%0D%0A%3Fresultado+foaf%3Aname+%3Fnombre%0D%0A%7D+LIM" +
            "IT+1&format=application%2Fsparql-results%2Bjson&CXML_redir_for_subjs=121&CXML_redir_for_hrefs=&" +
            "timeout=30000&debug=on&run=+Run+Query+");
        yield return www;
        //print(www.text);
        JsonData data = JsonMapper.ToObject(www.text);
        Nombres nom = new Nombres();
        nom.Objeto = data["results"]["bindings"][0]["nombre"]["value"].ToString();
        string strNombre;
        strNombre = nom.Objeto;
        PlayerPrefs.SetString("nombre", strNombre);
        nombre.text = PlayerPrefs.GetString("nombre");
        //Debug.Log("strUrl: " + strNombre);
    }
    IEnumerator CorutineLoadImagen()
    {
        WWW www = new WWW("https://dbpedia.org/sparql?default-graph-uri=http%3A%2F%2Fdbpedia.org&query=" +
            "SELECT+%3Fresultado%0D%0AWHERE+%7B%0D%0A%3Chttp%3A%2F%2Fdbpedia.org%2Fresource" +
            "%2F"+ inputPais.text + "%3E+dbo%3Athumbnail+%3Fresultado+.%0D%0A%7D&format=application%2" +
            "Fsparql-results%2Bjson&CXML_redir_for_subjs=121&CXML_redir_for_hrefs=&time" +
            "out=30000&debug=on&log_debug_info=on&run=+Run+Query+");
        yield return www;
        print(www.text);
        JsonData data = JsonMapper.ToObject(www.text);
        Nombres nom = new Nombres();
        nom.Predicado = data["results"]["bindings"][0]["resultado"]["value"].ToString();
        string strImagen;
        strImagen = nom.Predicado;
        
        StartCoroutine(LoadFromLikeCoroutine(strImagen));
        Debug.Log("strUrl: " + strImagen);
    }

    private IEnumerator LoadFromLikeCoroutine(string url)
    {
        Debug.Log("Loading ....");
        WWW wwwLoader = new WWW(url);   // create WWW object pointing to the url
        yield return wwwLoader;         // start loading whatever in that url ( delay happens here )
        picture.texture = wwwLoader.texture;
        Debug.Log("Loaded");
    }
    public void ShowInfo(string pal1, string pal2)
    {
        consulta.text = "SELECT ?resultado \n" +
            "\nWHERE{ \n<http://dbpedia.org/resource/"+pal1+"> "+pal2+" ?resultado . \n}";
    }

    public void BotonEnlace()
    {
        Application.OpenURL(PlayerPrefs.GetString("objeto"));
    }
    public void BotonPaginas(string link)
    {
        Application.OpenURL(link);
    }
    public void BotonAbout()
    {
        panel2.SetActive(false);
        panel1.SetActive(false);
        panelAbout.SetActive(true);
    }
    public void InicioSBC()
    {
        panel2.SetActive(false);
        panel1.SetActive(true);
        panelAbout.SetActive(false);
    }

}
