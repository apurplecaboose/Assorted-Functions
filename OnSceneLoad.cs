using UnityEngine.SceneManagement;

if (Input.GetKeyDown(KeyCode.Space))
{
  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  SceneManager.sceneLoaded += OnSceneLoaded;
}

void OnSceneLoaded(Scene currentScene, LoadSceneMode mode)
{
    //Do action when scene lis loaded
    print("onSceneLoad");
    TestReference = Camera.main.gameObject;
}

