using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAudio : MonoBehaviour, IPointerEnterHandler
{
    AudioManager audioManager;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioManager == null) {
            audioManager = AudioManager.Instance;
        }

        if (eventData.pointerEnter)
        {
            audioManager.PlaySFX("UI Select");
        }
    }
}
