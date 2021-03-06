using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 entrancePosition;
    public VectorValueScriptableObject entrancePositionVector;
    public bool outside;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !other.isTrigger)
        {
            if (outside)
            {
                FindObjectOfType<GameManager>().GetComponent<SaveSceneState>().SavePositions();
            }
            FindObjectOfType<GameManager>().GetComponent<SaveAndLoad>().Save();
            entrancePositionVector.value = entrancePosition;
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void GoHome()
    {
        FindObjectOfType<GameManager>().GetComponent<SaveAndLoad>().Save();
        entrancePositionVector.value = entrancePosition;
        SceneManager.LoadScene(sceneToLoad);
    }
}
