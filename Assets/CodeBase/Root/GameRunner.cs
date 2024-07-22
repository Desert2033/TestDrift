using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRunner : MonoBehaviour
{
    private const string BoostrapScene = "BoostrapScene";

    private void Start()
    {
        GameBootrapper bootrapper = FindObjectOfType<GameBootrapper>();

        if (bootrapper == null)
            SceneManager.LoadScene(BoostrapScene);
    }
}
