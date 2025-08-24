using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalMenuController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            SceneManager.LoadScene("Main Menu");
    }
}