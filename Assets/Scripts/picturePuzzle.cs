using UnityEngine;
using System.Collections;

public class picturePuzzle : MonoBehaviour
{
    [SerializeField] GameObject image;
    [SerializeField] GameObject text;
    private int dot;
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
                image.SetActive(false);
                text.SetActive(false);
            
            } 
    }
   public void setdot()
    {
        dot++;
     //   Destroy(gameObject);
    }
}
