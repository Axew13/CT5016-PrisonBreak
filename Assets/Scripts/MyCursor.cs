using UnityEngine;
using UnityEngine.UI;

public enum InteractMode
{
    NORMAL,
    INTERACT,
    JUMP
}

public class MyCursor : MonoBehaviour
{
    public Sprite crosshairSprite;
    public Sprite interactSprite;
    public Sprite jumpSprite;

//    public bool interacting = false;

    private Image imageRenderer;

    private void Start()
    {
        imageRenderer = this.GetComponent<Image>();
    }

    // Start is called before the first frame update
    public void SetInteract (InteractMode mode)
    {
//       this.interacting = false;
        if (mode == InteractMode.INTERACT)
        {
//            this.interacting = true;
            imageRenderer.sprite = interactSprite;
        }
        else if (mode == InteractMode.JUMP)
        {
            imageRenderer.sprite = jumpSprite;
        }
        else
        {
            imageRenderer.sprite = crosshairSprite;
        }
    }
}
