using UnityEngine;

public class Bone : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (GameData.drawerOpened)
        {
            GameData.hasBone = true;

            this.gameObject.SetActive(false);
        }
    }
}
