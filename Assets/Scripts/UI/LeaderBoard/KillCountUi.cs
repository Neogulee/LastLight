using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;





public class KillCountUi : MonoBehaviour
{
    public int killcount { get; private set; } = 0;
    TMP_Text killCountUiText;
    private void Awake()
    {
        killCountUiText = GetComponent<TMP_Text>();
        Locator.event_manager.register<EnemyDestroyedEvent>(increase_kill_count);
    }
    
    public void increase_kill_count(IEventParam param)
    {
        killcount++;
    }

    private void LateUpdate()
    {
        killCountUiText.text = killcount.ToString();
    }
}
