using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyStatsUI : MonoBehaviour {

    [SerializeField]
    private RectTransform healthBarRect;
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text stage;

    // Use this for initialization
    void Start()
    {
        if (!healthBarRect)
        {
            Debug.LogError("STATUS INDICATOR:No health bar obj referened");
        }
        if (!healthText)
        {
            Debug.LogError("STATUS INDICATOR:No health text obj referened");
        }

    }

    public void SetHealth(float _cur, float _max)
    {
        float _value = (float)_cur / _max;

        healthBarRect.localScale = new Vector3(_value, healthBarRect.localScale.y, healthBarRect.localScale.z);
        healthText.text = _cur + "/" + _max + "HP";
    }

    public void SetStage(int _stage)
    {
        stage.text = _stage.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
