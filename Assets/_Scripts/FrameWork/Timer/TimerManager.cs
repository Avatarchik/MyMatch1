/*******************************************************
 * 
 * 文件名(File Name)：             SpriteTitleChange.cs
 *
 * 作者(Author)：                  http://www.youkexueyuan.com
 *								  XiaoHong 
 *                                Yangzj
 *
 * 创建时间(CreateTime):           2016/03/02 13:48:41
 *
 *******************************************************/

using UnityEngine;
using System.Collections;

namespace MyFrameWork
{
	public class TimerManager : Manager
	{
		public static TimerManager Instance
		{
			get
			{
				return AppFacade.Instance.GetManager<TimerManager>(ManagerName.Timer);
			}
		}

		public static TimerBehaviour GetTimer(GameObject target)
		{
			return target.GetOrAddComponent<TimerBehaviour>();
		}

		public void Update()
		{
			FrameTimerHeap.Tick();
		}

		protected void OnReleaseValue()
		{
			FrameTimerHeap.ReleaseVal();
		}

		protected void OnAppQuit()
		{
			OnReleaseValue();
		}
	}
}