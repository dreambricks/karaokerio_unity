using UnityEngine;

public class CtaWindow : MonoBehaviour
{

    [SerializeField] private UDPReceiver udp;
    [SerializeField] private GameObject countDown;


    private void Update()
    {
        VerifyData();
    }

    private void VerifyData()
    {
        string[] messages = udp.GetLastestData().Split(",");
        if (messages[0] == "start") { 
            countDown.SetActive(true);
            SaveLog();
            gameObject.SetActive(false);
        }
    }

    void SaveLog()
    {
        DataLog dataLog = LogUtil.GetDatalogFromJson();
        dataLog.status = StatusEnum.CANTOU.ToString();
        dataLog.additional = "vazio";
        LogUtil.SaveLog(dataLog);
    }

}
