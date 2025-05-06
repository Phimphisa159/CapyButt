using Unity.Netcode;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public Transform[] spawnPoints;

    [ServerRpc(RequireOwnership = false)]
    public void MoveAllPlayersToSpawnServerRpc()
    {
        var clients = NetworkManager.Singleton.ConnectedClientsList;

        for (int i = 0; i < clients.Count; i++)
        {
            var playerObj = clients[i].PlayerObject;

            if (playerObj != null)
            {
                // เลือก spawnPoint ตามลำดับ หรือวนซ้ำถ้ามีผู้เล่นมากกว่า
                Transform spawn = spawnPoints[i % spawnPoints.Length];
               /* response.Position = SpawnPoint.GetRandomSpawnPos();
                response.Rotation = Quaternion.identity;*/

                playerObj.transform.position = SpawnPoint.GetInGame();
                playerObj.transform.rotation = Quaternion.identity;
            }
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // image.SetActive(true);
            MoveAllPlayersToSpawnServerRpc();

        }
    }
    /* public Transform spawnPoint;

 [ServerRpc(RequireOwnership = false)]

 private void Update()
 {
     if (Input.GetKeyDown(KeyCode.E))
     {
         // image.SetActive(true);
         ResetAllPlayersPositionServerRpc();

     }
 }
 public void ResetAllPlayersPositionServerRpc()
 {

     var clients = NetworkManager.Singleton.ConnectedClientsList;
     foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
     {
         for (int i = 0; i < clients.Count; i++)
         {
             var player = clients[i].PlayerObject;
             player.transform.position = spawnPoint.position;
         }
     }/* var clients = NetworkManager.Singleton.ConnectedClientsList;
 for (int i = 0; i < clients.Count && i < spawnPoints.Count; i++)
 {
     var player = clients[i].PlayerObject;
     player.transform.position = spawnPoints[i].position;
 }
 } */

}
