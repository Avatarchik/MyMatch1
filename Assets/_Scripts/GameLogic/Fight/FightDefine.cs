/*******************************************************
 * 
 * 文件名(File Name)：             FightDefine
 *
 * 作者(Author)：                  http://www.youkexueyuan.com
 *								  XiaoHong 
 *                                Yangzj
 *
 * 创建时间(CreateTime):           2016/04/27 16:28:22
 *
 *******************************************************/

using UnityEngine;
using System.Collections;

namespace Fight
{
	public class FightDefine 
	{
		public enum E_NoteMsgType
		{
			None,
			NoMoves,
			NoShuffle,
		}

		public const string LabelHPFlying = "LabelHPFlying";

		public const string ParticleCardCrush = "ParticleCardCrush";

		//卡牌破碎图片名
		public const string CrushCardFormat = "CrushCard{0}";
		public const string CrushCard1 = "CrushCard1";
		public const string CrushCard2 = "CrushCard2";
		public const string CrushCard3 = "CrushCard3";
		public const string CrushCard4 = "CrushCard4";
		public const string CrushCard5 = "CrushCard5";
		public const string CrushCard6 = "CrushCard6";

		/// <summary>
		/// boss技能，不能移动的格子
		/// </summary>
		public const string SpecialBlock = "SpecialBlock";
	}
}
