using frameworks.utils;
using System.Collections.Generic;
using UnityEngine;

namespace frameworks.ui
{
    public class ScreenManager : Singleton<ScreenManager>
    {
        [SerializeField] private UIScreen _DefaultScreen;
        [SerializeField] private UIScreen[] _Screens;

        private Dictionary<string, UIScreen> mScreenMaps;
        private UIScreen mCurrentScreen;

        protected override void Awake()
        {
            base.Awake();
            InitialiseScreenMaps();
        }

        private void Start()
        {
            if (_DefaultScreen != null)
                ShowScreen(_DefaultScreen.ScreenID);
        }

        private void InitialiseScreenMaps()
        {
            mScreenMaps = new Dictionary<string, UIScreen>();
            foreach (UIScreen screen in _Screens)
            {
                screen.Init();
                screen.Hide();
                mScreenMaps.Add(screen.ScreenID, screen);
            }
        }

        public void ShowScreen(string screenID)
        {
            if (mCurrentScreen != null)
                mCurrentScreen.Hide();

            if (mScreenMaps.ContainsKey(screenID))
            {
                mCurrentScreen = mScreenMaps[screenID];
                mCurrentScreen.Show();
            }
        }
    }
}