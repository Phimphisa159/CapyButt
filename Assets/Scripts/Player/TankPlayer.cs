
using Unity.Netcode;

using UnityEngine;

using Unity.Cinemachine;

using Unity.Collections;



public class TankPlayer : NetworkBehaviour

{

    [Header("References")]

    [SerializeField] private CinemachineCamera virtualCamera;



    [Header("Settings")]

    [SerializeField] private int ownerPriority = 15;



    public NetworkVariable<FixedString32Bytes> PlayerName = new NetworkVariable<FixedString32Bytes>();



    public override void OnNetworkSpawn()

    {

        if (IsServer)

        {

            UserData UserData =

                HostSingleton.Instance.GameManager.NetworkServer.GetUserDataByClientId(OwnerClientId);



            PlayerName.Value = UserData.userName;

        }



        if (IsOwner)

        {

            virtualCamera.Priority = ownerPriority;

        }

    }

}



