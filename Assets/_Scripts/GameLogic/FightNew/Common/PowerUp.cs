using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FightNew
{
	[System.Serializable]
	public class PowerUps 
	{
		public string name = "";
		public string contentName = "";
		public bool color = true;
		public int levelEditorID = 0;
		public string levelEditorName = "";

		public PowerUps(string _name,string _contentName,bool _color,int _levelEditorID,string _levelEditorName)
		{
			name = _name;
			contentName = _contentName;
			color = _color;
			levelEditorID = _levelEditorID;
			levelEditorName = _levelEditorName;
		}


		private static List<PowerUps> _powerups = null;
		public static List<PowerUps> powerupsNew
		{
			get
			{
				if(_powerups == null)
				{
					_powerups = new List<PowerUps>();
				}

				if(_powerups.Count <= 0)
				{
					InitPowerUpData();
				}

				return _powerups;
			}
		}

		public static void InitPowerUpData()
		{
			if(_powerups == null)	
				_powerups = new List<PowerUps>();

			if(_powerups.Count > 0)
			{
				return;
			}

			_powerups.Add(new PowerUps("OneLine","OneLine",false,1,"单线"));
			_powerups.Add(new PowerUps("CrossLine","CrossLine",false,2,"十字线"));
			_powerups.Add(new PowerUps("ThreeLine","ThreeLine",false,3,"三线"));

		}
	}
}