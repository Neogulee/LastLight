using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{ 
    
    protected override void on_destroyed(){
        Locator.event_manager.notify(new GameClearEvent());
    }
}
