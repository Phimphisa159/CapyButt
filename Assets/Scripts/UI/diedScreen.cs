using UnityEngine;

public class diedScreen : MonoBehaviour
{
  [SerializeField]  GameObject penal;
    [SerializeField] GameObject text;
    [SerializeField] GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
            
    }

    // Update is called once per frame
    void Update()
    {
       player = GameObject.FindWithTag("Player");
        if (player == null)
        { return; }
        else { penal.SetActive(true);
        text.SetActive(true);} 
    }
}
