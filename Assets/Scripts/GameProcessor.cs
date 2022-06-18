using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.UI;

public class GameProcessor : MonoBehaviour
{

    [SerializeField] private AudioSource MainTheme;
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject PlayerButton;
    [SerializeField] private ProgressBar ProgressBar;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private Text CurrentLEvelText;

    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> bullets = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 1;
        MainTheme.Play();
        StartCoroutine(CleanBulletCollectionTask());
        StartCoroutine(SpawnEnemyTask());
        StartCoroutine(CleanEnemyCollectionTask());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void InsertBullet(GameObject bullet)
    {
        bullets.Add(bullet);
    }

    private IEnumerator CleanBulletCollectionTask()
    {
       while (true)
        {
            if (bullets.Count > Configs.MaxBulletInGame)
            {
                for (int i = 0; i < 100; i++)
                {
                    var bullet = bullets[0];
                    bullets.RemoveAt(0);
                    Destroy(bullet);
                    yield return null;
                }
            }
            yield return null;
        }
    }

    private IEnumerator CleanEnemyCollectionTask()
    {
        while (true)
        {
            if (enemies.Count > Configs.MaxEnemiesInGame)
            {
                for (int i = 0; i < 100; i++)
                {
                    var enemy = enemies[0];
                    enemies.RemoveAt(0);
                    Destroy(enemy);
                    yield return null;
                }
            }
            yield return null;
        }
    }

    private IEnumerator SpawnEnemyTask()
    {
        var dx = new[] { -37, 37 };
        var dy = new[] { -19, 19 };
        while (true)
        {
            var x = Random.Range(0, 2);
            var y = Random.Range(0, 2);
            Vector3 position = new Vector3(dx[x], dy[y], 0) + PlayerButton.transform.position;
            var enemy = Instantiate(EnemyPrefab, position, Quaternion.identity);
            enemy.GetComponent<Enemy>().SetTarget(PlayerButton.transform);
            enemy.GetComponent<Enemy>().SetProgressBar(ProgressBar);
            enemy.GetComponent<Enemy>().SetGameProcessor(this);
            yield return new WaitForSeconds(3f);
        }
    }

    public void StartGameOver()
    {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
        CurrentLEvelText.text = $"The Current Level is {ProgressBar.level}.";
    }
}
