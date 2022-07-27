using System;
using LiteNinja.UI.Hud;
using UnityEngine;


namespace Mizz.UI.Huds
{
    public class MainMenuHudView : AHud
    {
        [SerializeField] private GameObject _mainmenuCanvas;
        
        public Action ClickPlay;
        
        
        protected override void OnEnable()
        {
            SetActiveMenus(true);
        }

        protected override void OnDisable()
        {
            SetActiveMenus(false);
        }
        
        public void SetActiveMenus(bool value)
        {
            _mainmenuCanvas.SetActive(value);
        }
        
        public void OnClickPlay()
        {
            ClickPlay?.Invoke();
        }
    }
}
