using System;

using Unity.Netcode;

using UnityEngine;



public class DealDamageOnContact : NetworkBehaviour

{
  
    [SerializeField] private int damage = 1000;

    private ulong ownerClientId;

    public void SetOwner(ulong ownerClientId)

    {
        this.ownerClientId = ownerClientId;
    }

    private void OnTriggerEnter2D(Collider2D col)

    {
        
        if (col.attachedRigidbody == null) { return; }

        if (col.attachedRigidbody.TryGetComponent<NetworkObject>(out NetworkObject netObj))

        {

            if (ownerClientId == netObj.OwnerClientId)

            {
                return;
            }

        }
   
        if (col.attachedRigidbody.TryGetComponent<Health>(out Health health))

        {
                health.TakeDamage(damage);
                Debug.Log("kill");
        }
       
    }

}