using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private int _nextLevel;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            LoadLevel();
        }
    }
    private void LoadLevel()
    {
        SceneManager.LoadSceneAsync(_nextLevel);
    }
}
