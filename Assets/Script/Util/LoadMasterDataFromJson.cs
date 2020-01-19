using UnityEngine;

/// <summary>
/// 42体分のキンのデータ(info)をJsonファイルからUnity用のデータに読み込んでくれる
/// </summary>
public class LoadMasterDataFromJson : MonoBehaviour
{

    public KinSO kinSO;

    //Awakeで、保存されているMasterDataを読み込みに行く(けど、今は未使用)
    private void Awake()
    {
        if (!kinSO)
        {
            kinSO = Resources.Load<KinSO>("MasterData/KinMasterData");
        }
    }


    /// <summary>
    /// Jsonファイルを読み込んで、それをKinMasterDataに書き込む
    /// </summary>
    public void LoadFromJson()
    {
        kinSO.kinMasterData = new kinMasterData();
        kinSO.kinMasterData = JsonUtility.FromJson<kinMasterData>(JsonHelper.GetJsonFile("/", "ZukanInfo.Json"));
    }


}
