using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerTest : MonoBehaviour {
    List<Scene> sceneList = new List<Scene>();
    List<GameObject> sceneRootList = new List<GameObject>();

    IEnumerator LoadScene(string sceneName, LoadSceneMode mode)
    {
        var asyncOp = SceneManager.LoadSceneAsync(sceneName, mode);

        yield return asyncOp;

        sceneList.Add(SceneManager.GetSceneByName(sceneName));

        var obj = GameObject.Find(sceneName);
        if(obj != null)
        {
            sceneRootList.Add(obj);
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    IEnumerator Start () {
        yield return StartCoroutine(LoadScene("Scene001", LoadSceneMode.Additive));
        yield return StartCoroutine(LoadScene("Scene002", LoadSceneMode.Additive));

        ChangeActiveScene(0);
    }

    void ChangeActiveScene(int index)
    {
        for (int i = 0; i < sceneList.Count; i++)
        {
            if (index == i)
            {
                SceneManager.SetActiveScene(sceneList[i]);

                for (int j = 0; j < sceneList.Count; j++)
                {
                    sceneRootList[j].SetActive(i == j);
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        for(int i = 0; i < sceneList.Count; i++)
        {
            if (GUILayout.Button(sceneList[i].name)) {
                SceneManager.SetActiveScene(sceneList[i]);

                for(int j = 0; j < sceneList.Count; j++)
                {
                    sceneRootList[j].SetActive(i == j);
                }
            }
        }
        GUILayout.EndVertical();
    }
}
