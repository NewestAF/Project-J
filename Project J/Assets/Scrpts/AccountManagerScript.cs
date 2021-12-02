using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountManagerScript : MonoBehaviour
{
    public InputField _inputId;
    public InputField _inputPassword;
    public InputField _inputPasswordConfirm;
    public InputField _inputNickname;
    public GameObject SignupManager;


    public void TurnOnSignup()
    {
        gameObject.SetActive(false);
        SignupManager.SetActive(true);
    }



}
