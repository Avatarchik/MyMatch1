/*******************************************************
 * 
 * 文件名(File Name)：             DebugUtil
 *
 * 作者(Author)：                  Yangzj
 *
 * 创建时间(CreateTime):           2016/02/24 18:51:07
 *
 *******************************************************/


//#define LogOut  //开启日志,对外正式发布时关闭
//#define TxtOut //是否开启写txt文件,对外正式发布时关闭，只用于真机测试

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;
using System.Reflection;
using System.Linq;

/// <summary>
/// E log level.
/// 自定义Log等级
/// </summary>
public enum E_LogLevel
{
	NONE = 0,
	DEBUG = 1,
	INFO = 2,
	WARNING = 4,
	ERROR = 8,
	/// <summary>
	/// The EXCEPTION.
	/// Exception 异常Log
	/// </summary>
	EXCEPTION = 16,
	/// <summary>
	/// The CRITICA.
	/// 重要Log
	/// </summary>
	CRITICAL = 32,
}	

/// <summary>
/// Debug util.
/// Debug 工具类
/// DebugUtil.Debug("afjjakglj");
/// </summary>
public class DebugUtil
{
	public static E_LogLevel CurrentLogLevels = E_LogLevel.DEBUG | E_LogLevel.INFO | E_LogLevel.WARNING | E_LogLevel.ERROR | E_LogLevel.CRITICAL | E_LogLevel.EXCEPTION;
	private const Boolean IsShowStack = true;
	private static LogWriter _logWriter;

	static DebugUtil()
	{
		//010101
		_logWriter = new LogWriter();
		//Application.RegisterLogCallback(new Application.LogCallback(ProcessLogMessageReceived));
		Application.logMessageReceived += ProcessLogMessageReceived;
	}

	public static void Release()
	{
		_logWriter.Release();
	}

	static ulong index = 0;

	#if LogOut
	public static void Debug(object message, Boolean isShowStack = IsShowStack)
	{
		if (E_LogLevel.DEBUG == (CurrentLogLevels & E_LogLevel.DEBUG))
			Log(string.Concat(" <color=orange>[DEBUG]</color>: ", message," Index = ", index++,'\n',isShowStack ? GetStackInfo() : string.Empty), E_LogLevel.DEBUG);
	}

//	public static void Debug(string filter, object message, Boolean isShowStack = IsShowStack)
//	{
//		if (E_LogLevel.DEBUG == (CurrentLogLevels & E_LogLevel.DEBUG))
//		{
//			Log(string.Concat(" <color=orange>[DEBUG]</color>: ", isShowStack ? GetStackInfo() : "", message), E_LogLevel.DEBUG);
//		}
//
//	}

	public static void Info(object message, Boolean isShowStack = IsShowStack)
	{
		if (E_LogLevel.INFO == (CurrentLogLevels & E_LogLevel.INFO))
			Log(string.Concat(" [INFO]: ", message,'\n', isShowStack ? GetStackInfo() : string.Empty), E_LogLevel.INFO);
	}

	public static void Warning(object message, Boolean isShowStack = IsShowStack)
	{
		if (E_LogLevel.WARNING == (CurrentLogLevels & E_LogLevel.WARNING))
			Log(string.Concat(" [WARNING]: ", message,'\n',isShowStack ? GetStackInfo() : string.Empty), E_LogLevel.WARNING);
	}

	public static void Error(object message, Boolean isShowStack = IsShowStack)
	{
		if (E_LogLevel.ERROR == (CurrentLogLevels & E_LogLevel.ERROR))
			Log(string.Concat(" [ERROR]: ", message, '\n', isShowStack ? GetStackInfo() : string.Empty), E_LogLevel.ERROR);
	}

	public static void Critical(object message, Boolean isShowStack = IsShowStack)
	{
		if (E_LogLevel.CRITICAL == (CurrentLogLevels & E_LogLevel.CRITICAL))
			Log(string.Concat(" [CRITICAL]: ", message, '\n', isShowStack ? GetStackInfo() : string.Empty), E_LogLevel.CRITICAL);
	}

	public static void Except(Exception ex, object message = null)
	{
		if (E_LogLevel.EXCEPTION == (CurrentLogLevels & E_LogLevel.EXCEPTION))
		{
			Exception innerException = ex;
			while (innerException.InnerException != null)
			{
				innerException = innerException.InnerException;
			}
			Log(string.Concat(" [EXCEPTION]: ", message == null ? "" : message + "\n", ex.Message, innerException.StackTrace), E_LogLevel.CRITICAL);
		}
	}

	#else
	public static void Debug(object message, Boolean isShowStack = IsShowStack)
	{
	}

	public static void Info(object message, Boolean isShowStack = IsShowStack)
	{
	}

	public static void Warning(object message, Boolean isShowStack = IsShowStack)
	{
	}

	public static void Error(object message, Boolean isShowStack = IsShowStack)
	{
	}

	public static void Critical(object message, Boolean isShowStack = IsShowStack)
	{
	}

	public static void Except(Exception ex, object message = null)
	{
	}
	#endif

	//没毛用的
//	private static String GetStacksInfo()
//	{
//		StringBuilder sb = new StringBuilder();
//		StackTrace st = new StackTrace();
//		var sf = st.GetFrames();
//		for (int i = 2; i < sf.Length; i++)
//		{
//			sb.AppendLine("line:" + sf[i].GetFileLineNumber() + " " + sf[i].ToString());
//		}
//
//		return sb.ToString();
//	}
//
	private static void Log(string message, E_LogLevel level, bool writeEditorLog = true)
	{
		var msg = string.Concat(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message);
		_logWriter.WriteLog(msg, level, writeEditorLog);
	}

	private static String GetStackInfo()
	{
//		StackTrace st1 = new StackTrace();
//		StackFrame[] sfs = st1.GetFrames();
//		for (int u = 0; u < sfs.Length; ++u)
//		{
//			MethodBase mb = sfs[u].GetMethod();
//			string s = String.Format("[CALL STACK][{0}]: {1}.{2}", u, mb.DeclaringType.FullName, mb.Name);
//			UnityEngine.Debug.Log(sfs[u].GetFileLineNumber());
//
//
//		}
//
		StackTrace st = new StackTrace(true);
		StackFrame sf = st.GetFrame(2);//
		MethodBase method = sf.GetMethod();
		return String.Format("{0}.{1}()\n{2}", method.ReflectedType.Name, method.Name,BuildStackTraceMessage(st));
	}


	private static string BuildStackTraceMessage(StackTrace stackTrace)
	{
		if (stackTrace != null)
		{
			var frameList = stackTrace.GetFrames();
			var realFrameList = frameList.Where(i => i.GetMethod().DeclaringType != typeof(DebugUtil) && i.GetFileLineNumber() > 0);
			if (realFrameList.Any())
			{
				StringBuilder builder = new StringBuilder();
				realFrameList = realFrameList.Reverse();
				var lastFrame = realFrameList.Last();
				string[] fileName = lastFrame.GetFileName().Split('/');
				builder.AppendFormat("源文件：{0},行号：{1}", fileName[fileName.Length-1],lastFrame.GetFileLineNumber());//.AppendLine();
//				builder.AppendFormat("方法名：{0}", lastFrame.GetMethod().ToString()).AppendLine();
//				builder.AppendLine("堆栈跟踪：");
//				builder.AppendLine("=================================================================");
//			
//			
//				MethodBase method;
//				foreach (var frame in realFrameList)
//				{
//					method = frame.GetMethod();
//					builder.AppendFormat("> {0} 类下的第{1}行 {2} 方法", method.DeclaringType.ToString(), frame.GetFileLineNumber(), method.ToString()).AppendLine();
//				}
//				builder.AppendLine("=================================================================");
				return builder.ToString();
			}
		}

		return "没有堆栈信息";
	}
	
	private static void ProcessLogMessageReceived(string message, string stackTrace, LogType type)
	{
		E_LogLevel logLevel = E_LogLevel.DEBUG;
		switch (type)
		{
			case LogType.Assert:
				logLevel = E_LogLevel.DEBUG;
				break;
			case LogType.Error:
				logLevel = E_LogLevel.ERROR;
				break;
			case LogType.Exception:
				logLevel = E_LogLevel.EXCEPTION;
				break;
			case LogType.Log:
				logLevel = E_LogLevel.DEBUG;
				break;
			case LogType.Warning:
				logLevel = E_LogLevel.WARNING;
				break;
			default:
				break;
		}

		if (logLevel == (CurrentLogLevels & logLevel))
			Log(string.Concat(" [SYS_", logLevel, "]: ", message, '\n', stackTrace), logLevel, false);
	}
}

/// <summary>
/// Log writer.
/// Log 文件写入器
/// </summary>
public class LogWriter
{
	#if TxtOut
	private string _logPath = UnityEngine.Application.persistentDataPath + "/Log/";
	private string _logFileName = "Log_{0}.txt";
	private string _logFilePath;
	private FileStream _fs;
	private StreamWriter _sw;
	#endif
	private Action<String, E_LogLevel, bool> _logWriter;
	private readonly static object _locker = new object();


	public LogWriter()
	{
		try
		{
			_logWriter = Write;

			#if TxtOut
			if (!Directory.Exists(_logPath))
			Directory.CreateDirectory(_logPath);

			_logFilePath = String.Concat(_logPath, String.Format(_logFileName, DateTime.Today.ToString("yyyyMMdd")));
			_fs = new FileStream(_logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
			_sw = new StreamWriter(_fs);
			#endif

		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError(ex.Message);
		}
	}

	public void Release()
	{
		#if TxtOut
		lock (_locker)
		{
			if (_sw != null)
			{
				_sw.Close();
				_sw.Dispose();
			}
			if (_fs != null)
			{
				_fs.Close();
				_fs.Dispose();
			}
		}
		#endif
	}

	public void WriteLog(string msg, E_LogLevel level, bool writeEditorLog)
	{
		_logWriter(msg, level, writeEditorLog);
//		#if UNITY_IPHONE
//		_logWriter(msg, level, writeEditorLog);
//		#else
		//beginInvoke多核会有先后顺序不一致的
//		_logWriter.BeginInvoke(msg, level, writeEditorLog, null, null);
//		#endif
	}

	private void Write(string msg, E_LogLevel level, bool writeEditorLog)
	{
		lock (_locker)
			try
		{
			if (writeEditorLog)
			{
				switch (level)
				{
					case E_LogLevel.DEBUG:
					case E_LogLevel.INFO:
						UnityEngine.Debug.Log(msg);
						break;
					case E_LogLevel.WARNING:
						UnityEngine.Debug.LogWarning(msg);
						break;
					case E_LogLevel.ERROR:
					case E_LogLevel.EXCEPTION:
					case E_LogLevel.CRITICAL:
						UnityEngine.Debug.LogError(msg);
						break;
					default:
						break;
				}
			}

			#if TxtOut
			if (_sw != null)
			{
				_sw.WriteLine(msg);
				_sw.Flush();
			}
			#endif
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError(ex.Message);
		}
	}
}

