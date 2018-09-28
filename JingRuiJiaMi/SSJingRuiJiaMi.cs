using UnityEngine;
using System.Collections;

public class SSJingRuiJiaMi : MonoBehaviour
{
	/// <summary>
	/// �Ƿ����������У��.
	/// </summary>
	public static bool IsOpenJingRuiJiaMi = true;
	static SSJingRuiJiaMi _Instance;
	public static SSJingRuiJiaMi GetInstance()
	{
		return _Instance;
	}

	/// <summary>
	/// ���ܼ����UI.
	/// </summary>
	public GameObject m_JiaMiJianCeUI;
	GameObject m_JiaMiJianCeObj;
	void Start()
	{
		_Instance = this;
        if (IsOpenJingRuiJiaMi == true && XKGameVersionCtrl.IsInit == false)
        {
            XKGameVersionCtrl gmVersionCom = gameObject.AddComponent<XKGameVersionCtrl>();
            if (gmVersionCom != null)
            {
                gmVersionCom.Init();
            }
        }

        bool isJiaoYanJingRui = false;
        if (IsOpenJingRuiJiaMi)
        {
            int val = PlayerPrefs.GetInt("JiaoYanJingRui");
            if (val == 0)
            {
                isJiaoYanJingRui = true;
            }
            else
            {
                //�Զ�������Ϸʱ�����Զ���JiaoYanJingRui����Ϊ1,�����ظ�У����ܹ�.
            }
            PlayerPrefs.SetInt("JiaoYanJingRui", 0);
        }

        if (IsOpenJingRuiJiaMi && isJiaoYanJingRui)
		{
			CreatJiaMiJianCeUI();
			StartJingRuiJiaMi();
		}
		else
		{
			LoadGame();
		}
	}

	/// <summary>
	/// �����ľ������У��,ֻ��Ҫһ�γɹ�����,����У��ʱ����Ϊ�ǳɹ���.
	/// </summary>
	void StartJingRuiJiaMi()
	{
		StartCoroutine(DelayJingRuiJiaMiJiaoYan());
	}
	
	IEnumerator DelayJingRuiJiaMiJiaoYan()
	{
		Debug.Log("Start JingRui JiaMi test...");
		yield return new WaitForSeconds(5f);
		GameRoot.StartInitialization();
		StandbyProcess sp = new StandbyProcess();
		sp.Initialization();
	}

	/// <summary>
	/// �������ܼ��UI.
	/// </summary>
	void CreatJiaMiJianCeUI()
	{
		if (m_JiaMiJianCeUI != null)
		{
			m_JiaMiJianCeObj = (GameObject)Instantiate(m_JiaMiJianCeUI);
		}
	}

	/// <summary>
	/// ɾ�����ܼ��UI.
	/// </summary>
	public void RemoveJiaMiJianCeUI()
	{
		if (m_JiaMiJianCeObj != null)
		{
			Destroy(m_JiaMiJianCeObj);
		}
	}

	/// <summary>
	/// ������Ϸ����.
	/// </summary>
	public void LoadGame()
	{
		Application.LoadLevel(1);
	}

    /// <summary>
    /// ��Ϸ���������һ�ξ���4����У��.
    /// </summary>
	public static void OnGameOverCheckJingRuiJiaMi()
	{
		if (!IsOpenJingRuiJiaMi)
		{
			return;
		}
		
		//�������������һ����ȫ��ȫ��֤
		//�����֤ʧ������Ϸ�ͻᱻ����
		//��У��
		GameRoot.CheckCipherText(StandbyProcess.VerifyEnvironmentKey_LogoVideo);
	}
}