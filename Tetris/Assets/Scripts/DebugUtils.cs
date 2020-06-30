#define DEBUGLOG
//#define DEBUG_STATES

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class DebugUtils 
{
    
    private static readonly int DEFAULT_SIZE = 12;
    public static int FontSize = 12;


    public static void Log(object obj, object message)
    {
         
        var name = obj.GetType().FullName;  
        Log( "[" + name + "]: " + message );
        
    }

    public static void LogWarning(object obj, object message)
    {
        var name = obj.GetType().FullName;
 
        LogWarning( "[" + name + "]: " + message );
        
    }

    public static void LogError(object obj, object message)
    {
        var name = obj.GetType().FullName;
        LogError( "[" + name + "]: " + message );
        
    }

    public static void LogBold(object obj, object message)
    {
        var name = obj.GetType().FullName; 
        Log(ApplyStyle( "[" + name + "]: " + "<b>" + message + "</b>" ));
        
    }

    public static void LogItalic(object obj, object message)
    {
        var name = obj.GetType().FullName;
        Log(ApplyStyle( "[" + name + "]: " + "<i>" + message + "</i>" ));      
    }

    public static void Log(object obj, object message, string color)
    {
        var name = obj.GetType().FullName;
      
        Log(ApplyStyle( "[" + name + "]: " + "<color=" + color + ">" + message + "</color>" ));
        
    }

    #if DEBUG_STATES 

        public static void LogState(object obj, object message, string color = "yellow")
        {
            var name = obj.GetType().FullName;

            var log = ApplyStyle( "[" + name + "]: " + "<color=" + color + ">" + message + "</color>" );
            log = ApplyStyle(log);
            Log(log);
        }

    #else

        public static void LogState(object obj, object message, string color = "yellow")
        {
        
        }

    #endif

    private static object ApplyStyle(object message)
    {
        object log = message;

        if (DEFAULT_SIZE != FontSize )
        {
            log = "<size=" + FontSize.ToString() + ">" + message + "</size>";
        }

        log += "\n";

        return log;
    }

    
#if DEBUGLOG

        private static void Log(object message)
        {          
            UnityEngine.Debug.Log(ApplyStyle(message));      
        }
        private static void Log(object message, UnityEngine.Object context)
        {    
            UnityEngine.Debug.Log(ApplyStyle(message), context);     
        }


        private static void LogError(object message)
        {     
            UnityEngine.Debug.LogError(ApplyStyle(message));      
        }

        private static void LogError(object message, UnityEngine.Object context)
        {       
            UnityEngine.Debug.LogError(ApplyStyle(message), context);      
        }

        private static void LogWarning(object message)
        {       
            UnityEngine.Debug.LogWarning(ApplyStyle(message));
        
        }
        private static void LogWarning(object message, Object context)
        {      
            UnityEngine.Debug.LogWarning(ApplyStyle(message), context);      
        }


        private static void LogException(System.Exception exception)
        {   
            UnityEngine.Debug.LogException(exception);      
        }

        private static void LogException(System.Exception exception, UnityEngine.Object context)
        {    
            UnityEngine.Debug.LogException( exception, context);       
        } 

    #else

     private static void Log(object message)
        {   
           
            
        }
        private static void Log(object message, UnityEngine.Object context)
        {    
               
        }


        private static void LogError(object message)
        {     
               
        }

        private static void LogError(object message, UnityEngine.Object context)
        {       
               
        }

        private static void LogWarning(object message)
        {       
            
        
        }
        private static void LogWarning(object message, Object context)
        {      
               
        }


        private static void LogException(System.Exception exception)
        {   
               
        }

        private static void LogException(System.Exception exception, UnityEngine.Object context)
        {    
                   
        } 

    #endif


}
