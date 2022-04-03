using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ControlSettingButton : MonoBehaviour
{
    private enum KeyCodeFromControl 
    {
        Consumable1,
        Consumable2,
        Consumable3,
        Consumable4,
        Consumable5,
        RangedAttack,
        CloseAttack,
        ChangeArrows,
        OpenInventory,
        TakeItem,
    }


    [SerializeField] private KeyCodeFromControl keyCodeFromControlForChanging;
    private readonly Array keyCodes = Enum.GetValues(typeof(KeyCode));
    private bool changing = false;
    private Text text;
    private Button button;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
        
    }


    


    public void ChangeControl()
    {
        changing = true;
        button.interactable = false;
        text.text = "";
    }


    private void Update()
    {
        if (!changing) text.text = KeyToText(GetKeyCodeControlFunction(keyCodeFromControlForChanging));
        if (changing && Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in keyCodes)
            {
                if (Input.GetKey(keyCode))
                {
                    text.text = KeyToText(keyCode);
                    CnangeControlFunction(keyCodeFromControlForChanging, keyCode);
                    changing = false;
                    GameSettings.singleton.applyControlButton.interactable = true;
                    StartCoroutine(Delay());
                }
            }
            
        }
    }

    private void CnangeControlFunction(KeyCodeFromControl keyCodeFromControl, KeyCode newKeyCode)
    {
        switch(keyCodeFromControl)
        {
            case KeyCodeFromControl.Consumable1: Control.singleton.Consumable1 = newKeyCode; break;
            case KeyCodeFromControl.Consumable2: Control.singleton.Consumable2 = newKeyCode; break;
            case KeyCodeFromControl.Consumable3: Control.singleton.Consumable3 = newKeyCode; break;
            case KeyCodeFromControl.Consumable4: Control.singleton.Consumable4 = newKeyCode; break;
            case KeyCodeFromControl.Consumable5: Control.singleton.Consumable5 = newKeyCode; break;
            case KeyCodeFromControl.RangedAttack: Control.singleton.RangedAttack = newKeyCode; break;
            case KeyCodeFromControl.CloseAttack: Control.singleton.CloseAttack = newKeyCode; break;
            case KeyCodeFromControl.OpenInventory: Control.singleton.OpenInventory = newKeyCode; break;
            case KeyCodeFromControl.TakeItem: Control.singleton.TakeItem = newKeyCode; break;
            case KeyCodeFromControl.ChangeArrows: Control.singleton.ChangeArrows = newKeyCode; break;
        }
    }

    private string KeyToText(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.Alpha1: return "1";
            case KeyCode.Alpha2: return "2";
            case KeyCode.Alpha3: return "3";
            case KeyCode.Alpha4: return "4";
            case KeyCode.Alpha5: return "5";
            case KeyCode.Alpha6: return "6";
            case KeyCode.Alpha7: return "7";
            case KeyCode.Alpha8: return "8";
            case KeyCode.Alpha9: return "9";
            case KeyCode.Alpha0: return "0";
            case KeyCode.Mouse0: return "À Ã";
            case KeyCode.Mouse1: return "œ Ã";
            case KeyCode.Mouse2: return "— Ã";

            default: return keyCode.ToString();
        }
    }

    private KeyCode GetKeyCodeControlFunction(KeyCodeFromControl controlFunction)
    {
        switch(controlFunction)
        {
            case KeyCodeFromControl.Consumable1: return Control.singleton.Consumable1;
            case KeyCodeFromControl.Consumable2: return Control.singleton.Consumable2;
            case KeyCodeFromControl.Consumable3: return Control.singleton.Consumable3;
            case KeyCodeFromControl.Consumable4: return Control.singleton.Consumable4;
            case KeyCodeFromControl.Consumable5: return Control.singleton.Consumable5;
            case KeyCodeFromControl.CloseAttack: return Control.singleton.CloseAttack;
            case KeyCodeFromControl.RangedAttack: return Control.singleton.RangedAttack;
            case KeyCodeFromControl.ChangeArrows: return Control.singleton.ChangeArrows;
            case KeyCodeFromControl.OpenInventory: return Control.singleton.OpenInventory;
            case KeyCodeFromControl.TakeItem: return Control.singleton.TakeItem;
        }
        return 0;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.2f);
        button.interactable = true;
    }



}
