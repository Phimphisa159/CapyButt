using Unity.Netcode;
using UnityEngine;

public class Puzzlevariable : NetworkBehaviour
{
    public static NetworkVariable<int> TotalCoins = new NetworkVariable<int>(
    0,
    NetworkVariableReadPermission.Everyone,  // �ء Client ��ҹ�����
    NetworkVariableWritePermission.Server  // ���� Server ��������
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
