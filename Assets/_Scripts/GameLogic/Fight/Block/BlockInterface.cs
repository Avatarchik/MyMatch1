/*
 * 
 * 文件名(File Name)：             BlockInterface
 *
 * 作者(Author)：                  #AuthorName#
 *
 * 创建时间(CreateTime):           2016/03/29 17:07:17
 *
 */

using UnityEngine;
using System.Collections;

namespace Fight
{
	public abstract class BlockInterface : MonoBehaviour
	{
		public Slot slot;

		abstract public void BlockCrush(bool force);
		abstract public bool CanBeCrushedByNearSlot();
	}
}

