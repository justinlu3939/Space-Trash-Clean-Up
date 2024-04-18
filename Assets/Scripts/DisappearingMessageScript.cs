using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisappearingMessageScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI message;
    void Start()
    {
        Invoke("DisableText", 5f);
    }

    // Update is called once per frame
    void DisableText()
    {
        message.gameObject.SetActive(false);
    }
}
