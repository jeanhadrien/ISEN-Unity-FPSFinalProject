using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private GameLogic _gameLogic;
    private Text _text;
    private Text _text1;

    private Text nbKillText, rateText;
    public GameObject sliderNbKill, sliderNbKillText;
    public GameObject sliderRate, sliderRateText;

    void Start()
    {
        _gameLogic = GameObject.FindWithTag("Logic").GetComponent<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        var nbkill = sliderNbKill.GetComponent<Slider>().value;
        var rate = sliderRate.GetComponent<Slider>().value;
        sliderRateText.GetComponent<TextMeshProUGUI>().text = rate.ToString();
        sliderNbKillText.GetComponent<TextMeshProUGUI>().text = nbkill.ToString();
        _gameLogic.SetNbKill((int) nbkill);
        _gameLogic.SetRate(rate);
    }
}