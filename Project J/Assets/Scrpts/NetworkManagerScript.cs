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
            //초기화 성공 
            Debug.Log("초기화 성공!");
        }
        else
        {
            //초기화 실패
            Debug.Log("초기화 실패!");
        }
    }

    void Start()
    {
        //BackendReturnObject bro = Backend.BMember.LoginWithTheBackendToken();
        //if (bro.IsSuccess())
        //{
        //    Debug.Log("자동 로그인에 성공했습니다");
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

        string id = inputId.text;               //아이디
        string password = inputPassword.text;                //비밀번호
        string passwordConfirm = inputPasswordConfirm.text;                //비밀번호 확인
        string nickname = inputNickname.text;               //닉네임

        if (id != null && password != null && passwordConfirm != null && nickname != null)
        {
            if (password == passwordConfirm)
            {                
                var bro = Backend.BMember.CustomSignUp(id, password);
                if (bro.IsSuccess())
                {
                    Backend.BMember.CreateNickname(nickname);
                    Debug.Log("회원가입 성공!");
                }
                else
                {
                    Debug.Log("회원가입 실패!");
                    Debug.LogError(bro);
                }
            }
            else
            {
                Debug.LogError("비밀번호 확인이 다릅니다.");
            }
        }
        else
        {
            Debug.LogError("값을 입력해주세요");
        }


        

    }

    public void CustomLogin()
    {
        InputField inputId = Login.GetComponent<AccountManagerScript>()._inputId;
        InputField inputPassword = Login.GetComponent<AccountManagerScript>()._inputPassword;

        string id = inputId.text;               //아이디
        string password = inputPassword.text;                //비밀번호

        var bro = Backend.BMember.CustomLogin(id, password);
        if (bro.IsSuccess())
        {
            Debug.Log("로그인 성공!");
        }
        else
        {
            Debug.Log("로그인 실패!");
            Debug.LogError(bro);
        }
    }

}
