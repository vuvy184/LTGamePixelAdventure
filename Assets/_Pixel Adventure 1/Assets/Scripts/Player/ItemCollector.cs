using System;
using System.Collections;
using System.Collections.Generic;
using GameTool;
using TMPro;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;
    [SerializeField] private TextMeshProUGUI itemText;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item")) // kiểm tra đối tượng va chạm có tag là Item không
        {
            other.gameObject.SetActive(false); // tắt đối tượng va chạm khiến nó không còn được hiển thị hay tương tác trong cảnh nữa 
            ++cherries;
            itemText.text = "SCORE: " + cherries.ToString(); // cập nhật nội dung TextMesh
            PoolingManager.Instance.GetObject(NamePrefabPool.CollectVFX, null, other.transform.position).Disable(0.75f);
            AudioManager.Instance.Shot(eSoundName.Collect_Item); // âm thanh khi ăn vật phẩm
        }
       
    }
}