using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WalletLogin: MonoBehaviour
{
    public Text messageText;

    void Start() {
        // if remember me is checked, set the account to the saved account
        // if (rememberMe.isOn && PlayerPrefs.GetString("Account") != "")
        // {
        //     //test~~~~~~
        //     Debug.Log(PlayerPrefs.GetString("Account"));
        //     // move to next scene
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // }
    }

    async public void OnLogin()
    {
        // Butona basıldığında hiçbir şey olmaması için fonksiyonun içini boşalttık.
        // Debug.Log("Login button clicked!"); // İsteğe bağlı olarak bu satırı da kaldırabilirsiniz.
        // if (messageText != null)
        // {
        //     messageText.text = "tıklamayınız";
        // }
        // // get current timestamp
        // int timestamp = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        // // set expiration time
        // int expirationTime = timestamp + 60;
        // // set message
        // string message = expirationTime.ToString();
        // // sign message
        // string signature = await Web3Wallet.Sign(message);
        // // verify account
        // string account = await EVM.Verify(message, signature);
        // int now = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        // // validate
        // if (account.Length == 42 && expirationTime >= now) {
        //     // save account
        //     GameObject.Find("DontDistroy_Wallet").GetComponent<DontDistroyGameObject>().account = account;
        //     GameObject.Find("DontDistroy_Wallet").GetComponent<DontDistroyGameObject>().isLogined = true;
        //     // load next scene
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // }
    }
    

    public void skipLogin(){
        Debug.Log("Skip button clicked!");
        GameObject.Find("DontDistroy_Wallet").GetComponent<DontDistroyGameObject>().account = "NOWALLET";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
