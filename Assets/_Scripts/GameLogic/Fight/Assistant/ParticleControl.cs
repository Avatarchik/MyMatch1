/*******************************************************
 * 
 * 文件名(File Name)：             ParticleControl
 *
 * 作者(Author)：                  http://www.youkexueyuan.com
 *								  XiaoHong 
 *                                Yangzj
 *
 * 创建时间(CreateTime):           2016/04/28 13:44:21
 *
 *******************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyFrameWork;
using DG.Tweening;

namespace Fight
{
	public class ParticleControl : Singleton<ParticleControl> 
	{
		private Queue<GameObject> _pools = new Queue<GameObject>();
		private Dictionary<int,Texture2D> _dicTexture = new Dictionary<int, Texture2D>();
		public override void Init()
		{
			for(int i = 1;i <= 6;i++)
			{
				_dicTexture.Add(i,ResourceMgr.Instance.GetAsset(string.Format(FightDefine.CrushCardFormat,i)).Asset as Texture2D);
			}
		}

		public void AddParticle(Vector3 pos,int cardType)
		{
			GameObject obj = null;

			if(_pools.Count > 0)
			{
				obj = _pools.Dequeue();
			}
			else
			{
				obj = ResourceMgr.Instance.LoadAndInstanceGameObjectFromPreload(FightDefine.ParticleCardCrush);
			}

			Renderer render = obj.GetComponent<Renderer>();
//			render.sortingLayerName = "UI";
//			render.sortingOrder = 4;
			render.material.mainTexture = _dicTexture[cardType + 1];

			obj.transform.position = pos;
			obj.gameObject.SetActive(true);
			obj.GetComponent<ParticleSystem>().Play();

			APPMonoController.Instance.StartCoroutine(Deactive(obj));
//			obj.transform.RunAction(CCSequence.Create(CCDelay.Create(1f),
//				CCCallFunc.Create(
//					()=>{
//					obj.SetActive(false);
//					_pools.Enqueue(obj);
//				})));
		}

		public void Clear()
		{
			_pools.Clear();
		}

		private IEnumerator Deactive(GameObject obj)
		{
			yield return new WaitForSeconds(1f);

			if(obj == null)
				yield break;
			
			obj.SetActive(false);
			_pools.Enqueue(obj);
		}
	}
}
