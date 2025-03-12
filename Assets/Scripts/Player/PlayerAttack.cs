using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;

public class PlayerAttack : NetworkBehaviour
{
    public int damage = 20; // ความเสียหายที่ทำได้
    public float attackRange = 5f; // ระยะการโจมตี
    public LayerMask enemyLayer; // เลเยอร์สำหรับศัตรู
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }


    }

    // ฟังก์ชันที่เรียกเมื่อผู้เล่นโจมตี
          void Attack()
    {

        // สร้าง Raycast ในระยะที่กำหนด
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, attackRange);

        if (hit.collider != null)
        {
                Health enemyHealth = hit.collider.GetComponent<Health>();
                if (enemyHealth != null)
               { if (hit.collider.gameObject == gameObject)
                    {
                return;
                     }
                else
                {
                    enemyHealth.TakeDamage(damage);
                Debug.Log("Attacked " + hit.collider.name + " for " + damage + " damage.");

                }
                // เรียกฟังก์ชัน TakeDamage ของผู้เล่นที่ถูกโจมตี
                
                
               } 
            
        }
       
    }

}