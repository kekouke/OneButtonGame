using System.Collections;
using UnityEngine;
using Assets.Scripts;


public class Sattelite : MonoBehaviour
{
    const float fullCircleRadians = 6.28320f;

    private GameObject bulletInCreationProcess;
    private Vector3 positionOffset;
    private float angle;
    private float buttonPressedTime;
    private float radius;

    [SerializeField] private float RotationSpeed;
    [SerializeField] private Transform Target;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private PlayerButton ButtonComponent;
    [SerializeField] private GameProcessor gameProcessor;
    [SerializeField] private ProgressBar ProgressBar;

    private void Awake()
    {
        radius = (Target.position - transform.position).magnitude;
    }

    private void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (InputController.WasKeyPressed(KeyCode.Space))
        {
            StartCoroutine(CreateBulletTask());
        }
        if (InputController.WasKeyReleased(KeyCode.Space))
        {
            var direction = (Target.position - transform.position).normalized;
            ButtonComponent.ImpulseMove(direction, Configs.ImpulseOnCLick + Configs.MaxImpulse * buttonPressedTime);
            ProgressBar.AddSomeExp(Configs.ExperienceOnCLick * buttonPressedTime);
            buttonPressedTime = 0;
        }
        if (!InputController.IsKeyPressed(KeyCode.Space))
        {
            MoveAroundCircle();
        }
    }


    private void MoveAroundCircle()
    {
        positionOffset.Set(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, transform.position.z);
        transform.position = Target.position + positionOffset;
        angle = (angle + Time.deltaTime * RotationSpeed) % fullCircleRadians;
    }

    public IEnumerator CreateBulletTask()
    {
        bulletInCreationProcess = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        var bulletScript = bulletInCreationProcess.GetComponent<Bullet>();
        var impulsePower = buttonPressedTime;
        while (InputController.IsKeyPressed(KeyCode.Space))
        {
            IncreasePressedTime();
            impulsePower = buttonPressedTime;
            bulletInCreationProcess.transform.position = transform.position;
            bulletScript.SetScale(impulsePower);
            yield return null;
        }
        var direction = (transform.position - Target.position).normalized;
        bulletScript.Push(direction * (Configs.ImpulseOnCLick + Configs.MaxImpulse * impulsePower));
        gameProcessor.InsertBullet(bulletInCreationProcess);
        bulletInCreationProcess = null;
        bulletScript.IsActiveObject = true;
        yield return null;
    }

    private void IncreasePressedTime()
    {
        buttonPressedTime += Time.deltaTime * 2;
        buttonPressedTime = Mathf.Clamp01(buttonPressedTime);
    }
}
