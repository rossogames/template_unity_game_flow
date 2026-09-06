using Rossoforge.Toolbar.DataTool;
using UnityEditor.Toolbars;
using UnityEngine;

namespace Rossogames.Editor
{
    public static class CustomToolbarDrawer
    {
        public static IconButtonDataTool _bootAndPlayButton = Resources.Load<IconButtonDataTool>("Boot&PlayButton");
        public static IconButtonDataTool _editSceneButton = Resources.Load<IconButtonDataTool>("EditSceneButton");

        [MainToolbarElement("Custom Toolbar/Boot & Play", defaultDockPosition = MainToolbarDockPosition.Middle)]
        static MainToolbarElement LoadBootAndPlayButtonToolbar()
        {
            return _bootAndPlayButton.GetToolBarButton();
        }

        [MainToolbarElement("Custom Toolbar/Edit Scene", defaultDockPosition = MainToolbarDockPosition.Middle)]
        static MainToolbarElement EditSceneButtonToolbar()
        {
            return _editSceneButton.GetToolBarButton();
        }
    }
}