using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;

public class NetworkGameObjectManager : NetworkBehaviour
{
    // กำหนด NetworkVariable ที่เก็บ NetworkObjectId (ID ของ GameObject)
    public NetworkVariable<List<ulong>> networkedGameObjects = new NetworkVariable<List<ulong>>(new List<ulong>());

    // กำหนด NetworkVariable เพื่อเก็บข้อมูล IsImposter สำหรับแต่ละ GameObject
    public NetworkVariable<bool> isImposter = new NetworkVariable<bool>(false);

    // ฟังก์ชั่นในการเพิ่ม GameObject เข้าไปใน List
    public void AddGameObjectToNetworkList(GameObject obj)
    {
        if (IsServer) // เฉพาะฝั่งเซิร์ฟเวอร์เท่านั้นที่สามารถอัปเดต NetworkVariable ได้
        {
            ulong objectId = obj.GetComponent<NetworkObject>().NetworkObjectId;
            networkedGameObjects.Value.Add(objectId);
        }
    }

    // ฟังก์ชั่นในการสุ่มเลือก GameObject จาก List และเซ็ต IsImposter
    public void SetImposterForRandomGameObject()
    {
        if (IsServer && networkedGameObjects.Value.Count > 0)
        {
            // สุ่มเลือก GameObject จาก List
            int randomIndex = Random.Range(0, networkedGameObjects.Value.Count);
            ulong selectedObjectId = networkedGameObjects.Value[randomIndex];

            // ดึง GameObject จาก NetworkObjectId
            GameObject selectedObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[selectedObjectId].gameObject;

            // กำหนด IsImposter ให้เป็น true
            // สมมติว่า GameObject มีสคริปต์ที่ชื่อว่า GameObjectProperties ซึ่งมีตัวแปร IsImposter
            selectedObject.GetComponent<PlayerAttack>().IsImposter.Value = true;

            // แจ้งให้ Network Variable อัปเดต (สามารถเพิ่ม NetworkVariable สำหรับ IsImposter ได้เช่นกัน)
            isImposter.Value = true;
        }
    }
    

    // ตัวอย่างการใช้งาน
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        // เพิ่ม GameObject ลงใน List (ตัวอย่าง)
     //   AddGameObjectToNetworkList(player); // ตัวแปร someGameObject ควรเป็น GameObject ที่คุณต้องการ
    }
}
