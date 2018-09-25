using System.Collections;
using System.Collections.Generic;
using ConfigDatas;
using UnityEngine;
using UnityEngine.UI;

public class BagPanel : MonoBehaviour
{
    public GameObject cellType;
    public Transform[] grids;

    private Button closeBtn;

    public Transform GetEmptyGrid()
    {
        for (int i = 0; i < grids.Length; i++)
        {
            if (grids[i].childCount == 0)
            {
                return grids[i];
            }
        }

        return null;
    }

    // Use this for initialization
    void Start ()
    {
        var childCell = transform.Find("ScrollPanel/BagCells");
        foreach (var hItemConfig in ConfigData.HItemDict)
        {
            Debug.Log(hItemConfig.Value.Name);
            var appleGO = Instantiate(cellType, new Vector3(0, 0, 0), childCell.localRotation);
            //   appleGO.transform.SetParent(gameLayer.transform);
            //   appleGO.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            var bagItem = appleGO.GetComponent<BagItem>();
            bagItem.UpdateText(hItemConfig.Value.Name);
            bagItem.UpdateImage(hItemConfig.Value.Url);
            appleGO.transform.SetParent(childCell);
            appleGO.transform.localScale = Vector3.one;
        }
        closeBtn = transform.Find("ButtonClose").GetComponent<Button>();
        closeBtn.onClick.AddListener(OnCloseClick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCloseClick()
    {
        PanelContainer.Instance.Remove(transform);
    }
}
