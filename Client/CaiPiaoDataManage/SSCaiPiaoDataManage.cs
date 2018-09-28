//#define TEST_SHOW_PLAYER_CAIPIAO
//#define TEST_OUT_PRINT_CARD
//#define CREATE_SUPER_JPBOSS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSCaiPiaoDataManage : SSGameMono
{
    public class GuDingBanCaiPiaoJPBossData
    {
        /// <summary>
        /// ��С������ֵ.
        /// </summary>
        internal int MinCoin = 0;
        /// <summary>
        /// ���������ֵ.
        /// </summary>
        internal int MaxCoin = 0;
        /// <summary>
        /// JP�󽱴򱬹̶��Ų�����.
        /// </summary>
        internal int JPBossDeCai = 0;
        public GuDingBanCaiPiaoJPBossData(int min, int max, int caiPiaoVal)
        {
            MinCoin = min;
            MaxCoin = max;
            JPBossDeCai = caiPiaoVal;
        }
    }
    /// <summary>
    /// �̶����Ʊ����.
    /// </summary>
    public class GuDingBanCaiPiaoData
    {
        /// <summary>
        /// ս���򱬹̶���Ʊ30��(ս����������)
        /// </summary>
        internal int ZhanCheDeCai = 30;
        /// <summary>
        /// JP�󽱴򱬹̶��Ų�1000��2000��3000�ţ�������ݳ������õļ���������һ�ҵ��ڶ����Ų�Ʊȷ��.
        /// 1����3��������1000��
        /// 4����5��������2000��
        /// 6����10��������3000��
        /// </summary>
        internal GuDingBanCaiPiaoJPBossData[] JPBossDeCaiData = new GuDingBanCaiPiaoJPBossData[3]
        {
            new GuDingBanCaiPiaoJPBossData(1, 3, 1000),
            new GuDingBanCaiPiaoJPBossData(4, 5, 2000),
            new GuDingBanCaiPiaoJPBossData(6, 10, 3000),
        };

        /// <summary>
        /// ��ȡJPBoss��Ʊ����.
        /// </summary>
        public int GetJPBossChuPiaoTiaoJian()
        {
            int chuPiaoVal = 0;
            int coinStart = XKGlobalData.GameNeedCoin;
            int length = JPBossDeCaiData.Length;
            for (int i = 0; i < length; i++)
            {
                if (coinStart >= JPBossDeCaiData[i].MinCoin && coinStart <= JPBossDeCaiData[i].MaxCoin)
                {
                    chuPiaoVal = JPBossDeCaiData[i].JPBossDeCai;
                    break;
                }
            }
            return chuPiaoVal;
        }
    }
    public GuDingBanCaiPiaoData m_GuDingBanCaiPiaoData = new GuDingBanCaiPiaoData();

    /// <summary>
    /// JPBoss������.
    /// </summary>
    [System.Serializable]
    public class JPBossDaJiangData
    {
        public Transform CaiPiaoLiZiPoint;
        /// <summary>
        /// ��Ʊ����Ԥ��.
        /// </summary>
        public GameObject CaiPiaoLiZiPrefab;
    }
    /// <summary>
    /// JPBoss�󽱲�Ʊ����.
    /// </summary>
    GameObject m_JPBossDaJiangCaiPiaoLiZiObj;
    /// <summary>
    /// JPBoss������.
    /// </summary>
    public JPBossDaJiangData m_JPBossDaJiangData;
    /// <summary>
    /// ����JPBoss�󽱲�Ʊ����.
    /// </summary>
    public void CreatJPBossDaJiangCaiPiaoLiZi()
    {
        if (m_JPBossDaJiangCaiPiaoLiZiObj == null)
        {
            if (m_JPBossDaJiangData.CaiPiaoLiZiPrefab != null && m_JPBossDaJiangData.CaiPiaoLiZiPoint != null)
            {
                m_JPBossDaJiangCaiPiaoLiZiObj = (GameObject)Instantiate(m_JPBossDaJiangData.CaiPiaoLiZiPrefab, m_JPBossDaJiangData.CaiPiaoLiZiPoint);
            }
            else
            {
                UnityLogWarning("CreatJPBossDaJiangCaiPiaoLiZi -> CaiPiaoLiZiPrefab or CaiPiaoLiZiPoint was null!");
            }
        }
    }

    /// <summary>
    /// ɾ��JPBoss�󽱲�Ʊ����.
    /// </summary>
    public void RemoveJPBossDaJiangCaiPiaoLiZi()
    {
        if (m_JPBossDaJiangCaiPiaoLiZiObj != null)
        {
            Destroy(m_JPBossDaJiangCaiPiaoLiZiObj);
        }
    }

    /// <summary>
    /// ��Ϸ��Ʊ����.
    /// </summary>
    [System.Serializable]
    public class GameCaiPiaoData
    {
        float _XuBiChuPiaoLv = 0.7f;
        /// <summary>
        /// ���ҳ�Ʊ��.
        /// </summary>
        public float XuBiChuPiaoLv
        {
            set
            {
                _XuBiChuPiaoLv = value;
            }
            get
            {
                return _XuBiChuPiaoLv;
            }
        }
        float _ZhengChangChuPiaoLv = 0.4f;
        /// <summary>
        /// �����òʳ�Ʊ��.
        /// </summary>
        public float ZhengChangChuPiaoLv
        {
            set
            {
                _ZhengChangChuPiaoLv = value;
            }
            get
            {
                return _ZhengChangChuPiaoLv;
            }
        }
        /// <summary>
        /// ս���òʳ�Ʊ��.
        /// </summary>
        float ZhanCheChuPiaoLv = 0.3f;
        /// <summary>
        /// ������߳�Ʊ��.
        /// </summary>
        float SuiJiDaoJuChuPiaoLv = 0.05f;
        /// <summary>
        /// JPBoss��Ʊ��.
        /// </summary>
        float JPBossChuPiaoLv = 0.25f;
        /// <summary>
        /// ս����Ʊ����(��Ϸ�����������Ը�ֵ).
        /// </summary>
        float ZhanCheChuPiaoTiaoJian = 2.5f;
        /// <summary>
        /// ������߳�Ʊ����(��Ϸ�����������Ը�ֵ).
        /// </summary>
        float SuiJiDaoJuChuPiaoTiaoJian = 0.5f;
        /// <summary>
        /// JPBoss��Ʊ����(��Ϸ�����������Ը�ֵ).
        /// </summary>
        float JPBossChuPiaoTiaoJian = 50f;
        int _ZhanCheDeCai = 0;
        /// <summary>
        /// ս���ò��ۻ�����.
        /// </summary>
        public int ZhanCheDeCai
        {
            set
            {
                XKGlobalData.GetInstance().SetZhanCheCaiChi(value);
                _ZhanCheDeCai = value;
            }
            get
            {
                return _ZhanCheDeCai;
            }
        }
        //int _SuiJiDaoJuDeCai = 0;
        /// <summary>
        /// ������ߵò��ۻ�����.
        /// </summary>
        public int SuiJiDaoJuDeCai
        {
            set
            {
                XKGlobalData.GetInstance().SetDaoJuCaiChi(value);
                //_SuiJiDaoJuDeCai = value;
            }
            get
            {
                return XKGlobalData.GetInstance().m_DaoJuCaiChi;
                //return _SuiJiDaoJuDeCai;
            }
        }
        int _JPBossDeCai = 0;
        /// <summary>
        /// JPBoss�ò��ۻ�����.
        /// </summary>
        public int JPBossDeCai
        {
            set
            {
                XKGlobalData.GetInstance().SetJPBossCaiChi(value);
                _JPBossDeCai = value;
            }
            get
            {
                return _JPBossDeCai;
            }
        }
        
        /// <summary>
        /// �ò�״̬.
        /// </summary>
        public enum DeCaiState
        {
            /// <summary>
            /// ս������.
            /// </summary>
            ZhanChe = 0,
            /// <summary>
            /// �����������.
            /// </summary>
            SuiJiDaoJu = 1,
            /// <summary>
            /// JPBoss����.
            /// </summary>
            JPBoss = 2,
            /// <summary>
            /// ��ͨ�����ò�����.
            /// </summary>
            ZhengChang = 3,
        }

        /// <summary>
        /// ����ò�������Ϣ.
        /// </summary>
        public void FenPeiDeCaiVal(bool isPlayerXuBi)
        {
            int coinStart = XKGlobalData.GetInstance().m_CoinToCard * XKGlobalData.GameNeedCoin;
            float xuBiChuPiaoLvTmp = isPlayerXuBi == true ? XuBiChuPiaoLv : 1f;
            if (isPlayerXuBi)
            {
                //������һ��۵�Ԥ֧��Ʊ�صĲ�Ʊ����.
                int jiLeiToYuZhiCaiPiaoChiVal = (int)(coinStart * XuBiChuPiaoLv);
                XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_GameYuZhiCaiPiaoData.AddYuZhiCaiPiao(jiLeiToYuZhiCaiPiaoChiVal);
            }

            coinStart = (int)(coinStart * xuBiChuPiaoLvTmp);
            ZhanCheDeCai += (int)(coinStart * ZhanCheChuPiaoLv);
            int suiJiDaoJuDeCaiFenPei = (int)(coinStart * SuiJiDaoJuChuPiaoLv);
            if (suiJiDaoJuDeCaiFenPei < 1)
            {
                //���ٸ�������߷���һ�Ų�Ʊ.
                suiJiDaoJuDeCaiFenPei = 1;
            }
            SuiJiDaoJuDeCai += suiJiDaoJuDeCaiFenPei;
            JPBossDeCai += (int)(coinStart * JPBossChuPiaoLv);
            Debug.Log("Unity: FenPeiDeCaiVal -> coinStart == " + coinStart
                + ", ZhanCheDeCai == " + ZhanCheDeCai
                + ", SuiJiDaoJuDeCai == " + SuiJiDaoJuDeCai
                + ", JPBossDeCai == " + JPBossDeCai
                + ", isPlayerXuBi ==== " + isPlayerXuBi);
        }

        /// <summary>
        /// ��ȥ��Ϸĳ�����͵ò��ۻ�����.
        /// </summary>
        public void SubGameDeCaiValByDeCaiState(PlayerEnum index, DeCaiState type, SuiJiDaoJuState suiJiDaoJuType = SuiJiDaoJuState.BaoXiang)
        {
            if (XkGameCtrl.GetInstance().m_GamePlayerAiData.IsActiveAiPlayer == true)
            {
                //û�м����κ����.
                return;
            }

            int val = 0;
            int coinStart = XKGlobalData.GetInstance().m_CoinToCard * XKGlobalData.GameNeedCoin;
            switch (type)
            {
                case DeCaiState.ZhanChe:
                    {
                        if (XkGameCtrl.GetInstance().m_CaiPiaoMode == XkGameCtrl.CaiPiaoModeSuanFa.GuDing)
                        {
                            val = XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_GuDingBanCaiPiaoData.ZhanCheDeCai;
                        }
                        else
                        {
                            val = (int)(coinStart * ZhanCheChuPiaoTiaoJian);
                        }
                        ZhanCheDeCai -= val;
                        //��Ԥ�Ʋʳ���ȡ��ƱͶ��ս���ʳ�.
                        XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_GameYuZhiCaiPiaoData.SubZhanCheCaiPiaoVal();
                        break;
                    }
                case DeCaiState.SuiJiDaoJu:
                    {
                        //�������.
                        float suiJiDaoJuChuPiaoLv = 0f;
                        SuiJiDaoJuData suiJiDaoJuData = XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_SuiJiDaoJuData;
                        if (suiJiDaoJuType == SuiJiDaoJuState.TouZi)
                        {
                            suiJiDaoJuChuPiaoLv = suiJiDaoJuData.TouZiDePiaoLv;
                        }
                        else
                        {
                           suiJiDaoJuChuPiaoLv = suiJiDaoJuData.BaoXiangDePiaoLv;
                        }
                        val = (int)(coinStart * SuiJiDaoJuChuPiaoTiaoJian);

                        //Ӧ�ø���ҵĲ�Ʊ����.
                        int outCaiPiao = (int)(val * suiJiDaoJuChuPiaoLv);

                        //������߻��۵�Ԥ֧��Ʊ�صĲ�Ʊ����.
                        int jiLeiToYuZhiCaiPiaoChiVal = val - outCaiPiao;
                        XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_GameYuZhiCaiPiaoData.AddYuZhiCaiPiao(jiLeiToYuZhiCaiPiaoChiVal);

                        val = outCaiPiao;
                        SuiJiDaoJuDeCai -= val;
                        break;
                    }
                case DeCaiState.JPBoss:
                    {
                        if (XkGameCtrl.GetInstance().m_CaiPiaoMode == XkGameCtrl.CaiPiaoModeSuanFa.GuDing)
                        {
                            val = XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_GuDingBanCaiPiaoData.GetJPBossChuPiaoTiaoJian();
                        }
                        else
                        {
                            val = (int)(coinStart * JPBossChuPiaoTiaoJian);
                        }
                        JPBossDeCai -= val;
                        //��Ԥ�Ʋʳ���ȡ��ƱͶ��JPBoss�ʳ�.
                        XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_GameYuZhiCaiPiaoData.SubJPBossCaiPiaoVal();
                        break;
                    }
            }

            if (val > 0)
            {
                //��ʱ��Ʊ��Ӧ�ø���Ӧ��ҳ�val�Ų�Ʊ.
                Debug.Log("Unity: SubGameDeCaiValByDeCaiState -> index ====== " + index
                    + ", chuPiaoVal ====== " + val
                    + ", type ======= " + type);
                XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.AddCaiPiaoToPlayer(index, val, type);
            }
        }

        /// <summary>
        /// ��ȡ��Ҫ��ӡ�Ĳ�Ʊ����.
        /// </summary>
        public int GetPrintCaiPiaoValueByDeCaiState(DeCaiState type, SuiJiDaoJuState suiJiDaoJuType = SuiJiDaoJuState.BaoXiang)
        {
            int value = 0;
            int coinStart = XKGlobalData.GetInstance().m_CoinToCard * XKGlobalData.GameNeedCoin;
            switch (type)
            {
                case DeCaiState.ZhanChe:
                    {
                        if (XkGameCtrl.GetInstance().m_CaiPiaoMode == XkGameCtrl.CaiPiaoModeSuanFa.GuDing)
                        {
                            value = XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_GuDingBanCaiPiaoData.ZhanCheDeCai;
                        }
                        else
                        {
                            value = (int)(coinStart * ZhanCheChuPiaoTiaoJian);
                        }
                        break;
                    }
                case DeCaiState.SuiJiDaoJu:
                    {
                        float suiJiDaoJuChuPiaoLv = 0f;
                        SuiJiDaoJuData suiJiDaoJuData = XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_SuiJiDaoJuData;
                        if (suiJiDaoJuType == SuiJiDaoJuState.TouZi)
                        {
                            suiJiDaoJuChuPiaoLv = suiJiDaoJuData.TouZiDePiaoLv;
                        }
                        else
                        {
                            suiJiDaoJuChuPiaoLv = suiJiDaoJuData.BaoXiangDePiaoLv;
                        }
                        value = (int)(coinStart * SuiJiDaoJuChuPiaoTiaoJian);
                        //Ӧ�ø���ҵĲ�Ʊ����.
                        value = (int)(value * suiJiDaoJuChuPiaoLv);
                        break;
                    }
                case DeCaiState.JPBoss:
                    {
                        if (XkGameCtrl.GetInstance().m_CaiPiaoMode == XkGameCtrl.CaiPiaoModeSuanFa.GuDing)
                        {
                            value = XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_GuDingBanCaiPiaoData.GetJPBossChuPiaoTiaoJian();
                        }
                        else
                        {
                            value = (int)(coinStart * JPBossChuPiaoTiaoJian);
                        }
                        break;
                    }
            }
            return value;
        }

        /// <summary>
        /// �ж��Ƿ�ﵽĳ�ֵò����͵ĳ�������.
        /// </summary>
        public bool GetIsChuCaiPiaoByDeCaiState(DeCaiState type)
        {
            bool isChuPiao = false;
            int coinToCaiPiao = XKGlobalData.GetInstance().m_CoinToCard * XKGlobalData.GameNeedCoin;
            float chuPiaoTiaoJian = 0f;
            int deCaiVal = -1;
            switch (type)
            {
                case DeCaiState.ZhanChe:
                    {
                        chuPiaoTiaoJian = ZhanCheChuPiaoTiaoJian;
                        deCaiVal = ZhanCheDeCai;
                        break;
                    }
                case DeCaiState.SuiJiDaoJu:
                    {
                        chuPiaoTiaoJian = SuiJiDaoJuChuPiaoTiaoJian;
                        deCaiVal = SuiJiDaoJuDeCai;
                        break;
                    }
                case DeCaiState.JPBoss:
                    {
                        chuPiaoTiaoJian = JPBossChuPiaoTiaoJian;
                        deCaiVal = JPBossDeCai;
                        break;
                    }
            }

            int chuCaiVal = (int)(coinToCaiPiao * chuPiaoTiaoJian);
            if (XkGameCtrl.GetInstance().m_CaiPiaoMode == XkGameCtrl.CaiPiaoModeSuanFa.GuDing
                && XkPlayerCtrl.GetInstanceFeiJi() != null)
            {
                switch (type)
                {
                    case DeCaiState.ZhanChe:
                        {
                            chuCaiVal = XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_GuDingBanCaiPiaoData.ZhanCheDeCai;
                            break;
                        }
                    case DeCaiState.JPBoss:
                        {
                            chuCaiVal = XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_GuDingBanCaiPiaoData.GetJPBossChuPiaoTiaoJian();
                            break;
                        }
                }
            }

            if (deCaiVal >= chuCaiVal)
            {
                isChuPiao = true;
                //Debug.Log("Unity: GetIsChuCaiPiaoBy -> the type is can shuCaiPiao! type ============ " + type);
            }
            return isChuPiao;
        }

        /// <summary>
        /// ��ȡ��ǰ�ʳ��ǳ�Ʊ�����ļ���.
        /// </summary>
        public int GetChuPiaoTiaoJianBeiShu(DeCaiState type)
        {
            int chuPiaoBeiShu = 0;
            int coinToCaiPiao = XKGlobalData.GetInstance().m_CoinToCard * XKGlobalData.GameNeedCoin;
            float chuPiaoTiaoJian = 0f;
            int deCaiVal = -1;
            switch (type)
            {
                case DeCaiState.ZhanChe:
                    {
                        chuPiaoTiaoJian = ZhanCheChuPiaoTiaoJian;
                        deCaiVal = ZhanCheDeCai;
                        break;
                    }
                case DeCaiState.SuiJiDaoJu:
                    {
                        chuPiaoTiaoJian = SuiJiDaoJuChuPiaoTiaoJian;
                        deCaiVal = SuiJiDaoJuDeCai;
                        break;
                    }
                case DeCaiState.JPBoss:
                    {
                        chuPiaoTiaoJian = JPBossChuPiaoTiaoJian;
                        deCaiVal = JPBossDeCai;
                        break;
                    }
            }

            int chuCaiVal = (int)(coinToCaiPiao * chuPiaoTiaoJian);
            chuPiaoBeiShu = deCaiVal / chuCaiVal;
            return chuPiaoBeiShu;
        }
    }
    /// <summary>
    /// ��Ϸ��Ʊ������Ϣ.
    /// </summary>
    [HideInInspector]
    public GameCaiPiaoData m_GameCaiPiaoData = new GameCaiPiaoData();


    /// <summary>
    /// �����������.
    /// </summary>
    public enum SuiJiDaoJuState
    {
        /// <summary>
        /// ����.
        /// </summary>
        TouZi = 0,
        /// <summary>
        /// ����.
        /// </summary>
        BaoXiang = 1,
    }

    /// <summary>
    /// �������������Ϣ.
    /// </summary>
    [System.Serializable]
    public class SuiJiDaoJuData
    {
        float _TouZiGaiLv = 0.5f;
        /// <summary>
        /// ���Ӳ����ĸ���.
        /// </summary>
        public float TouZiGaiLv
        {
            get
            {
                return _TouZiGaiLv;
            }
        }
        /// <summary>
        /// ���������������ĵ�Ʊ��.
        /// </summary>
        internal float TouZiDePiaoLv = 0.6f;
        /// <summary>
        /// ���������������ĵ�Ʊ��.
        /// </summary>
        internal float BaoXiangDePiaoLv = 0.8f;
        /// <summary>
        /// ���ӵ���Ԥ��.
        /// </summary>
        public GameObject TouZiPrefab;
        /// <summary>
        /// �������Ԥ��.
        /// </summary>
        public GameObject BaoXiangPrefab;
    }
    /// <summary>
    /// �������������Ϣ.
    /// </summary>
    public SuiJiDaoJuData m_SuiJiDaoJuData = new SuiJiDaoJuData();
    /// <summary>
    /// ��ϷԤ֧��Ʊ����.
    /// </summary>
    internal SSGameYuZhiCaiPiaoData m_GameYuZhiCaiPiaoData = new SSGameYuZhiCaiPiaoData();

    /// <summary>
    /// ��ȡ�������Ԥ��.
    /// </summary>
    public GameObject GetSuiJiDaoJuPrefab(PlayerEnum index)
    {
        SuiJiDaoJuState type = SuiJiDaoJuState.TouZi;
        GameObject obj = null;
        //SSCaiPiaoSuiJiDaoJu suiJiDaoJu = null;
        float rv = UnityEngine.Random.Range(0, 100) / 100f;
        if (rv < m_SuiJiDaoJuData.TouZiGaiLv)
        {
            obj = m_SuiJiDaoJuData.TouZiPrefab;

            //if (obj != null)
            //{
            //    suiJiDaoJu = obj.GetComponent<SSCaiPiaoSuiJiDaoJu>();
            //    if (suiJiDaoJu != null)
            //    {
            //        suiJiDaoJu.DaoJuType = SuiJiDaoJuState.TouZi;
            //    }
            //    else
            //    {
            //        UnityLogWarning("GetSuiJiDaoJuPrefab -> suiJiDaoJu was null..........");
            //    }
            //}
        }
        else
        {
            type = SuiJiDaoJuState.BaoXiang;
            obj = m_SuiJiDaoJuData.BaoXiangPrefab;
            //if (obj != null)
            //{
            //    suiJiDaoJu = obj.GetComponent<SSCaiPiaoSuiJiDaoJu>();
            //    if (suiJiDaoJu != null)
            //    {
            //        suiJiDaoJu.DaoJuType = SuiJiDaoJuState.BaoXiang;
            //    }
            //    else
            //    {
            //        UnityLogWarning("GetSuiJiDaoJuPrefab -> suiJiDaoJu was null..........");
            //    }
            //}
        }

        m_GameCaiPiaoData.SubGameDeCaiValByDeCaiState(index, GameCaiPiaoData.DeCaiState.SuiJiDaoJu, type);
        return obj;
    }

    [System.Serializable]
    public class PlayerActiveTimeData
    {
        /// <summary>
        /// ��Ϸ����ʱ��ȼ�.
        /// </summary>
        public int TimeLevel = 0;
        /// <summary>
        /// �����˺��ٷֱ�.
        /// </summary>
        public float DamageAdd = 0f;
        public PlayerActiveTimeData(int time, float damage)
        {
            TimeLevel = time;
            DamageAdd = damage;
        }
    }
    /// <summary>
    /// ��Ҽ�����Ϸʱ���ȼ���Ϣ.
    /// m_PlayerActiveTimeData.TimeLevel�ӵ͵���.
    /// </summary>
    internal PlayerActiveTimeData[] m_PlayerActiveTimeData = new PlayerActiveTimeData[3]
    {
        new PlayerActiveTimeData(90, 0.3f),
        new PlayerActiveTimeData(120, 0.5f),
        new PlayerActiveTimeData(180, 0.8f),
    };

    /// <summary>
    /// �����ò�������Ϣ.
    /// </summary>
    [System.Serializable]
    public class ZhengChangDeCaiData
    {
        /// <summary>
        /// ��Ϸ����ʱ��.
        /// </summary>
        public float TimeVal = 0f;
        /// <summary>
        /// ���������Ʊ�ı���.
        /// </summary>
        public float DeCaiBiLi = 0f;
        public ZhengChangDeCaiData(float time, float biLi)
        {
            TimeVal = time;
            DeCaiBiLi = biLi;
        }
    }
    internal ZhengChangDeCaiData[] m_ZhengChangDeCaiData = new ZhengChangDeCaiData[3]
    {
            new ZhengChangDeCaiData(90f, 0.8f),
            new ZhengChangDeCaiData(120f, 0.9f),
            new ZhengChangDeCaiData(9999f, 1f),
    };

    /// <summary>
    /// ��ȡ��������ӵ��˺���ֵ.
    /// </summary>
    public float GetAddDamageToPlayer(PlayerEnum index)
    {
        int indexVal = (int)index - 1;
        if (indexVal < 0 || indexVal >= m_PlayerCoinData.Length)
        {
            return 0f;
        }

        float damageVal = 0f;
        float timeVal = Time.time - m_PlayerCoinData[indexVal].TimeActive;
        for (int i = m_PlayerActiveTimeData.Length - 1; i > -1; i--)
        {
            if (m_PlayerActiveTimeData[i].TimeLevel <= timeVal)
            {
                //��Ҽ�����Ϸʱ�����ڵ��ڵ�ǰ�ȼ�.
                damageVal = m_PlayerActiveTimeData[i].DamageAdd;
                break;
            }
        }
        return damageVal;
    }

    /// <summary>
    /// �������������Ϣ.
    /// </summary>
    public class PlayerCoinData
    {
        /// <summary>
        /// ��������.
        /// </summary>
        public int XuBiVal = 0;
        /// <summary>
        /// ��Ϸ����ʱ���¼.
        /// </summary>
        public float TimeActive = 0f;
        int _ZhengChangDeCai = 0;
        /// <summary>
        /// ��������²�����.
        /// </summary>
        public int ZhengChangDeCai
        {
            set
            {
                _ZhengChangDeCai = value;
            }
            get
            {
                return _ZhengChangDeCai;
            }
        }

        /// <summary>
        /// ���������ò���Ϣ.
        /// </summary>
        public void ResetZhengChangDeCai(PlayerEnum index)
        {
            int indexVal = (int)index - 1;
            if (indexVal < 0 || indexVal >= XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_PlayerCoinData.Length)
            {
                return;
            }
            
            float timeVal = Time.time - XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_PlayerCoinData[indexVal].TimeActive;
            int deCaiVal = ZhengChangDeCai;
            ZhengChangDeCaiData[] data = XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_ZhengChangDeCaiData;
            float deCaiBiLi = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (timeVal <= data[i].TimeVal)
                {
                    deCaiBiLi = data[i].DeCaiBiLi;
                    break;
                }
            }

            deCaiVal = (int)(ZhengChangDeCai * deCaiBiLi);
            //���ղ�Ʊ.
            int huiShouCaiPiao = ZhengChangDeCai - deCaiVal;
            XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_GameYuZhiCaiPiaoData.AddYuZhiCaiPiao(huiShouCaiPiao);
            ZhengChangDeCai = deCaiVal;

            if (deCaiVal > 0)
            {
                //���ʱ��Ӧ�ô�ӡ����ҵ��������ò�����.
                Debug.Log("Unity: ResetZhengChangDeCai -> index ========== " + index + ", ZhengChangDeCai ==== " + ZhengChangDeCai + ", deCaiBiLi == " + deCaiBiLi);
                //��ʾ���ʣ���Ʊ�ɾ�UI.
                SSUIRoot.GetInstance().m_GameUIManage.CreatePlayerCaiPiaoChengJiu(index, deCaiVal);
                //XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.AddCaiPiaoToPlayer(index, deCaiVal, GameCaiPiaoData.DeCaiState.ZhengChang);
            }
            ZhengChangDeCai = 0;
        }

        /// <summary>
        /// ���������ò�����.
        /// </summary>
        public void AddPlayerZhengChangDeCai(bool isPlayerXuBi)
        {
            int deCaiVal = 0;
            float xuBiChuPiaoLvTmp = 1f;
            float zhengChangChuPiaoLvTmp = 0f;
            int coinStart = XKGlobalData.GetInstance().m_CoinToCard * XKGlobalData.GameNeedCoin;
            if (isPlayerXuBi)
            {
                xuBiChuPiaoLvTmp = XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_GameCaiPiaoData.XuBiChuPiaoLv;
            }
            zhengChangChuPiaoLvTmp = XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_GameCaiPiaoData.ZhengChangChuPiaoLv;
            XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.m_GameCaiPiaoData.FenPeiDeCaiVal(isPlayerXuBi);

            deCaiVal = (int)(coinStart * xuBiChuPiaoLvTmp * zhengChangChuPiaoLvTmp);
            ZhengChangDeCai += deCaiVal;
            Debug.Log("Unity: AddPlayerZhengChangDeCai -> ZhengChangDeCai ==== " + ZhengChangDeCai + ", indexPlayer == " + IndexPlayer);
            
            if (XkGameCtrl.GetInstance().m_PlayerJiChuCaiPiaoData != null)
            {
                //��ӻ����ò�����.
                XkGameCtrl.GetInstance().m_PlayerJiChuCaiPiaoData.AddPlayerJiChuCaiPiao(IndexPlayer, deCaiVal);
            }
        }

        /// <summary>
        /// ���������ò�����.
        /// </summary>
        public void SubZhengChangDeCai(int val)
        {
            if (ZhengChangDeCai >= val)
            {
                ZhengChangDeCai -= val;
            }
            else
            {
                val = ZhengChangDeCai;
                ZhengChangDeCai = 0;
            }
            Debug.Log("Unity: SubZhengChangDeCai -> ZhengChangDeCai ================= " + ZhengChangDeCai + ", indexPlayer == " + IndexPlayer);

            if (XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage != null && val > 0)
            {
                XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_CaiPiaoDataManage.AddCaiPiaoToPlayer(IndexPlayer, val, GameCaiPiaoData.DeCaiState.ZhengChang);
            }
        }

        /// <summary>
        /// �������.
        /// </summary>
        public PlayerEnum IndexPlayer = PlayerEnum.Null;
        public PlayerCoinData(PlayerEnum index)
        {
            IndexPlayer = index;
        }
    }
    /// <summary>
    /// ���������Ϣ.
    /// </summary>
    public PlayerCoinData[] m_PlayerCoinData = new PlayerCoinData[3];

    /// <summary>
    /// ������������Ƚ���.
    /// </summary>
    int PlayerCoinDataSortByXuBiVal(PlayerCoinData x, PlayerCoinData y)//������  
    {
        if (x == null)
        {
            if (y == null)
            {
                return 0;
            }
            return 1;
        }

        if (y == null)
        {
            return -1;
        }

        int retval = y.XuBiVal.CompareTo(x.XuBiVal);
        return retval;
    }

    /// <summary>
    /// ��ȡ�����������Ϣ�����������б�.
    /// </summary>
    public PlayerCoinData[] GetSortPlayerCoinData()
    {
        List<PlayerCoinData> listDt = new List<PlayerCoinData>(m_PlayerCoinData);
        listDt.Sort(PlayerCoinDataSortByXuBiVal);
        return listDt.ToArray();
    }

    /// <summary>
    /// ��ʼ��.
    /// </summary>
    public void Init()
    {
        PlayerEnum index = PlayerEnum.Null;
        for (int i = 0; i < m_PlayerCoinData.Length; i++)
        {
            index = (PlayerEnum)(i + 1);
            m_PlayerCoinData[i] = new PlayerCoinData(index);
        }

        m_GameYuZhiCaiPiaoData.Init();
        PcvrComInputEvent.GetInstance().OnCaiPiaJiWuPiaoEvent += OnCaiPiaJiWuPiaoEvent;
        PcvrComInputEvent.GetInstance().OnCaiPiaJiChuPiaoEvent += OnCaiPiaJiChuPiaoEvent;
    }

    /// <summary>
    /// ��Ʊ����Ʊ.
    /// </summary>
    private void OnCaiPiaJiWuPiaoEvent(pcvrTXManage.CaiPiaoJi val)
    {
        int indexVal = (int)val;
        if (indexVal < 0 || indexVal > 2)
        {
            Debug.LogWarning("OnCaiPiaJiWuPiaoEvent -> indexVal was wrong! indexVal ==== " + indexVal);
            return;
        }

        PlayerEnum indexPlayer = (PlayerEnum)(indexVal + 1);
        if (SSUIRoot.GetInstance().m_GameUIManage != null)
        {
            SSUIRoot.GetInstance().m_GameUIManage.CreatCaiPiaoBuZuPanel(indexPlayer);
        }
    }

    /// <summary>
    /// ��Ʊ����Ʊ.
    /// </summary>
    private void OnCaiPiaJiChuPiaoEvent(pcvrTXManage.CaiPiaoJi val)
    {
        int indexVal = (int)val;
        if (indexVal < 0 || indexVal > 2)
        {
            Debug.LogWarning("OnCaiPiaJiChuPiaoEvent -> indexVal was wrong! indexVal ==== " + indexVal);
            return;
        }

        PlayerEnum indexPlayer = (PlayerEnum)(indexVal + 1);
        SubPlayerCaiPiao(indexPlayer, 1);
    }

    /// <summary>
    /// ���������Ϸ����ʱ����Ϣ.
    /// </summary>
    public void SetPlayerCoinTimeActive(PlayerEnum index)
    {
        int indexVal = (int)index - 1;
        if (indexVal < 0 || indexVal >= m_PlayerCoinData.Length)
        {
            return;
        }
        m_PlayerCoinData[indexVal].TimeActive = Time.time;
        Debug.Log("Unity: SetPlayerCoinTimeActive -> index == " + index + ", time == " + Time.time);
    }

    /// <summary>
    /// ��������������.
    /// </summary>
    public void AddPlayerXuBiVal(PlayerEnum index)
    {
        int indexVal = (int)index - 1;
        if (indexVal < 0 || indexVal >= m_PlayerCoinData.Length)
        {
            return;
        }

        //��ҽ��������Ҽ�����Ϸ����.
        //����JPBoss���������״̬.
        XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_JPBossRulerData.IsPlayerXuBi = true;

        int coinStart = XKGlobalData.GameNeedCoin;
        m_PlayerCoinData[indexVal].XuBiVal += coinStart;
        Debug.Log("Unity: AddPlayerXuBiVal -> index == " + index + ", coinVal ==== " + m_PlayerCoinData[indexVal].XuBiVal);
    }

    /// <summary>
    /// �����������ò�����.
    /// </summary>
    public void AddPlayerZhengChangDeCai(PlayerEnum index, bool isPlayerXuBi)
    {
        int indexVal = (int)index - 1;
        if (indexVal < 0 || indexVal >= m_PlayerCoinData.Length)
        {
            return;
        }

        m_PlayerCoinData[indexVal].AddPlayerZhengChangDeCai(isPlayerXuBi);
    }

    /// <summary>
    /// ������������ò�����.
    /// </summary>
    public void SubPlayerZhengChangDeCai(PlayerEnum indexPlayer, int val)
    {
        int indexVal = (int)indexPlayer - 1;
        if (indexVal < 0 || indexVal >= m_PlayerCoinData.Length)
        {
            return;
        }

        m_PlayerCoinData[indexVal].SubZhengChangDeCai(val);
    }


    /// <summary>
    /// ���������������.
    /// </summary>
    public void ResetPlayerXuBiInfo(PlayerEnum index)
    {
        int indexVal = (int)index - 1;
        if (indexVal < 0 || indexVal >= m_PlayerCoinData.Length)
        {
            return;
        }
        m_PlayerCoinData[indexVal].XuBiVal = 0;
        Debug.Log("Unity: ResetPlayerXuBiInfo -> index == " + index + ", coinVal ==== " + m_PlayerCoinData[indexVal].XuBiVal);

        //���������ò�����.
        m_PlayerCoinData[indexVal].ResetZhengChangDeCai(index);
    }

    /// <summary>
    /// SuperJPBoss��Ʊ����.
    /// </summary>
    [System.Serializable]
    public class SuperJPBossCaiPiaoData
    {
        /// <summary>
        /// ��Ʊ���ʻ���.
        /// </summary>
        public int CaiPiaoBeiLvJiShu = 25;
        /// <summary>
        /// ��������.
        /// </summary>
        public int BaoCaiTiaoJian = 125;
        /// <summary>
        /// ��������.
        /// </summary>
        public int BaoCaiShuLiang = 150;
        public SuperJPBossCaiPiaoData(int jiShu, int tiaoJian, int shuLiang)
        {
            CaiPiaoBeiLvJiShu = jiShu;
            BaoCaiTiaoJian = tiaoJian;
            BaoCaiShuLiang = shuLiang;
        }
        /// <summary>
        /// ��Ʊ��������.
        /// </summary>
        int CaiPiaoJiShuCount = 1;
        int _SuperJPCaiPiao = 0;
        /// <summary>
        /// ����JPBoss��Ʊ��.
        /// </summary>
        public int SuperJPCaiPiao
        {
            get
            {
                return _SuperJPCaiPiao;
            }
            set
            {
                _SuperJPCaiPiao = value;
                int coinToCaiPiao = XKGlobalData.GetInstance().m_CoinToCard;
                int caiPiaoVal = CaiPiaoJiShuCount * CaiPiaoBeiLvJiShu * coinToCaiPiao;
                //Debug.Log("Unity: SuperJPCaiPiao =============== " + SuperJPCaiPiao
                //    + ", CaiPiaoJiShuCount == " + CaiPiaoJiShuCount
                //    + ", CaiPiaoBeiLvJiShu == " + CaiPiaoBeiLvJiShu
                //    + ", coinToCaiPiao == " + coinToCaiPiao
                //    + ", caiPiaoVal == " + caiPiaoVal);
                if (value >= caiPiaoVal)
                {
                    //���Բ���SuperJPBoss��.
                    CaiPiaoJiShuCount = (value / (CaiPiaoBeiLvJiShu * coinToCaiPiao)) + 1;
#if CREATE_SUPER_JPBOSS
                    if (XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_ZhanCheJPBossData.IsCreatSuperJPBoss == false)
                    {
                        Debug.Log("Unity: game can create superJPBoss...................");
                        XkPlayerCtrl.GetInstanceFeiJi().m_SpawnNpcManage.m_ZhanCheJPBossData.IsCreatSuperJPBoss = true;
                    }
#endif
                }
            }
        }
    }
    /// <summary>
    /// SuperJPBoss��Ʊ����.
    /// </summary>
    internal SuperJPBossCaiPiaoData m_SuperJPBossCaiPiaoData = new SuperJPBossCaiPiaoData(25, 125, 150);

    /// <summary>
    /// ��Ϸ��Ҫ��ӡ����ҵĲ�Ʊ������Ϣ.
    /// </summary>
    public class PcvrPrintCaiPiaoData
    {
        PlayerEnum IndexPlayer;
        int _CaiPiaoVal = 0;
        /// <summary>
        /// ��Ʊ����.
        /// </summary>
        internal int CaiPiaoVal
        {
            set
            {
                bool isShowCaiPiaoNum = true;
                if (_CaiPiaoVal <= 0 && CaiPiaoValCache <= 0)
                {
                    isShowCaiPiaoNum = false;
                }

                _CaiPiaoVal = value;
                if (SSUIRoot.GetInstance().m_GameUIManage != null)
                {
                    //��ʾ��Ҳ�Ʊ����.
                    SSUIRoot.GetInstance().m_GameUIManage.ShowPlayerCaiPiaoInfo(IndexPlayer, _CaiPiaoVal + CaiPiaoValCache, false, isShowCaiPiaoNum);
                }

                if (IsDaJiangCaiPiao == true && _CaiPiaoVal + CaiPiaoValCache <= 0)
                {
                    IsDaJiangCaiPiao = false;
                    //ɾ����Ʊ��UI����.
                    if (SSUIRoot.GetInstance().m_GameUIManage != null)
                    {
                        SSUIRoot.GetInstance().m_GameUIManage.RemoveCaiPiaoDaJiangPanel();
                    }
                }
            }
            get
            {
                return _CaiPiaoVal;
            }
        }

        /// <summary>
        /// �Ƿ�õ�JPBoss��.
        /// </summary>
        internal bool IsDaJiangCaiPiao = false;

        /// <summary>
        /// �Ƿ����ڴ�ӡ��Ʊ.
        /// </summary>
        internal bool IsPrintCaiPiao = false;
        /// <summary>
        /// ��Ʊ���ݻ�����Ϣ.
        /// ����Ʊ�����ڴ�ӡ��Ʊʱ,�¼ӽ����Ĳ�Ʊ����ʱ���뻺����������,�Ȳ�Ʊ����ӡ�굱ǰ
        /// ��Ʊ��,��ȥ��黺�����Ĳ�Ʊ��,�������������ӡ��������Ʊ������������Ʊ���,û��
        /// �����򲻽����κβ���.
        /// </summary>
        internal int CaiPiaoValCache = 0;

        /// <summary>
        /// �����Ʊ����.
        /// </summary>
        public void ClearCaiPiaoData()
        {
            IsPrintCaiPiao = false;
            CaiPiaoValCache = 0;
            CaiPiaoVal = 0;
        }

        public PcvrPrintCaiPiaoData(PlayerEnum indexPlayerVal)
        {
            IndexPlayer = indexPlayerVal;
        }
    }
    /// <summary>
    /// 3����ҵĲ�Ʊ������Ϣ.
    /// </summary>
    internal PcvrPrintCaiPiaoData[] m_PcvrPrintCaiPiaoData = new PcvrPrintCaiPiaoData[3]
    {
        new PcvrPrintCaiPiaoData(PlayerEnum.PlayerOne),
        new PcvrPrintCaiPiaoData(PlayerEnum.PlayerTwo),
        new PcvrPrintCaiPiaoData(PlayerEnum.PlayerThree),
    };

    /// <summary>
    /// ��Ӳ�Ʊ�����.
    /// </summary>
    internal void AddCaiPiaoToPlayer(PlayerEnum indexPlayer, int caiPiao, GameCaiPiaoData.DeCaiState type, bool isPlayCaiPiaoNumAni = true)
    {
        int index = (int)indexPlayer - 1;
        if (index < 0 || index > 2)
        {
            UnityLogWarning("AddCaiPiaoToPlayer -> index was wrong! index ==== " + index);
            return;
        }

        //test
        //if (type != GameCaiPiaoData.DeCaiState.ZhanChe)
        //{
        //    return;
        //}
        //test

        if (type == GameCaiPiaoData.DeCaiState.JPBoss)
        {
            if (m_PcvrPrintCaiPiaoData[index].IsDaJiangCaiPiao == false)
            {
                m_PcvrPrintCaiPiaoData[index].IsDaJiangCaiPiao = true;
                //������Ʊ��UI����.
                if (SSUIRoot.GetInstance().m_GameUIManage != null)
                {
                    SSUIRoot.GetInstance().m_GameUIManage.CreatCaiPiaoDaJiangPanel(indexPlayer, caiPiao);
                }
            }
        }

        XKGlobalData.GetInstance().SetTotalOutPrintCards(XKGlobalData.GetInstance().m_TotalOutPrintCards + caiPiao);
        if (m_PcvrPrintCaiPiaoData[index].IsPrintCaiPiao)
        {
            //��ǰ��λ���ڴ�ӡ��Ʊ.
            //���µõ��Ĳ�Ʊ���뻺����.
            m_PcvrPrintCaiPiaoData[index].CaiPiaoValCache += caiPiao;
            if (isPlayCaiPiaoNumAni == true)
            {
                //�в��Ų�Ʊ���ֶ���,�ȶ��������ڸ��²�Ʊ����UI.
            }
            else
            {
                //û�в��Ų�Ʊ���ֶ���,ֱ�Ӹ��²�Ʊ����UI.
                if (SSUIRoot.GetInstance().m_GameUIManage != null)
                {
                    //��ʾ��Ҳ�Ʊ����.
                    SSUIRoot.GetInstance().m_GameUIManage.ShowPlayerCaiPiaoInfo(indexPlayer,
                        m_PcvrPrintCaiPiaoData[index].CaiPiaoVal + m_PcvrPrintCaiPiaoData[index].CaiPiaoValCache, false, true);
                }
            }
            return;
        }

        m_PcvrPrintCaiPiaoData[index].IsPrintCaiPiao = true;
        m_PcvrPrintCaiPiaoData[index].CaiPiaoVal += caiPiao;

        //if (isPlayCaiPiaoNumAni == true)
        //{
        //    //�в��Ų�Ʊ���ֶ���,�ȶ��������ڸ��²�Ʊ����UI.
        //}
        //else
        //{
        //    //û�в��Ų�Ʊ���ֶ���,ֱ�Ӹ��²�Ʊ����UI.
        //    if (SSUIRoot.GetInstance().m_GameUIManage != null)
        //    {
        //        //��ʾ��Ҳ�Ʊ����.
        //        SSUIRoot.GetInstance().m_GameUIManage.ShowPlayerCaiPiaoInfo(indexPlayer,
        //            m_PcvrPrintCaiPiaoData[index].CaiPiaoVal + m_PcvrPrintCaiPiaoData[index].CaiPiaoValCache);
        //    }
        //}

        Debug.Log("AddCaiPiaoToPlayer ->CaiPiaoVal ===== " + m_PcvrPrintCaiPiaoData[index].CaiPiaoVal
            + ", addCaiPiao ====== " + caiPiao
            + ", coinToCaiPiao ==== " + XKGlobalData.GetInstance().m_CoinToCard);

        //�������pcvr��ӡ��Ʊ����Ϣ.
        StartCoroutine(DelayPrintPlayerCaiPiao(indexPlayer, caiPiao, type));
        //pcvr.GetInstance().StartPrintPlayerCaiPiao(indexPlayer, caiPiao);
    }

    /// <summary>
    /// �ӳٴ�ӡ��Ҳ�Ʊ.
    /// </summary>
    IEnumerator DelayPrintPlayerCaiPiao(PlayerEnum indexPlayer, int caiPiao, GameCaiPiaoData.DeCaiState type)
    {
        switch (type)
        {
            case GameCaiPiaoData.DeCaiState.JPBoss:
            case GameCaiPiaoData.DeCaiState.ZhanChe:
                {
                    yield return new WaitForSeconds(3.3f);
                    break;
                }
            default:
                {
                    yield return new WaitForSeconds(1.3f);
                    break;
                }
        }
        //�������pcvr��ӡ��Ʊ����Ϣ.
        pcvr.GetInstance().StartPrintPlayerCaiPiao(indexPlayer, caiPiao);
    }

    /// <summary>
    /// ��ȡ����Ƿ��в�Ʊû�д�ӡ��.
    /// </summary>
    internal bool GetAllPlayerIsHaveCaiPiao()
    {
        if (XKGlobalData.IsFreeMode == true)
        {
            //���ģʽ.
            return false;
        }

        bool isHaveCaiPiao = false;
        int length = m_PcvrPrintCaiPiaoData.Length;
        for (int i = 0; i < length; i++)
        {
            if (m_PcvrPrintCaiPiaoData[i].CaiPiaoVal > 0 || m_PcvrPrintCaiPiaoData[i].CaiPiaoValCache > 0)
            {
                isHaveCaiPiao = true;
                break;
            }
        }
        return isHaveCaiPiao;
    }

    /// <summary>
    /// ��ʼ��ӡ��������Ʊ.
    /// </summary>
    void StartPrintCaiPiaoCache(PlayerEnum indexPlayer)
    {
        int index = (int)indexPlayer - 1;
        if (index < 0 || index > 2)
        {
            UnityLogWarning("AddCaiPiaoToPlayer -> index was wrong! index ==== " + index);
            return;
        }

        if (m_PcvrPrintCaiPiaoData[index].IsPrintCaiPiao)
        {
            //��ǰ��λ���ڴ�ӡ��Ʊ.
            return;
        }
        m_PcvrPrintCaiPiaoData[index].IsPrintCaiPiao = true;

        int caiPiao = m_PcvrPrintCaiPiaoData[index].CaiPiaoValCache;
        //��ջ�������Ʊ����,����UI��ʾ��Ʊ������ʾΪ��������Ʊ����2��.
        m_PcvrPrintCaiPiaoData[index].CaiPiaoValCache = 0;
        //���ò�Ʊ������Ϣ.
        m_PcvrPrintCaiPiaoData[index].CaiPiaoVal = caiPiao;
        //UnityLog("StartPrintCaiPiaoCache -> indexPlayer ====== " + indexPlayer + ", caiPiao ==== " + caiPiao);

        //�������pcvr��ӡ��Ʊ����Ϣ.
        pcvr.GetInstance().StartPrintPlayerCaiPiao(indexPlayer, caiPiao);
    }
    
    /// <summary>
    ///  ������Ҳ�Ʊ.
    /// </summary>
    internal void SubPlayerCaiPiao(PlayerEnum indexPlayer, int caiPiao)
    {
        int index = (int)indexPlayer - 1;
        if (index < 0 || index > 2)
        {
            UnityLogWarning("SubPlayerCaiPiao -> index was wrong! index ==== " + index);
            return;
        }

        if (m_PcvrPrintCaiPiaoData[index].CaiPiaoVal >= caiPiao)
        {
            m_PcvrPrintCaiPiaoData[index].CaiPiaoVal -= caiPiao;
        }
        else
        {
            m_PcvrPrintCaiPiaoData[index].CaiPiaoVal = 0;
        }

        if (m_PcvrPrintCaiPiaoData[index].CaiPiaoVal <= 0)
        {
            //��Ʊ�Ѿ���ӡ���.
            m_PcvrPrintCaiPiaoData[index].IsPrintCaiPiao = false;
            if (m_PcvrPrintCaiPiaoData[index].CaiPiaoValCache > 0)
            {
                //��ʼ��ӡ��������Ʊ.
                StartPrintCaiPiaoCache(indexPlayer);
            }
        }
        //Debug.Log("SubPlayerCaiPiao ->CaiPiaoVal ===== " + m_PcvrPrintCaiPiaoData[index].CaiPiaoVal
        //    + ", CaiPiaoValCache ==== " + m_PcvrPrintCaiPiaoData[index].CaiPiaoValCache
        //    + ", subCaiPiao ====== " + caiPiao);

        if (SSUIRoot.GetInstance().m_GameUIManage != null)
        {
            //ɾ����Ʊ����UI����.
            SSUIRoot.GetInstance().m_GameUIManage.RemoveCaiPiaoBuZuPanel(indexPlayer, false);
        }
    }

    /// <summary>
    /// ��ȡ��ҵ�ǰ�Ĳ�Ʊ����.
    /// </summary>
    internal int GetPlayerCaiPiaoVal(PlayerEnum indexPlayer)
    {
        int indexVal = (int)indexPlayer;
        if (indexVal < 1 || indexVal > 3)
        {
            Debug.LogWarning("GetPlayerCaiPiaoVal -> indexVal was wrong! indexVal ==== " + indexVal);
            return 0;
        }

        if (m_PcvrPrintCaiPiaoData.Length < indexVal)
        {
            Debug.LogWarning("GetPlayerCaiPiaoVal -> m_PcvrPrintCaiPiaoData was wrong! indexVal ==== " + indexVal);
            return 0;
        }
        return m_PcvrPrintCaiPiaoData[indexVal - 1].CaiPiaoVal;
    }

    /// <summary>
    /// ������Ա�����Ʊ�����̨��Ʊ����.
    /// ������Ҳ�Ʊ������Ϣ.
    /// </summary>
    internal void ClearPlayerCaiPiaoData(PlayerEnum indexPlayer)
    {
        int index = (int)indexPlayer - 1;
        if (index < 0 || index > 2)
        {
            UnityLogWarning("ClearPlayerCaiPiaoData -> index was wrong! index ==== " + index);
            return;
        }
        UnityLog("ClearPlayerCaiPiaoData -> indexPlayer ======= " + indexPlayer);
        //������Ҳ�Ʊ����.
        m_PcvrPrintCaiPiaoData[index].ClearCaiPiaoData();
        //�������pcvr��Ʊ����.
        pcvr.GetInstance().ClearCaiPiaoData(indexPlayer);
    }

#if TEST_OUT_PRINT_CARD && UNITY_EDITOR
    void Update()
    {
        if (IsTestPrintCard == false)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            //����Ʊ.
            TestAddCard();
        }
        TestUpdataPrintCard();
    }

    float TestLastPrintCardTime = 0f;
    int TestCardNum = 0;
    public bool IsTestPrintCard = true;
    void TestAddCard()
    {
        if (TestCardNum <= 0)
        {
            TestCardNum = 10;
        }
    }

    void TestUpdataPrintCard()
    {
        if (TestCardNum <= 0)
        {
            //û�в�Ʊ��.
            return;
        }

        if (m_PcvrPrintCaiPiaoData[0].CaiPiaoVal <= 0)
        {
            return;
        }

        if (Time.time - TestLastPrintCardTime >= 0.5f)
        {
            TestLastPrintCardTime = Time.time;
            PcvrComInputEvent.GetInstance().OnCaiPiaJiChuPiao(pcvrTXManage.CaiPiaoJi.Num01);
            TestCardNum--;
            if (TestCardNum <= 0)
            {
                //û�в�Ʊ��.
                PcvrComInputEvent.GetInstance().OnCaiPiaJiWuPiao(pcvrTXManage.CaiPiaoJi.Num01);
            }
        }
    }
#endif

#if TEST_SHOW_PLAYER_CAIPIAO
    private void OnGUI()
    {
        string info = "CaiPiaoP1: " + m_PcvrPrintCaiPiaoData[0].CaiPiaoVal + ", CaiPiaoCacheP1: " + m_PcvrPrintCaiPiaoData[0].CaiPiaoValCache
            + ", CaiPiaoP2: " + m_PcvrPrintCaiPiaoData[1].CaiPiaoVal + ", CaiPiaoCacheP2: " + m_PcvrPrintCaiPiaoData[1].CaiPiaoValCache
            + ", CaiPiaoP3: " + m_PcvrPrintCaiPiaoData[2].CaiPiaoVal + ", CaiPiaoCacheP3: " + m_PcvrPrintCaiPiaoData[2].CaiPiaoValCache;
        Rect rectVal = new Rect(15f, 15f, Screen.width - 30f, 25f);
        GUI.Box(rectVal, "");
        GUI.Label(rectVal, info);
        
        info = "CaiPiJiP1: ";
        byte[] buffer = MyCOMDevice.ComThreadClass.ReadByteMsg;
        //UpdateCaiPiaoJiInfo(buffer[44], buffer[15], buffer[16]);
        pcvrTXManage.CaiPiaoPrintState type = (pcvrTXManage.CaiPiaoPrintState)buffer[44];
        switch (type)
        {
            case pcvrTXManage.CaiPiaoPrintState.WuXiao:
                {
                    info += "��Ч";
                    break;
                }
            case pcvrTXManage.CaiPiaoPrintState.Failed:
                {
                    info += "ʧ��";
                    break;
                }
            case pcvrTXManage.CaiPiaoPrintState.Succeed:
                {
                    info += "�ɹ�";
                    break;
                }
        }
        
        info += ", CaiPiJiP2: ";
        type = (pcvrTXManage.CaiPiaoPrintState)buffer[15];
        switch (type)
        {
            case pcvrTXManage.CaiPiaoPrintState.WuXiao:
                {
                    info += "��Ч";
                    break;
                }
            case pcvrTXManage.CaiPiaoPrintState.Failed:
                {
                    info += "ʧ��";
                    break;
                }
            case pcvrTXManage.CaiPiaoPrintState.Succeed:
                {
                    info += "�ɹ�";
                    break;
                }
        }

        info += ", CaiPiJiP3: ";
        type = (pcvrTXManage.CaiPiaoPrintState)buffer[16];
        switch (type)
        {
            case pcvrTXManage.CaiPiaoPrintState.WuXiao:
                {
                    info += "��Ч";
                    break;
                }
            case pcvrTXManage.CaiPiaoPrintState.Failed:
                {
                    info += "ʧ��";
                    break;
                }
            case pcvrTXManage.CaiPiaoPrintState.Succeed:
                {
                    info += "�ɹ�";
                    break;
                }
        }
        rectVal = new Rect(15f, 45f, Screen.width - 30f, 25f);
        GUI.Box(rectVal, "");
        GUI.Label(rectVal, info);

        info = "PcvrCaiPiaoP1: " + pcvr.GetInstance().mPcvrTXManage.CaiPiaoCountPrint[0].ToString()
            + ", PcvrCaiPiaoP2: " + pcvr.GetInstance().mPcvrTXManage.CaiPiaoCountPrint[1].ToString()
            + ", PcvrCaiPiaoP3: " + pcvr.GetInstance().mPcvrTXManage.CaiPiaoCountPrint[2].ToString();
        rectVal = new Rect(15f, 75f, Screen.width - 30f, 25f);
        GUI.Box(rectVal, "");
        GUI.Label(rectVal, info);

        info = "PcvrCaiPiaoPrintFailedP1: " + pcvr.GetInstance().mPcvrTXManage.CaiPiaoPrintFailedCount[0].ToString()
            + ", PcvrCaiPiaoPrintFailedP2: " + pcvr.GetInstance().mPcvrTXManage.CaiPiaoPrintFailedCount[1].ToString()
            + ", PcvrCaiPiaoPrintFailedP3: " + pcvr.GetInstance().mPcvrTXManage.CaiPiaoPrintFailedCount[2].ToString();
        rectVal = new Rect(15f, 105f, Screen.width - 30f, 25f);
        GUI.Box(rectVal, "");
        GUI.Label(rectVal, info);
    }
#endif
}