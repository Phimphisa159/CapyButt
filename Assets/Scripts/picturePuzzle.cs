using UnityEngine;
using System.Collections;
using Unity.Netcode;
using TMPro;

public class picturePuzzle : NetworkBehaviour
{
    [SerializeField] GameObject image;
    [SerializeField] GameObject text;
    public NetworkVariable<int> TotalCoins = new NetworkVariable<int>();
    private int dot;
    public TMP_Text healthText;
    private bool done;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            image.SetActive(true);

        }
        if (dot == 4)
            {
            if(done == true) {return; }
            else { image.SetActive(false);
                text.SetActive(false);
                 TotalCoins.Value ++;
                done = true;
            }
               

        } 
        healthText.text = " "+TotalCoins.Value;
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
}
