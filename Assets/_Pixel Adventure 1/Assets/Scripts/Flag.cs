using System;
using System.Collections;
using System.Collections.Generic;
using GameTool;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player") // kiểm tra xem đối tượng va chạm có phải player không
        {
            CanvasManager.Instance.Push(eUIName.WinPopup); // hiển thị một UI  có tên WinPopup bằng cách dùng CanvasManager
            // CanvasManager là một lớp để quản lý các thành phần UI như hộp thoại, nút bấm, văn bản
        }
    }
}
