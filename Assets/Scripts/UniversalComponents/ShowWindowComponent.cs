using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.UniversalComponents
{
    public class ShowWindowComponent : MonoBehaviour
    {
        [SerializeField] private string _path;

        public void Show()
        {
            WindowUtils.CreateWindow(_path);
        }
    }
}