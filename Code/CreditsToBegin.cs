using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsToBegin : MonoBehaviour {
    public int levelNum;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Enter"))
        {
            SceneManager.LoadScene(levelNum);
        }
    }
}
