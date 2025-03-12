using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Health : NetworkBehaviour
{
    [field: SerializeField] public int MaxHealth { get; private set; } = 100;
    public NetworkVariable<int> CurrentHealth = new NetworkVariable<int>();
    private NetworkVariable<bool> isDead = new NetworkVariable<bool>(false);
    [SerializeField] GameObject diedScreen;
    // private bool isDead;

    public Action<Health> OnDie;
    public override void OnNetworkSpawn()
    {
        if (!IsServer) {return; }

        CurrentHealth.Value = MaxHealth;
    }
    public void TakeDamage(int damageValue)
    {
       if (!IsOwner) return;

        ModifyHealth(-damageValue);
    }
    public void RestoreHealth(int healValue) 
    {
        ModifyHealth(healValue);
    }
    public void ModifyHealth(int value)
    {
        if(isDead.Value == true) { return; }
        RequestChangeHPServerRpc(value);
       // CurrentHealth.Value = Mathf.Clamp(newHealth,0,MaxHealth);

        if(CurrentHealth.Value <= 0)
        {
            OnDie?.Invoke(this);
            RequestDiedInServerRpc();
            diedScreen.SetActive(true);


        }
    }
    [Rpc(SendTo.ClientsAndHost)]
    private void RequestDiedInServerRpc()
    {
        isDead.Value = true;
        gameObject.SetActive(false);
    }
    [ServerRpc(RequireOwnership = false)]
    private void RequestChangeHPServerRpc(int value)
    {
        int newHealth = CurrentHealth.Value + value;
        CurrentHealth.Value = Mathf.Clamp(newHealth, 0, MaxHealth);
    }

}
