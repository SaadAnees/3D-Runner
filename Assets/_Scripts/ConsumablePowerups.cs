using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ConsumablePowerups : MonoBehaviour
{
    public static ConsumablePowerups instance;

    public Button ConsumeBtn;

    private void Update()
    {
        if (PowerManager.instance.powerState == PowerManager.PowerState.Run)
            ConsumeBtn.interactable = true;
        else
            ConsumeBtn.interactable = false;

        if (Databank.instance.PowerupCount <= 0)
            ConsumeBtn.interactable = false;

    }

    public void TurnOnConsumablePowerUp()
    {
        print("power count: " + Databank.instance.PowerupCount);
        if (Databank.instance.PowerupCount <= 0)
            return;

        Debug.Log("Consume Powerup: " + Databank.instance.Powerup);

        if (Databank.instance.Powerup == 1)
            PowerManager.instance.OnShield();
        if (Databank.instance.Powerup == 2)
            PowerManager.instance.OnHover();
        if (Databank.instance.Powerup == 3)
            PowerManager.instance.OnJetpack();
       

        Databank.instance.ConsumePowerUps(Databank.instance.Powerup);

    }
}
