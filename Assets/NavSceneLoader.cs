using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NavSceneLoader : MonoBehaviour {

    public string sSceneName;
    public GameObject canvasFade;

	void OnTriggerEnter(Collider other)
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        canvasFade.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(sSceneName);
    }


}
