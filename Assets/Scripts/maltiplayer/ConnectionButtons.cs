using UnityEngine;
using Unity.Netcode;

public class ConnectionButtons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void StartCilent()
    {
        NetworkManager.Singleton.StartClient();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
