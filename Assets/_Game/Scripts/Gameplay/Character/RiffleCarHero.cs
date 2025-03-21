using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiffleCarHero : Hero
{
    //public override void OnWalkEnter()
    //{
    //    destination = EntitiesManager.Ins.CurrentCar.TargetPosCarHero.position;
    //    if (!isDestination)
    //    {
    //        SetDestination(destination); //override destination o cac class child
    //        ChangeAnim(Constant.ANIM_WALK);
    //    }

    //}
    //public override void OnWalkExecute()
    //{
    //    if(isDestination)
    //    {
    //        ChangeAnim(Constant.ANIM_KNEELDOWN);
    //        //ChangeState(Constant.ATK_STATE);
    //    }    
    //}
    //public override void OnStandUpEnter()
    //{
    //    base.OnStandUpEnter();
    //}
    //public override void OnStandUpExecute()
    //{
    //    base.OnStandUpExecute();
    //}
    //public override void OnAttackEnter()
    //{
    //    base.OnAttackEnter();
    //}
    //public override void OnAttackExecute()
    //{
    //    if (target != null)
    //    {
    //        if (timer > 0)
    //        {
    //            timer -= Time.deltaTime;
    //            //isAttack = false;
    //        }
    //        else
    //        {
    //            if (!target.isDeath)
    //            {
    //                OnAttackEnter();
    //            }
    //        }
    //    }
    //    else if(target == null || target.isDeath)
    //    {
    //        ChangeAnim(Constant.ANIM_STANDUP);
    //    }    
    //}    
}
