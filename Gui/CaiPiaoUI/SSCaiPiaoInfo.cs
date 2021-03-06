﻿using System;
using System.Collections;
using UnityEngine;

public class SSCaiPiaoInfo : SSGameMono
{
    /// <summary>
    /// 数字缩放动画.
    /// </summary>
    public Animator m_AnimationNumSuoFang;
    /// <summary>
    /// 数字动画声音.
    /// </summary>
    public AudioSource m_AniAudio;
    /// <summary>
    /// 彩票数字管理组件.
    /// </summary>
    public SSGameNumUI m_GameNumUI;
    /// <summary>
    /// 正在出票UI.
    /// </summary>
    public GameObject m_ZhengZaiChuPiaoUI;
    /// <summary>
    /// 设置正在出票UI的显示状态.
    /// </summary>
    public void SetActiveZhengZaiChuPiao(bool isActive)
    {
        if (m_ZhengZaiChuPiaoUI != null)
        {
            m_ZhengZaiChuPiaoUI.SetActive(isActive);
        }
        else
        {
            UnityLogWarning("SetActiveZhengZaiChuPiao -> m_ZhengZaiChuPiaoUI was null!");
        }
    }

    /// <summary>
    /// 是否初始化彩票数字动画.
    /// </summary>
    bool IsInitCaiPiaoAni = false;
    int m_MaxCaiPiaoNumAni = 0;
    int m_MinCaiPiaoNumAni = 0;
    float TimeCaiPiaoAni = 1f;
    float TimeLastCaiPiaoAni = 0f;
    PlayerEnum IndexPlayer = PlayerEnum.Null;
    /// <summary>
    /// 初始化彩票数字动画.
    /// </summary>
    public void InitCaiPiaoAnimation(float timeVal, PlayerEnum indexPlayer)
    {
        UnityLog("SSCaiPiaoInfo::InitCaiPiaoAnimation -> indexPlayer == " + indexPlayer
            + ", time == " + Time.time);
        IndexPlayer = indexPlayer;

        int indexVal = (int)indexPlayer - 1;
        int caiPiao = XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_PcvrPrintCaiPiaoData[indexVal].CaiPiaoVal;
        int caiPiaoCache = XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_PcvrPrintCaiPiaoData[indexVal].CaiPiaoValCache;
        if (caiPiao + caiPiaoCache > 5)
        {
            timeVal = 0.8f;
        }
        else if (caiPiao + caiPiaoCache > 2)
        {
            timeVal = 0.5f;
        }
        else
        {
            timeVal = 0.2f;
        }
        TimeCaiPiaoAni = timeVal;
        TimeLastCaiPiaoAni = Time.time;

        int totalCaiPiao = caiPiao + caiPiaoCache;
        m_MaxCaiPiaoNumAni = (int)(Mathf.Pow(10f, totalCaiPiao.ToString().Length) - 1f);
        m_MinCaiPiaoNumAni = (int)Mathf.Pow(10f, totalCaiPiao.ToString().Length - 1);
        IsInitCaiPiaoAni = true;

        if (m_GameNumUI != null)
        {
            //显示彩票数据UI信息.
            m_GameNumUI.SetActive(true);
        }

        if (m_AniAudio != null)
        {
            m_AniAudio.enabled = true;
            m_AniAudio.loop = true;
			m_AniAudio.Play();
        }
    }

    public void Init(PlayerEnum indexPlayer)
    {
        switch (indexPlayer)
        {
            case PlayerEnum.PlayerOne:
                {
                    InputEventCtrl.GetInstance().ClickStartBtOneEvent += ClickStartBtOneEvent;
                    break;
                }
            case PlayerEnum.PlayerTwo:
                {
                    InputEventCtrl.GetInstance().ClickStartBtTwoEvent += ClickStartBtTwoEvent;
                    break;
                }
            case PlayerEnum.PlayerThree:
                {
                    InputEventCtrl.GetInstance().ClickStartBtThreeEvent += ClickStartBtThreeEvent;
                    break;
                }
        }
    }

    private void ClickStartBtOneEvent(pcvr.ButtonState val)
    {
        if (val == pcvr.ButtonState.UP)
        {
            PcvrRestartPrintCaiPiao(PlayerEnum.PlayerOne);
        }
    }

    private void ClickStartBtTwoEvent(pcvr.ButtonState val)
    {
        if (val == pcvr.ButtonState.UP)
        {
            PcvrRestartPrintCaiPiao(PlayerEnum.PlayerTwo);
        }
    }

    private void ClickStartBtThreeEvent(pcvr.ButtonState val)
    {
        if (val == pcvr.ButtonState.UP)
        {
            PcvrRestartPrintCaiPiao(PlayerEnum.PlayerThree);
        }
    }
    
    /// <summary>
    /// 重新开始出票.
    /// </summary>
    void PcvrRestartPrintCaiPiao(PlayerEnum indexPlayer)
    {
        //这里添加pcvr重新出票的代码.
        pcvr.GetInstance().RestartPrintCaiPiao(indexPlayer);
    }

    void Update()
    {
        if (m_GameNumUI != null && IsInitCaiPiaoAni)
        {
            if (Time.time - TimeLastCaiPiaoAni <= TimeCaiPiaoAni)
            {
                m_GameNumUI.ShowNumUI(UnityEngine.Random.Range(m_MinCaiPiaoNumAni, m_MaxCaiPiaoNumAni));
            }
            else
            {
                UnityLog("SSCaiPiaoInfo::CloseCaiPiaoAnimation -> indexPlayer == " + IndexPlayer
                    + ", time == " + Time.time);
                //结束彩票数字动画.
                IsInitCaiPiaoAni = false;
                if (SSUIRoot.GetInstance().m_GameUIManage != null)
                {
                    int indexVal = (int)IndexPlayer - 1;
                    if (SSUIRoot.GetInstance().m_GameUIManage)
                    {
                        //显示玩家当前彩票数据信息.
                        int caiPiao = XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_PcvrPrintCaiPiaoData[indexVal].CaiPiaoVal;
                        int caiPiaoCache = XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_PcvrPrintCaiPiaoData[indexVal].CaiPiaoValCache;
                        //m_GameNumUI.ShowNumUI(caiPiao + caiPiaoCache);
                        if (SSUIRoot.GetInstance().m_GameUIManage != null)
                        {
                            //显示玩家彩票数量.
                            SSUIRoot.GetInstance().m_GameUIManage.ShowPlayerCaiPiaoInfo(IndexPlayer, caiPiao + caiPiaoCache, false, true);
                        }
                    }
                }

                if (m_AniAudio != null)
                {
					m_AniAudio.Stop();
                    m_AniAudio.enabled = false;
                }
            }
        }
    }

    /// <summary>
    /// 播放彩票数字缩放动画.
    /// </summary>
    public void PlayCaiPiaoNumSuoFangAnimation()
    {
        if (m_AnimationNumSuoFang != null)
        {
            if (m_GameNumUI != null)
            {
                //显示彩票数据UI信息.
                m_GameNumUI.SetActive(true);
            }
            m_AnimationNumSuoFang.enabled = true;
            m_AnimationNumSuoFang.SetBool("IsPlay", true);
            StartCoroutine(DelayCloseCaiPiaoNumSuoFangAnimation());
        }
    }

    IEnumerator DelayCloseCaiPiaoNumSuoFangAnimation()
    {
        yield return new WaitForSeconds(3f);
        if (m_AnimationNumSuoFang != null)
        {
            m_AnimationNumSuoFang.SetBool("IsPlay", false);
            //m_AnimationNumSuoFang.enabled = false;
        }
    }

    bool IsRemoveSelf = false;
    internal void RemoveSelf(PlayerEnum indexPlayer)
    {
        if (IsRemoveSelf == false)
        {
            IsRemoveSelf = true;

            switch (indexPlayer)
            {
                case PlayerEnum.PlayerOne:
                    {
                        InputEventCtrl.GetInstance().ClickStartBtOneEvent -= ClickStartBtOneEvent;
                        break;
                    }
                case PlayerEnum.PlayerTwo:
                    {
                        InputEventCtrl.GetInstance().ClickStartBtTwoEvent -= ClickStartBtTwoEvent;
                        break;
                    }
                case PlayerEnum.PlayerThree:
                    {
                        InputEventCtrl.GetInstance().ClickStartBtThreeEvent -= ClickStartBtThreeEvent;
                        break;
                    }
            }
            Destroy(gameObject);
        }
    }
}