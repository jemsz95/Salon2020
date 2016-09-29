using UnityEngine;
using System.Collections;

public class SortUIManager : MonoBehaviour {

    public GameObject goFrontUI, goEndUI;

	public void SwapUIs()
    {
        goFrontUI.SetActive(!goFrontUI.activeSelf);
        goEndUI.SetActive(!goEndUI.activeSelf);
    }
}
