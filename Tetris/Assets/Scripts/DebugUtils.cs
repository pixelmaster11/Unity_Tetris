#define DEBUGLOG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class DebugUtils 
{
    
    private static readonly int DEFAULT_SIZE = 12;
    public static int FontSize = 12;
    private static IList<string> m_objects = new List<string>();




    public static void Add(object obj)
    {
        var name = obj.GetType().FullName;

        if ( !m_objects.Contains( name ) )
        {
            m_objects.Add( name );
        }
    }

    public static void Remove( object obj )
    {
        var name = obj.GetType().FullName;
        if ( m_objects.Contains( name ) )
        {
            m_objects.Remove( name );
        }
    }


    public static void Log(object obj, object message)
    {
         
        var name = obj.GetType().FullName;
        if ( m_objects.Contains( name ) )
        {
            Log( "[" + name + "]: " + message );
        }
    }

    public static void LogWarning(object obj, object message)
    {
        var name = obj.GetType().FullName;
        if ( m_objects.Contains( name ) )
        {
            LogWarning( "[" + name + "]: " + message );
        }
    }

    public static void LogError(object obj, object message)
    {
        var name = obj.GetType().FullName;
        if ( m_objects.Contains( name ) )
        {
            LogError( "[" + name + "]: " + message );
        }
    }

    public static void LogBold(object obj, object message)
    {
        var name = obj.GetType().FullName;
        if ( m_objects.Contains( name ) )
        {
            Log( ApplyStyle( "[" + name + "]: " + "<b>" + message + "</b>" ) );
        }
    }

    public static void LogItalic(object obj, object message)
    {
        var name = obj.GetType().FullName;
        if ( m_objects.Contains( name ) )
        {
            Log( ApplyStyle( "[" + name + "]: " + "<i>" + message + "</i>" ) );
        }
    }

    public static void LogColor(object obj, object message, string color)
    {
        var name = obj.GetType().FullName;
        if ( m_objects.Contains( name ) )
        {
            Log( ApplyStyle( "[" + name + "]: " + "<color=" + color + ">" + message + "</color>" ) );
        }
    }

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
        public static void Log(object message)
        {   
           
            UnityEngine.Debug.Log(ApplyStyle(message));      
        }
        public static void Log(object message, UnityEngine.Object context)
        {    
            UnityEngine.Debug.Log(ApplyStyle(message), context );     
        }


        public static void LogError(object message)
        {     
            UnityEngine.Debug.LogError(ApplyStyle(message));      
        }

        public static void LogError(object message, UnityEngine.Object context)
        {       
            UnityEngine.Debug.LogError(ApplyStyle(message), context);      
        }

        public static void LogWarning(object message)
        {       
            UnityEngine.Debug.LogWarning(ApplyStyle(message));
        
        }
        public static void LogWarning(object message, Object context)
        {      
            UnityEngine.Debug.LogWarning(ApplyStyle(message), context);      
        }


        public static void LogException(System.Exception exception)
        {   
            UnityEngine.Debug.LogException(exception);      
        }

        public static void LogException(System.Exception exception, UnityEngine.Object context)
        {    
            UnityEngine.Debug.LogException( exception, context);       
        } 

    #else

     public static void Log(object message)
        {   
           
            
        }
        public static void Log(object message, UnityEngine.Object context)
        {    
               
        }


        public static void LogError(object message)
        {     
               
        }

        public static void LogError(object message, UnityEngine.Object context)
        {       
               
        }

        public static void LogWarning(object message)
        {       
            
        
        }
        public static void LogWarning(object message, Object context)
        {      
               
        }


        public static void LogException(System.Exception exception)
        {   
               
        }

        public static void LogException(System.Exception exception, UnityEngine.Object context)
        {    
                   
        } 

        #endif


}
