using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour {

    public GameObject[] scenes;
    private GameObject activeScene;

    void Start()
    {
        ChangeScene(1);
    }

	public void ChangeScene(int index)
    {
        foreach(GameObject scene in scenes)
        {
            if(scene.name == "Scene" + index)
            {
                scene.SetActive(true);
                activeScene = scene;
            }
            else
            {
                scene.SetActive(false);
            }
        }
    }

    public void ChangeSceneView(int index)
    {
        foreach(Transform view in activeScene.transform)
        {
            if(view.gameObject.name == "View" + index)
            {
                view.gameObject.SetActive(true);
            }
            else
            {
                view.gameObject.SetActive(false);
            }
        }
    }
}
