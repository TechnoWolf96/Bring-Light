using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSettingButton : MonoBehaviour
{
    [HideInInspector] public KeyCode currentKey;

    private readonly Array keyCodes = Enum.GetValues(typeof(KeyCode));
    private bool changing = false;
    private Text text;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
        text.text = currentKey.ToString();
    }



    public void ChangeControl()
    {
        changing = true;
        button.interactable = false;
        text.text = "";
    }


    private void Update()
    {
        if (changing && Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in keyCodes)
            {
                if (Input.GetKey(keyCode))
                {
                    currentKey = keyCode;
                    text.text = currentKey.ToString();
                    changing = false;
                    StartCoroutine(Delay());
                }
            }
            
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.2f);
        button.interactable = true;
    }



}
