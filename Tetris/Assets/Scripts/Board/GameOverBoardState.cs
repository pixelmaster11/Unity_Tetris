using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardSystem
{
    public class GameOverBoardState : BoardState
    {
        public GameOverBoardState(Board _board, BoardStateController _controller) : base(_board, _controller)
        {

        }

        public override void Entry()
        {
            DebugUtils.Log(this, "GameOver");
        }

     
        public override void StateUpdate()
        {
            
        }


        public override void Exit()
        {
           
        }
    }

}
