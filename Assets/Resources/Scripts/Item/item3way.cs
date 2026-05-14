using UnityEngine;

public class item3way : MonoBehaviour , IPlayerInteractive
{
    public void OnPlayerTouch(PlayerController player, PlayerManager manager)
    {
        manager.canUse3way = true;
        manager.UpdateUI();
        Destroy(gameObject);
    }
}
