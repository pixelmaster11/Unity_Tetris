using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Configs;

namespace BoardSystem
{

    /// <summary>
    /// This state is responsible for checking line completion,
    ///  removing lines with chosen line clear method, and bring lines down after line clear
    /// </summary>
    public class LineCompletionBoardState : BoardState
    {

        //Ref to line clear method
        private ILineClearStrategy lineClearStrategy;

        //Init and set line clear method
        public LineCompletionBoardState(Board _board, BoardStateController _controller) : base(_board, _controller)
        {
            SetLineClearMethod();
        }

        public override void Entry()
        {
        
            lineClearStrategy.ClearLine();
            stateController.ChangeState(BoardStateType.AutoFallState);
        }

        public override void Exit()
        {
            
        }

        public override void StateUpdate()
        {
            
        }


        //Sets appropriate line clear method
        private void SetLineClearMethod()
        {
            switch(boardConfig.lineClearType)
            {
                case LineClearType.Naive:
                lineClearStrategy = new NaiveLineClear(board);
                break;

                default:
                lineClearStrategy = new NaiveLineClear(board);
                break;
            }
        }

    }

}
