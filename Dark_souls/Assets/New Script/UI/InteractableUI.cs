using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableUI : MonoBehaviour
{
    public Text Interactabletext;
    public Text ItemText;
    public RawImage Itemimage;
    private GameObject HintBox;
    private GameObject HintItemBox;
    public bool ActiveItemUI = false;

    private void Start() {
        HintBox = this.transform.GetChild(0).gameObject;
        HintItemBox = this.transform.GetChild(1).gameObject;

        HintBox.gameObject.SetActive(false);
    }

    public void SetActiveInteractable(bool Active){
        HintBox.gameObject.SetActive(Active);
    }

    public void SetActiveItemInteractable(bool Active){
        HintItemBox.gameObject.SetActive(Active);
        ActiveItemUI = Active;
    }
}
