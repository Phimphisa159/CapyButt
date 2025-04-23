using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;

public class NetworkGameObjectManager : NetworkBehaviour
{
    // ��˹� NetworkVariable ����� NetworkObjectId (ID �ͧ GameObject)
    public NetworkVariable<List<ulong>> networkedGameObjects = new NetworkVariable<List<ulong>>(new List<ulong>());

    // ��˹� NetworkVariable �����红����� IsImposter ����Ѻ���� GameObject
    public NetworkVariable<bool> isImposter = new NetworkVariable<bool>(false);

    // �ѧ����㹡������ GameObject ����� List
    public void AddGameObjectToNetworkList(GameObject obj)
    {
        if (IsServer) // ੾�н�������������ҹ�鹷������ö�ѻവ NetworkVariable ��
        {
            ulong objectId = obj.GetComponent<NetworkObject>().NetworkObjectId;
            networkedGameObjects.Value.Add(objectId);
        }
    }

    // �ѧ����㹡���������͡ GameObject �ҡ List ����� IsImposter
    public void SetImposterForRandomGameObject()
    {
        if (IsServer && networkedGameObjects.Value.Count > 0)
        {
            // �������͡ GameObject �ҡ List
            int randomIndex = Random.Range(0, networkedGameObjects.Value.Count);
            ulong selectedObjectId = networkedGameObjects.Value[randomIndex];

            // �֧ GameObject �ҡ NetworkObjectId
            GameObject selectedObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[selectedObjectId].gameObject;

            // ��˹� IsImposter ����� true
            // �������� GameObject ��ʤ�Ի���������� GameObjectProperties ����յ���� IsImposter
            selectedObject.GetComponent<PlayerAttack>().IsImposter.Value = true;

            // ����� Network Variable �ѻവ (����ö���� NetworkVariable ����Ѻ IsImposter ���蹡ѹ)
            isImposter.Value = true;
        }
    }
    

    // ������ҧ�����ҹ
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        // ���� GameObject ŧ� List (������ҧ)
     //   AddGameObjectToNetworkList(player); // ����� someGameObject ����� GameObject ���س��ͧ���
    }
}
