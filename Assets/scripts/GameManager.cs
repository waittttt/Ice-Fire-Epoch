using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject failCanvas;

    public GameObject winCanvas;
    private bool isGameOver = false;

    private bool isGameWin = false;
    public AudioClip failSound;
    public AudioClip winSound;
    private AudioSource audioSource;
    void Start()
    {
        // 确保失败画布初始状态是隐藏的
        if (failCanvas != null)
        {
            failCanvas.SetActive(false);
        }
        if (winCanvas != null)
        {
            winCanvas.SetActive(false);
        }
        audioSource = GetComponent<AudioSource>();
    }
    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        // 显示失败画布
        if (failCanvas != null)
        {
            failCanvas.SetActive(true);
        }


        audioSource.clip = failSound;
        audioSource.loop = false;
        audioSource.Play();


        // 暂停游戏
        //Time.timeScale = 0f;
    }
    public void GameWin()
    {
        if (isGameWin) return;

        isGameWin = true;
        audioSource.clip = winSound;
        audioSource.loop = false;
        audioSource.Play();

        // 显示失败画布
        if (winCanvas != null)
        {
            winCanvas.SetActive(true);
        }
    }

    public void RestartGame()
    {
        // 恢复时间
        Time.timeScale = 1f;

        // 重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        // 恢复时间
        Time.timeScale = 1f;

        // 退出游戏（在编辑器中无效）
        Application.Quit();
    }
    /*    public void RestartGame()
        {
            // 使用当前场景的名称重新加载场景
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }*/
    /*
        public void LoadSceneByName(string sceneName)
        {
            // 加载指定名称的场景
            SceneManager.LoadScene(sceneName);
        }

        public void LoadSceneByIndex(int sceneIndex)
        {
            // 加载指定索引的场景
            SceneManager.LoadScene(sceneIndex);
        }
        */
}
