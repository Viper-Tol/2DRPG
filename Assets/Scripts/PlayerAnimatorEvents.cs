using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorEvents : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void AnimationTrigger(){

        player.AttackOver();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
