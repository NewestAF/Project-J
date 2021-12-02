using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class NetworkManagerScript : MonoBehaviour
{
    public GameObject Signup;
    public GameObject Login;



    
    private void Awake()
    {
        var bro = Backend.Initialize(true);
        if (bro.IsSuccess())
        {
            //�ʱ�ȭ ���� 
            Debug.Log("�ʱ�ȭ ����!");
        }
        else
        {
            //�ʱ�ȭ ����
            Debug.Log("�ʱ�ȭ ����!");
        }
    }

    void Start()
    {
        //BackendReturnObject bro = Backend.BMember.LoginWithTheBackendToken();
        //if (bro.IsSuccess())
        //{
        //    Debug.Log("�ڵ� �α��ο� �����߽��ϴ�");
        //}
        //else
        //{
        //    Login.SetActive(true);
        //}
    }


    public void CustomSignup()
    {
        InputField inputId = Signup.GetComponent<AccountManagerScript>()._inputId;
        InputField inputPassword = Signup.GetComponent<AccountManagerScript>()._inputPassword;
        InputField inputPasswordConfirm = Signup.GetComponent<AccountManagerScript>()._inputPasswordConfirm;
        InputField inputNickname = Signup.GetComponent<AccountManagerScript>()._inputNickname;

        string id = inputId.text;               //���̵�
        string password = inputPassword.text;                //��й�ȣ
        string passwordConfirm = inputPasswordConfirm.text;                //��й�ȣ Ȯ��
        string nickname = inputNickname.text;               //�г���

        if (id != null && password != null && passwordConfirm != null && nickname != null)
        {
            if (password == passwordConfirm)
            {                
                var bro = Backend.BMember.CustomSignUp(id, password);
                if (bro.IsSuccess())
                {
                    Backend.BMember.CreateNickname(nickname);
                    Debug.Log("ȸ������ ����!");
                }
                else
                {
                    Debug.Log("ȸ������ ����!");
                    Debug.LogError(bro);
                }
            }
            else
            {
                Debug.LogError("��й�ȣ Ȯ���� �ٸ��ϴ�.");
            }
        }
        else
        {
            Debug.LogError("���� �Է����ּ���");
        }


        

    }

    public void CustomLogin()
    {
        InputField inputId = Login.GetComponent<AccountManagerScript>()._inputId;
        InputField inputPassword = Login.GetComponent<AccountManagerScript>()._inputPassword;

        string id = inputId.text;               //���̵�
        string password = inputPassword.text;                //��й�ȣ

        var bro = Backend.BMember.CustomLogin(id, password);
        if (bro.IsSuccess())
        {
            Debug.Log("�α��� ����!");
        }
        else
        {
            Debug.Log("�α��� ����!");
            Debug.LogError(bro);
        }
    }

}
