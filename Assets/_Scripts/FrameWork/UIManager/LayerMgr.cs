/*******************************************************
 * 
 * 文件名(File Name)：             LayerMgr
 *
 * 作者(Author)：                  http://www.youkexueyuan.com
 *								  XiaoHong 
 *                                Yangzj
 *
 * 创建时间(CreateTime):           2016/03/21 11:27:39
 *
 *******************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;

namespace MyFrameWork
{
	public enum E_LayerType
	{
		MainUI = 0,
		NormalUI = 200,
		Tips = 300,
		TopBar = 950,
		Notify = 1000,
	}



	public class LayerMgr : Singleton<LayerMgr> 
	{
		private Dictionary<E_LayerType,GameObject> _dicLayer; 
		private Transform _parentTransform;

		public override void Init()
		{
			_dicLayer = new Dictionary<E_LayerType, GameObject>();

			_parentTransform = GameObject.Find("UI Root").transform;

			//初始化
			int nums = System.Enum.GetNames(typeof(E_LayerType)).Length;
			for(int i = 0;i < nums;i++)
			{
				object obj = System.Enum.GetValues(typeof(E_LayerType)).GetValue(i);
				_dicLayer.Add((E_LayerType)obj,CreateLayerGameObject(obj.ToString(),(E_LayerType)obj));
			}
		}

		protected override void OnReleaseValue()
		{
			_dicLayer.Clear();
		}

		protected override void OnAppQuit()
		{
			_dicLayer = null;
		}

		private GameObject CreateLayerGameObject(string name,E_LayerType type)
		{
			GameObject layer = new GameObject(name);
			layer.transform.SetParent(AppFacade.Instance.GetManager<UIMgr>(ManagerName.UI).UIContainer,false);
			layer.transform.localPosition = new Vector3(0f,0f,(int)type) * -1f;
			layer.layer = _parentTransform.gameObject.layer;

			return layer;
		}


		public void SetLayer(BaseUI baseUI)
		{
			baseUI.CachedTransform.SetParent(_dicLayer[baseUI.mUILayertype].transform,false);

            UIPanel[] panels = baseUI.GetComponentsInChildren<UIPanel>(true);
			foreach(UIPanel panel in panels)
			{
				panel.depth += (int)baseUI.mUILayertype;
			}
		}
	}
}
