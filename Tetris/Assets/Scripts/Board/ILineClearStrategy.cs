using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardSystem
{

    /// <summary>
    /// Base interface for line clear algorithms
    /// </summary>
    public interface ILineClearStrategy 
    {
        void ClearLine();
    }

}
