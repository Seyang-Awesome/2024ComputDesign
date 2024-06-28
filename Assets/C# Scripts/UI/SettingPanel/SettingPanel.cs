using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] private GameObject settingPanelRoot;
    [SerializeField] private Slider mouseSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider seSlider;

    private void Start()
    {
        mouseSlider.onValueChanged.AddListener(OnMouseSliderValueChanged);
        bgmSlider.onValueChanged.AddListener(OnBgmSliderValueChanged);
        seSlider.onValueChanged.AddListener(OnSeSliderValueChanged);
    }

    public void Show()
    {
        settingPanelRoot.SetActive(true);
        UpdatePanel();
    }

    public void Hide()
    {
        settingPanelRoot.SetActive(false);
        UpdatePanel();
    }

    private void UpdatePanel()
    {
        mouseSlider.value = GlobalSettings.mouseSensitivity;
        bgmSlider.value = AudioManager.Instance.BgmVolume;
        seSlider.value = AudioManager.Instance.SeVolume;
    }

    private void OnMouseSliderValueChanged(float val)
    {
        GlobalSettings.mouseSensitivity = val;
    }

    private void OnBgmSliderValueChanged(float _)
    {
        AudioManager.Instance.SetVolume(bgmSlider.value, seSlider.value);
    }

    private void OnSeSliderValueChanged(float _)
    {
        AudioManager.Instance.SetVolume(bgmSlider.value, seSlider.value);
    }
}
