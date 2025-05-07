using Unity.Netcode;
using UnityEngine;

public class Puzzlevariable : NetworkBehaviour
{
    public static NetworkVariable<int> TotalCoins = new NetworkVariable<int>(
    0,
    NetworkVariableReadPermission.Everyone,  // ทุก Client อ่านค่าได้
    NetworkVariableWritePermission.Server  // มีแค่ Server ที่แก้ไขได้
);
    public CanvasGroup canvas;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (TotalCoins.Value == 5)
        {
            canvas.alpha = 1;
        }
    }
}
