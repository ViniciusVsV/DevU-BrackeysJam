using UnityEngine;
using UnityEngine.SceneManagement;

public class Teste : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            SceneManager.LoadScene("Final Menu");

        else if (Input.GetKeyDown(KeyCode.P))
            AudioController.Instance.PlayTestSound();
    }
}