using UnityEngine;
using System.Collections;
using Unity.Netcode;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class picturePuzzle : NetworkBehaviour
{
    public Image TestBar;
    [SerializeField] CanvasGroup image;
    [SerializeField] GameObject text;
    public NetworkVariable<int> TotalCoins = new NetworkVariable<int>(
    0,
    NetworkVariableReadPermission.Everyone,  // ทุก Client อ่านค่าได้
    NetworkVariableWritePermission.Server   // มีแค่ Server ที่แก้ไขได้
);
    private int dot;
    public TMP_Text healthText;
    private bool done;
    private int score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      TotalCoins.OnValueChanged += UpdateCoinUI;
        score = TotalCoins.Value;
        
      //  openImage();
      

    }
   
    // Update is called once per frame
    void Update()
    {
        score = TotalCoins.Value;
     //TotalCoins.OnValueChanged += UpdateCoinUI;
        
        if (Input.GetKeyDown("space"))
        {
           // image.SetActive(true);
            openImage();

        }
        if (dot == 4)
            {
              if(done == true)
            {
                return; 
            }
            else
            { 
                closeImage();
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
        healthText.text = "test complete:" + score+"/5";
        TestBar.fillAmount =score*20/100f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit!!");
        if (done == true) 
        { closeImage(); }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
           {
               // image.SetActive(true);
                openImage();

           }
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        closeImage();
    }
    public void setdot()
    {
        dot++;
     //Destroy(gameObject);
    }
    [ServerRpc(RequireOwnership = false)]
    private void RequestIncreaseCoinsServerRpc()
    {
        TotalCoins.Value++;
        healthText.text = "test complete:" + score + "/5";
        TestBar.fillAmount = score * 20 / 100f;
    }
    private void UpdateCoinUI(int oldValue, int newValue)
    {
        healthText.text = "test complete" + newValue + "/5";
        TestBar.fillAmount = newValue * 20 / 100f;

    }

    private void closeImage()
    {
        image.alpha = 0;
        image.blocksRaycasts = false;
        image.interactable = false;
    }
    private void openImage()
    {
        image.alpha = 1;
        image.blocksRaycasts = true;
        image.interactable = true;
    }

}
