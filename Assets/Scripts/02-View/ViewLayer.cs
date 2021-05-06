using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ViewLayer : MonoBehaviour
{
    private RectTransform titleNameRecTf;
    private RectTransform menuUIPanelRecTf;
    private RectTransform gameUIPanelRecTf;
    private RectTransform gameOverUIPanelTf;
    private RectTransform gameSettingUIPanelTf;
    private RectTransform gameRankUIPanelTf;
    private RectTransform gamePromptUIPanelTf;

    private GameObject restartButton;
    private GameObject muteSoundButton;

    //UI to show the score
    private Text curScoreTxt;
    private Text highestScoreTxt;
    private Text gameOverScoreTxt;
    private Text roundCountsTxt;
    private Text rankHighestScoreTxt;

    private string promptBehaviourName = "Default Nothing"; // prompt ui behaviour is confirm operation or n
    public string PromptBehaviourName { get { return promptBehaviourName; } }

    private void Awake()
    {
        titleNameRecTf = transform.Find("Canvas/TitleName_txt") as RectTransform;
        menuUIPanelRecTf = transform.Find("Canvas/MenuUI_panel") as RectTransform;
        gameUIPanelRecTf = transform.Find("Canvas/GameUI_panel") as RectTransform;
        gameOverUIPanelTf = transform.Find("Canvas/GameOverUI_panel") as RectTransform;
        gameSettingUIPanelTf = transform.Find("Canvas/SettingUI_panel") as RectTransform;
        gameRankUIPanelTf = transform.Find("Canvas/RecordUI_panel") as RectTransform;
        gamePromptUIPanelTf = transform.Find("Canvas/PromptUI_panel") as RectTransform;

        restartButton = transform.Find("Canvas/MenuUI_panel/ReStart_btn").gameObject;
        muteSoundButton = transform.Find("Canvas/SettingUI_panel/SettingBg_img/Sound_btn/muteIcon_img").gameObject;

        curScoreTxt = transform.Find("Canvas/GameUI_panel/CurScoreCount_txt").GetComponent<Text>();
        highestScoreTxt = transform.Find("Canvas/GameUI_panel/HighestScoreCount_txt").GetComponent<Text>();
        gameOverScoreTxt = transform.Find("Canvas/GameOverUI_panel/GameOverBg_img/CurScoreCount_txt").GetComponent<Text>();
        roundCountsTxt = transform.Find("Canvas/RecordUI_panel/RecordBg_img/RoundCounts_txt").GetComponent<Text>();
        rankHighestScoreTxt = transform.Find("Canvas/RecordUI_panel/RecordBg_img/RankHighestScore_txt").GetComponent<Text>();
    }

    public void ShowMenu(int roundCounts, int highestScore)
    {
        titleNameRecTf.gameObject.SetActive(true);
        titleNameRecTf.DOAnchorPosY(-191.7f, 0.5f);
        menuUIPanelRecTf.gameObject.SetActive(true);
        menuUIPanelRecTf.DOAnchorPosY(125f, 0.5f);
        ShowRanKDataTxt(roundCounts, highestScore);
    }

    public void ShowRanKDataTxt(int roundCounts, int highestScore)
    {
        this.rankHighestScoreTxt.text = highestScore.ToString();
        this.roundCountsTxt.text = roundCounts.ToString();
    }
    public void HideMenu()
    {
        titleNameRecTf.DOAnchorPosY(105f, 0.5f)
            .OnComplete(delegate {
            titleNameRecTf.gameObject.SetActive(false);
        } );
        menuUIPanelRecTf.DOAnchorPosY(-113f, 0.5f)
            .OnComplete(delegate {
                menuUIPanelRecTf.gameObject.SetActive(false);
            });
    }

    public void UpdateScoreGameUI(int curScore, int highestScore)
    {
        this.curScoreTxt.text = curScore.ToString();
        this.highestScoreTxt.text = highestScore.ToString();
        this.rankHighestScoreTxt.text = highestScore.ToString();
    }

    public void ShowGameUI(int curScore = 0, int highestScore = 0)
    {
        UpdateScoreGameUI(curScore, highestScore);
        gameUIPanelRecTf.gameObject.SetActive(true);
        gameUIPanelRecTf.DOAnchorPosY(-302f, 0.5f);
    }

    public void HideGameUI()
    {
        gameUIPanelRecTf.DOAnchorPosY(-91f, 0.5f)
            .OnComplete(delegate
            {
                gameUIPanelRecTf.gameObject.SetActive(false);
            });
    }

    public void OnRestartButtonShow()
    {
        restartButton.SetActive(true);
    }

    public void ShowGameOverUI(int curScore = 0)
    {
        FadeUIPanel(true, gameOverUIPanelTf, "GameOverBg_img", 0.5f, 0f);
        gameOverUIPanelTf.gameObject.SetActive(true);
        gameOverScoreTxt.text = curScore.ToString();
        gameOverUIPanelTf.DOSizeDelta(new Vector2(825f, 1439.5f), 0.5f);
    }

    public void HideGameOverUI(bool isReloadScene, ControlLayer ctrl = null)
    {
        FadeUIPanel(false, gameOverUIPanelTf, "GameOverBg_img", 0f, 0.2f);
        gameOverUIPanelTf.DOSizeDelta(new Vector2(1800f, 4000f), 0.5f)
            .OnComplete(delegate
            {
                gameOverUIPanelTf.gameObject.SetActive(false);
                if (isReloadScene) { ctrl.gameManager.ReloadGameScene(); }
            });
    }

    public void ShowSettingUI()
    {
        gameSettingUIPanelTf.gameObject.SetActive(true);
        FadeUIPanel(true, gameSettingUIPanelTf, "SettingBg_img", 0.5f, 0f);
        gameSettingUIPanelTf.DOSizeDelta(new Vector2(850f, 1440f), 0.5f);
    }

    public void HideSettingUI()
    {
        FadeUIPanel(false, gameSettingUIPanelTf, "SettingBg_img", 0f, 0.2f);
        gameSettingUIPanelTf.DOSizeDelta(new Vector2(1850f, 3440f), 0.5f)
            .OnComplete(delegate
            {
                gameSettingUIPanelTf.gameObject.SetActive(false);
            });
    }

    public void ShowRankUI()
    {
        gameRankUIPanelTf.gameObject.SetActive(true);
        FadeUIPanel(true, gameRankUIPanelTf, "RecordBg_img", 0.5f, 0f);
        gameRankUIPanelTf.DOSizeDelta(new Vector2(850f, 1440f), 0.5f);
    }

    public void HideRankUI()
    {
        FadeUIPanel(false, gameRankUIPanelTf, "RecordBg_img", 0f, 0.2f);
        gameRankUIPanelTf.DOSizeDelta(new Vector2(1850f, 3440f), 0.5f)
            .OnComplete(delegate
            {
                gameRankUIPanelTf.gameObject.SetActive(false);
            });
    }

    public void ShowPromptUIPanel(string promptForWhichBehaviour)
    {
        promptBehaviourName = promptForWhichBehaviour;
        gamePromptUIPanelTf.gameObject.SetActive(true);
        FadeUIPanel(true, gamePromptUIPanelTf, "PromptBg_img", 0.5f, 0f);
        gamePromptUIPanelTf.DOSizeDelta(new Vector2(750f, 1334f), 0.5f);
    }

    public void HidePromptUIPanel()
    {
        FadeUIPanel(false, gamePromptUIPanelTf, "PromptBg_img", 0f, 0.2f);
        gamePromptUIPanelTf.DOSizeDelta(new Vector2(1600f, 2700f), 0.5f)
            .OnComplete(delegate
            {
                gamePromptUIPanelTf.gameObject.SetActive(false);
            });
    }

    private void FadeUIPanel(bool isShow, RectTransform rectTf, string panelBgName, float endValue, float duration)
    {
        float fadeLess = 0.35f;
        rectTf.GetComponent<Image>().DOFade(endValue, duration);
        if (isShow) { endValue += fadeLess; }
        rectTf.Find(panelBgName).GetComponent<Image>().DOFade(endValue, duration);
    }

    public void OnMuteSoundImageActive(bool isOn)
    {
        muteSoundButton.SetActive(isOn);
    }
}
