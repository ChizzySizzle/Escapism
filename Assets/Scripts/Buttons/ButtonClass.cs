
using UnityEngine;
using UnityEngine.UI;

public class ButtonClass : MonoBehaviour
{
    protected Button button;

    public virtual void Start() {
        button = GetComponent<Button>();

        button.onClick.AddListener(OnButtonClick);
    }

    protected void CutAlpha() {
        gameObject.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }

    public virtual void OnButtonClick() {
        button.interactable = false;
        button.interactable = true;
    }
}