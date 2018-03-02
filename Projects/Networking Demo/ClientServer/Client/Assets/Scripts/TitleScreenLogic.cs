using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleScreenLogic : MonoBehaviour {

    public Text server;
    public Text port;
    public Text enterName;

    public ExampleClient client;

    public GameObject LoginUI;
    public GameObject ReadyUI;
    public GameObject RedWinsUI;
    public GameObject BlueWinsUI;

    public void Connect()
    {
        if (enterName.text != "")
        {
            client.ConnectToServer(server.text, int.Parse(port.text), enterName.text);
           // client.InputName(enterName.text);
        }
       
    }

    public void HideMouse()
    {
        Cursor.visible = false;
    }

    public void ToGameUI()
    {
        LoginUI.transform.localPosition = new Vector3(1000, 0, 0);
        ReadyUI.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void AfterReady()
    {
        ReadyUI.transform.localPosition = new Vector3(1000, 0, 0);
    }

    public void RedWins()
    {
        RedWinsUI.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void BlueWins()
    {
        BlueWinsUI.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void ToLogin()
    {
        LoginUI.transform.localPosition = new Vector3(0, 0, 0);
        ReadyUI.transform.localPosition = new Vector3(0, 0, 0);
    }
}
