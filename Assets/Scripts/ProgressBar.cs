using Assets.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;

    public Text levelText;

    public GameObject levelUpPanel;

    public AudioSource LevelUpSound;

    public int level = 0;

    void Update()
    {
        levelText.text = $"LVL: {level}";
    }

    public void AddSomeExp(float experience)
    {
        slider.value += experience;
        if (slider.maxValue == slider.value)
        {
            level++;
            slider.value = 0;
            slider.maxValue = Configs.IncreaseMaxExperenceCoefficient * level;
            StartCoroutine(ShowLevelUpPanelTask());
        }
    }

    private IEnumerator ShowLevelUpPanelTask()
    {
        LevelUpSound.Play();
        levelUpPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        levelUpPanel.SetActive(false);
        yield return null;
    }
}
