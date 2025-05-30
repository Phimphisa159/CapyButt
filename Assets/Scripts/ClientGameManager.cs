
using System;

using System.Text;

using System.Threading.Tasks;

using Unity.Netcode;

using Unity.Netcode.Transports.UTP;

using Unity.Networking.Transport.Relay;

using Unity.Services.Authentication;

using Unity.Services.Core;

using Unity.Services.Relay;

using Unity.Services.Relay.Models;

using UnityEngine;

using UnityEngine.SceneManagement;



public class ClientGameManager:IDisposable

{

    private JoinAllocation allocation;

    private NetworkClient networkClient;

    private const string MenuSceneName = "Menu";

    public void Dispose()
    {
        networkClient.Dispose();
    }
    public async Task<bool> InitAsync()

    {

        await UnityServices.InitializeAsync();



        networkClient = new NetworkClient(NetworkManager.Singleton);



        AuthState authState = await AuthenticationWrapper.DoAuth();



        if (authState == AuthState.Authenticated)

        {

            return true;

        }



        return false;

    }



    public void GoToMenu()

    {

        SceneManager.LoadScene(MenuSceneName);

    }



    public async Task StartClientAsync(string joinCode)

    {

        try

        {

            allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

        }

        catch (Exception e)

        {

            Debug.Log(e);

            return;

        }



        UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();



        RelayServerData relayServiceData = allocation.ToRelayServerData("dtls");

        transport.SetRelayServerData(relayServiceData);



        UserData userData = new UserData()

        {

            userName = PlayerPrefs.GetString(NameSelector.PlayerNameKey, "Missing Name"),

            userAuthId = AuthenticationService.Instance.PlayerId

        };

        string payload = JsonUtility.ToJson(userData);

        byte[] payloadBytes = Encoding.UTF8.GetBytes(payload);



        NetworkManager.Singleton.NetworkConfig.ConnectionData = payloadBytes;



        NetworkManager.Singleton.StartClient();

    }

}



