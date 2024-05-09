using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace TaskArcher.UI
{
    public class TabGroup : MonoBehaviour
    {
        [SerializeField] private List<TabButton> tabButtons;
        
        [SerializeField] private Color tabIdleColor;
        [SerializeField] private Color tabHoverColor;
        [SerializeField] private Color tabActiveColor;
        
        [SerializeField] private Color iconIdleColor;
        [SerializeField] private Color iconActiveColor;
        
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private TabButton selectedTab;
        
        public UnityEvent onSelectedTabChanged = new();

        public List<TabButton> TabButtons => tabButtons;
        
        public TabButton SelectedTab
        {
            get => selectedTab;

            set => selectedTab = value;
        }

        private void Start()
        {
            SelectDefault();
        }

        public void Subscribe(TabButton button)
        {
            tabButtons ??= new List<TabButton>();

            tabButtons.Add(button);
        }

        public void OnTabEnter(TabButton button)
        {
            ResetTabs();
            if (selectedTab == null || button != selectedTab)
            {
                button.background.color = tabHoverColor;
            }
        }

        public void OnTabExit(TabButton button)
        {
            ResetTabs();
        }

        public void OnTabSelected(TabButton button)
        {
            selectedTab = button;
            
            ResetTabs();
            button.background.color = tabActiveColor;
            
            if (button.icon != null) 
            {
                button.icon.color = iconActiveColor;
            }
            
            onSelectedTabChanged?.Invoke();
        }
        
        public void SelectDefault()
        {
            selectedTab = tabButtons[0];
            
            ResetTabs();
            selectedTab.background.color = tabActiveColor;
            selectedTab.icon.color = iconActiveColor;
            
            onSelectedTabChanged?.Invoke();
        }

        private void ResetTabs()
        {
            foreach (TabButton button in tabButtons.Where(button => selectedTab == null || button != selectedTab)) 
            {
                button.background.color = tabIdleColor;
                
                if (button.icon != null) 
                {
                    button.icon.color = iconIdleColor;
                }
            }
        }

        public void ToggleCanvasGroup(bool value)
        {
            canvasGroup.alpha = value ? 1 : 0;
            canvasGroup.interactable = value;
            canvasGroup.blocksRaycasts = value;
        }
    }
}
