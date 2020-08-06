using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextoDBO : MonoBehaviour
{
    public TextMeshProUGUI textoDBO;

    public void CargarTextoDBO(string dbo)
    {
        textoDBO.text = dbo;
    }
}
