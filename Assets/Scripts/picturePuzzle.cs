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
    public Puzzlevariable gameManager;
    private int dot;
    public TMP_Text healthText;
    private bool done;
    private int score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager.GetComponent<Puzzlevariable>();
        Puzzlevariable.TotalCoins.OnValueChanged += UpdateCoinUI;
        score = Puzzlevariable.TotalCoins.Value;
        //  openImage();


    }

    // Update is called once per frame
    void Update()
    {//
        Puzzlevariable.TotalCoins.OnValueChanged += UpdateCoinUI;
        score = Puzzlevariable.TotalCoins.Value;
        Puzzlevariable.TotalCoins.OnValueChanged += UpdateCoinUI;

        /* if (Puzzlevariable.TotalCoins != null)
         {
             healthText.text = "Coins: " + Puzzlevariable.TotalCoins.Value.ToString();
         }*/


        /*  if (Input.GetKeyDown("space"))
          {
             // image.SetActive(true);
              openImage();

          }*/
        if (dot == 4)
        {
            if (done == true)
            {
                return;
            }
            else
            {
                closeImage();
                text.SetActive(false);
                if (IsServer)
                {
                    Puzzlevariable.TotalCoins.Value++;

                }

                else
                {
                    RequestIncreaseCoinsServerRpc();

                }


                done = true;
            }


        }
        healthText.text = "test complete:" + Puzzlevariable.TotalCoins.Value + "/5";
        TestBar.fillAmount = Puzzlevariable.TotalCoins.Value * 20 / 100f;
    }
    private void OnTriggerStay2D(Collider2D collision)
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
        Puzzlevariable.TotalCoins.Value++;
        healthText.text = "test complete:" + Puzzlevariable.TotalCoins.Value + "/5";
        TestBar.fillAmount = Puzzlevariable.TotalCoins.Value * 20 / 100f;
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
