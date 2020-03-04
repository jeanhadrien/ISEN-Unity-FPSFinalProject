using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static Text UpL, UpR, Center;
    void Start()
    {
        // get canvas children
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject current = transform.GetChild(i).gameObject;
            if (current.name == "UpL") UpL = current.GetComponent<Text>();
            if (current.name == "UpR") UpR = current.GetComponent<Text>();
            if (current.name == "Center") Center = current.GetComponent<Text>();
        }
        
        SetTextCenter("");
        SetTextUpR("");
        SetTextUpL("");
    }

    static public void SetTextUpL(string txt) { UpL.text = txt; }

    static public void SetTextUpR(string txt) { UpR.text = txt; }

    static public void SetTextCenter(string txt) { Center.text = txt; }

}
