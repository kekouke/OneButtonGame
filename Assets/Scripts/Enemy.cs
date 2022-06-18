using Assets.Scripts;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Transform PlayerButton;
    private ProgressBar progressBar;
    private Collider2D enemyColider;
    private GameProcessor gameProcessor;

    // Start is called before the first frame update
    void Start()
    {
        enemyColider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerButton != null)
        {
            var direction = -(transform.position - PlayerButton.position).normalized;
            MoveTo(direction);
        }
    }

    private void OnTriggerStay2D(Collider2D colider)
    {
        if (colider.transform.tag == "Bullet")
        {
            var bulletScript = colider.gameObject.GetComponent<Bullet>();
            if (bulletScript == null || !bulletScript.IsActiveObject)
            {
                return;
            }           
            if (colider.bounds.size.sqrMagnitude > enemyColider.bounds.size.sqrMagnitude)
            {
                progressBar.AddSomeExp(Configs.ExperienceOnKillEnemy);
                Destroy(colider.gameObject);
                Destroy(gameObject);
            } else { 
                Destroy(colider.gameObject);
            }
        }
        else if (colider.transform.tag == "Player")
        {
            gameProcessor.StopAllCoroutines();
            Destroy(colider.gameObject);
            gameProcessor.StartGameOver();
        }
    }

    public void MoveTo(Vector3 direction)
    {
        transform.position += direction * Configs.EnemySpeed * Time.deltaTime;
    }

    public void SetTarget(Transform target)
    {
        PlayerButton = target;
    }

    public void SetProgressBar(ProgressBar progressBar)
    {
        this.progressBar = progressBar;
    }

    public void SetGameProcessor(GameProcessor gameProcessor)
    {
        this.gameProcessor = gameProcessor;
    }
}
