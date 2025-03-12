using UnityEngine;
using System.Collections;
using Unity.Netcode;
using TMPro;
using UnityEngine.UI;

public class picturePuzzle : NetworkBehaviour
{
    public Image TestBar;
    [SerializeField] GameObject image;
    [SerializeField] GameObject text;
    public NetworkVariable<int> TotalCoins = new NetworkVariable<int>(
    0,
    NetworkVariableReadPermission.Everyone,  // �ء Client ��ҹ�����
    NetworkVariableWritePermission.Server   // ���� Server ��������
);
    private int dot;
    public TMP_Text healthText;
    private bool done;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TotalCoins.OnValueChanged += UpdateCoinUI;
    }
   
    // Update is called once per frame
    void Update()
    {
        TotalCoins.OnValueChanged += UpdateCoinUI;
        if (Input.GetKeyDown("space"))
        {
            image.SetActive(true);

        }
        if (dot == 4)
            {
            if(done == true) {return; }
            else { image.SetActive(false);
                text.SetActive(false);
                if (IsServer)
                {
                    TotalCoins.Value++;
                }
                else
                {
                    RequestIncreaseCoinsServerRpc();
                }


                done = true;
            }
               

        } 
        healthText.text = "test complete:" + TotalCoins.Value + "/5";
        TestBar.fillAmount = TotalCoins.Value*20/100f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (done == true) 
        { image.SetActive(false); }
        else
        {
            if (Input.GetKeyDown("space"))
           {
            image.SetActive(true);

           }
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        image.SetActive(false);
    }
    public void setdot()
    {
        dot++;
     //   Destroy(gameObject);
    }
    [ServerRpc(RequireOwnership = false)]
    private void RequestIncreaseCoinsServerRpc()
    {
        TotalCoins.Value++;
    }
    private void UpdateCoinUI(int oldValue, int newValue)
    {
        healthText.text = " " + newValue;
    }

}
