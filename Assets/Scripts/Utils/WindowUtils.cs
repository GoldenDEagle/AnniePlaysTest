﻿using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class WindowUtils
    {
        public static void CreateWindow(string resourcePath)
        {
            var window = Resources.Load<GameObject>(resourcePath);
            var canvas = GameObject.FindObjectOfType<Canvas>();
            Object.Instantiate(window, canvas.transform);
        }
    }
}
