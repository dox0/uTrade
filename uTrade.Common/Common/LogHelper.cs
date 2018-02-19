///
/// Author      Shi Xiaokang
/// http://www.cnblogs.com/xiaokang088/
/// Version 	1.00	
/// Date: 2011-1-21
/// </summary>

using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

public static class LogHelper
{
    public enum LogLevel
    {
        Trace = 1,
        Debug,
        Info,
        Error
    }

    public static string logLevel { get; set; }

    public static string AutoFulsh { get; set; }

    #region private variable
    //{datetime} {MethodInfo|FileInfo} {CustomInfo} 
    const string formatWithMethodInfo = "UI|{0} | {1} | {2}";
    const string formatWithTwoParms = "UI|{0} | {1} ";
    const string formatWithOneParm = "UI|{0} ";
    const string formatErrorWithMethodInfo = "UIErr: {0} | {1} | {2}";
    const string formatTimeWithMilliSecond = "HH:mm:ss fff";
    const string formatWithMethodResult = "Method:{0} --> Result:{1}";
    const string formaWithMethodResultAndParms = "Method:{0} --> Result:{1} -- Parms:{2}";
    #endregion

    #region public methods

    #region init

    static LogHelper()
    {
        Init();
    }
    
    /// <summary>
    /// init by configuration
    /// </summary>
    public static void Init()
    {
        string method = string.Format("{0}.{1}", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name);


        #region define
        LogLevel m_logLevel = LogLevel.Error;
        string m_logPath = "Config\\";
        FileStream stream;
        FileMode fileMode;
        #endregion


            Int16 iLogLevel = Convert.ToInt16(logLevel);
            if(iLogLevel >= 1 && iLogLevel <= 4)
            {
                m_logLevel = (LogLevel)iLogLevel;
            }

        // auto flush
        Trace.AutoFlush = AutoFulsh == "1";
        #endregion


        //Debugger&File (Append the log at the end of file and close it after each log output)
        fileMode = FileMode.Append;


        #region check path
        //path is null
        //m_logPath = ConfigurationManager.AppSettings["logSubPath"].Trim();


        //path has invalid char
        var pathCharArray = m_logPath.ToCharArray();
        if (pathCharArray.Any(o => Path.GetInvalidPathChars().Contains(o)))
            return;

        //FileName has invalid char
        //note : invalid file name chars count is 41, 
        //invalid path  chars count  is 36
        //and , the top 36 of invalid file name chars  are  same as invalid path chars
        //so,first check path invalid chars,second check filename,only filename
        var filenameCharArray = Path.GetFileName(m_logPath).ToCharArray();
        if (filenameCharArray.Any(o => Path.GetInvalidFileNameChars().Contains(o)))
            return;

        //EnvironmentVariables Path
        if (m_logPath.Contains('%'))
            m_logPath = Environment.ExpandEnvironmentVariables(m_logPath);

        //cheng relative path to absolute path.
        if (String.IsNullOrEmpty(Path.GetPathRoot(m_logPath)))
            m_logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, m_logPath);
        #endregion

        #region file log
        //risk:directory readonly;need administrator right to createfile;and so on
        //use try-catch


        try
        {
            m_logPath += "AppLog_" + DateTime.Now.ToString("yyMMdd") + ".log";
            if (!Directory.Exists(Path.GetDirectoryName(m_logPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(m_logPath));
            }

            stream = File.Open(m_logPath, fileMode, FileAccess.Write, FileShare.ReadWrite);
            TextWriterTraceListener text = new TextWriterTraceListener(stream);
            //text.TraceOutputOptions = TraceOptions.DateTime;
            Trace.Listeners.Add(text);
        }
        catch (Exception ex)
        {
            Trace.Write(ex);
        }
        #endregion

    }
    #endregion

    #region write override

    /// <summary>
    /// output info with time,the time has milliseccond
    /// example as :[UI|2011/12/20 14:57:39 630 | Test Write Method  ]
    /// </summary>
    /// <param name="info"></param>
    public static void Write(LogLevel loglevel, string info)
    {
        string logInfo = string.Format("{0} | {1} | {2} ", DateTime.Now.ToString(formatTimeWithMilliSecond), loglevel.ToString(), info);

        Trace.WriteLine(logInfo);
    }

    /// <summary>
    /// output info with time,the time has milliseccond,but no date.
    /// receive format params
    /// example as
    /// LogHelper.Write("current ID:{0} Name:{1}", 115001, "Jackon");
    /// UI|2011/12/20 15:01:28 801 | current ID:115001 Name:Jackon 
    /// </summary>
    /// <param name="format"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    /// <exception >
    /// string.format fail,only output format 
    /// </exception>
    public static void Write(string format, params object[] args)
    {
        try
        {
            CheckNullParms(args);
            Write(string.Format(format, args));
        }
        catch (Exception ex)
        {
            Trace.Write(ex);
        }
        finally
        {

        }
    }

    #endregion

    #region with with stack information
    /// <summary>
    /// example:
    /// UI|2011/12/20 15:31:23 220 | CSharpLogDemo.TestClass.TestDo 
    /// </summary>
    /// <param name="info"></param>
    public static void WriteStack()
    {
        Write(GetExecutingMethodName());
    }

    /// <summary>
    /// example
    /// UI|2011/12/20 15:31:23 954 | E:\ToshibaCommon\TosWpfCommonLib\CSharpLogDemo\MainWindow.xaml.cs(116)  --> CSharpLogDemo.TestClass.TestDoDetail 
    /// must has pdb file,otherse ,no file info
    /// </summary>
    /// <param name="info">custom info</param>
    public static void WriteStackDetail()
    {
        Write(GetExecutingInfo());
    }
    #endregion

    #region Write Exception
    /// <summary>
    /// output datetime and exception.message
    /// UI|2011/12/20 15:39:48 088 | DoTest 
    ///System.Exception: Test Exception
    ///at CSharpLogDemo.TestClass.TestException() in E:\ToshibaCommon\TosWpfCommonLib\CSharpLogDemo\MainWindow.xaml.cs:line 153
    ///at CSharpLogDemo.MainWindow.btnException_Click(Object sender, RoutedEventArgs e) in E:\ToshibaCommon\TosWpfCommonLib\CSharpLogDemo\MainWindow.xaml.cs:line 119
    /// </summary>
    /// <param name="ex">current exception.</param>
    /// <param name="caption">custom Caption</param>
    public static void WriteException(Exception ex, string caption)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat(formatWithTwoParms, DateTime.Now.ToString(formatTimeWithMilliSecond), caption);
        sb.AppendLine();
        sb.Append(ex.ToString());
        if (ex.InnerException != null)
        {
            sb.AppendLine("Inner Exception");
            sb.Append(ex.InnerException.ToString());
        }
        Trace.WriteLine(sb.ToString());
    }

    /// <summary>
    /// output datetime and exception.message
    /// UI|2011/12/20 15:39:45 405 
    ///System.Exception: Test Exception
    ///at CSharpLogDemo.TestClass.TestException() in E:\ToshibaCommon\TosWpfCommonLib\CSharpLogDemo\MainWindow.xaml.cs:line 153
    ///at CSharpLogDemo.MainWindow.btnException_Click(Object sender, RoutedEventArgs e) in E:\ToshibaCommon\TosWpfCommonLib\CSharpLogDemo\MainWindow.xaml.cs:line 119
    /// </summary>
    /// <param name="ex">current exception.</param>
    /// <param name="caption">custom Caption</param>
    public static void Exception(Exception ex)
    {
        if (ex == null) return;
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat(formatWithOneParm, DateTime.Now.ToString(formatTimeWithMilliSecond));
        sb.AppendLine();
        sb.Append(ex.ToString());
        if (ex.InnerException != null)
        {
            sb.AppendLine("Inner Exception");
            sb.Append(ex.InnerException.ToString());
        }
        Trace.WriteLine(sb.ToString());
    }
    #endregion




    #region private method


    /// <summary>
    /// get current call method name
    /// </summary>
    /// <returns>FullName.Name,example as [WpfTraceSolution.MainWindow.button1_Click]</returns>
    static string GetExecutingMethodName()
    {
        string result = "Unknown";
        StackTrace trace = new StackTrace(false);

        for (int index = 0; index < trace.FrameCount; ++index)
        {
            StackFrame frame = trace.GetFrame(index);
            MethodBase method = frame.GetMethod();
            Type declaringType = method.DeclaringType;
            if (declaringType != typeof(LogHelper))
            {
                result = string.Concat(method.DeclaringType.FullName, ".", method.Name);
                break;
            }
        }

        return result;
    }

    /// <summary>
    /// get Execute details ,output example as [WpfTraceSolution.MainWindow.btnWriteWithFileInfo_Click | F:\WpfTraceSolution\MainWindow.xaml.cs(79)]
    /// </summary>
    /// <returns></returns>
    static string GetExecutingInfo()
    {
        string result = "Unknown";
        StringBuilder sbInfo = new StringBuilder();
        StackTrace trace = new StackTrace(true);

        for (int index = 0; index < trace.FrameCount; ++index)
        {
            StackFrame frame = trace.GetFrame(index);
            MethodBase method = frame.GetMethod();
            Type declaringType = method.DeclaringType;
            if (declaringType != typeof(LogHelper))
            {
                sbInfo.AppendFormat(" --> {0}.{1}", method.DeclaringType.FullName, method.Name);
                sbInfo.AppendFormat("{0}", frame.GetFileName());
                sbInfo.AppendFormat("({0}) ", frame.GetFileLineNumber());
                result = sbInfo.ToString();
                break;
            }
        }

        return result;
    }

    static void CheckNullParms(object[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == null)
                args[i] = "Null";
        }
    }

    public static void TraceFunction(_FunctionInfo _Function)
    {

    }

    public class _FunctionInfo
    {
        DateTime _EnetrTime;
        public _FunctionInfo()
        {
            _EnetrTime = DateTime.Now;
        }



    }
    #endregion
}

