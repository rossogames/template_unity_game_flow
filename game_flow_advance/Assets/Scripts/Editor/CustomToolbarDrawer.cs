using UnityEditor.Toolbars;
using UnityEngine;

namespace RossoGames.Editor
{
    public static class CustomToolbarDrawer
    {
        public static IconButtonProfile _bootAndPlayButton = Resources.Load<IconButtonProfile>("Boot&PlayButton");
        public static IconButtonProfile _editSceneButton = Resources.Load<IconButtonProfile>("EditSceneButton");

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