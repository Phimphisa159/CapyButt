using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;

public class PlayerAttack : NetworkBehaviour
{
    public int damage = 20; // ����������·�����
    public float attackRange = 5f; // ���С������
    public LayerMask enemyLayer; // �����������Ѻ�ѵ��
    public NetworkVariable<bool> IsImposter = new NetworkVariable<bool>(false);


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }


    }

    // �ѧ��ѹ������¡����ͼ���������
          void Attack()
    {

        // ���ҧ Raycast ����з���˹�
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
                
               } 
            
        }
       
    }

}