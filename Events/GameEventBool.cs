using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Objects fork https://github.com/Feddas/SoArchitecture
/// Author: Shawn Featherly in 2018
/// </summary>
namespace SoArchitecture
{
    [CreateAssetMenu(fileName = "GameEventBool", menuName = "SoArchitecture/GameEventBool")]
    public class GameEventBool : GameEventPayload<bool, IGameEventListener<bool>, BoolVariable>
    {
        public override void LateAwake()
        {
            // Ensure base.LateAwake can leverage PlayerPrefs
            playerPrefGet = () => PlayerPrefs.GetInt(playPrefsKey, PlayerPrefDefault ? 1 : 0) == 0 ? false : true;
            playerPrefSet = (payload) => PlayerPrefs.SetInt(playPrefsKey, payload ? 1 : 0);

            base.LateAwake();
        }

        [ContextMenu("Toggle value then Raise Event")]
        public void Toggle()
        {
            this.Raise(false == LastPayload);
        }

        /// <summary> Unity 2018.4 does not supporting Toggles being updated without raising OnValueChanged events. This function allows the isOn data to be retrieved when an EventTrigger components OnClick is used on the Toggle. </summary>
        public void OnClickToggle(UnityEngine.EventSystems.BaseEventData baseEvent)
        {
            // Guard if click did not occur ontop of a toggle
            var toggle = baseEvent.selectedObject.GetComponent<UnityEngine.UI.Toggle>();
            if (toggle == null)
            {
                Debug.LogError(baseEvent.selectedObject.name + " must contain a toggle to use GameEventBools OnclickToggle()");
                return;
            }

            // Raise event using toggle as payload
            this.Raise(toggle.isOn);
        }
    }
}
