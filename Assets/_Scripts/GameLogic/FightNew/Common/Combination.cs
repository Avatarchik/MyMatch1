using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FightNew
{
	[System.Serializable]
	public class Combinations 
	{
		public int priority = 0;
		public string powerup;
		public bool horizontal = true;
		public bool vertical = true;
		public bool square = false;
		public int minCount = 4;
		public string tag = "";

		public Combinations(int _priority,string _powerup,bool _horizontal,bool _vertical,int _minCount)
		{
			priority = _priority;
			powerup = _powerup;
			horizontal = _horizontal;
			vertical = _vertical;
			minCount = _minCount;
		}


		public static List<Combinations> combinations = new List<Combinations>();
		public static void InitCombinationData()
		{
			combinations.Add(new Combinations(1,"ThreeLine",true,false,5));
			combinations.Add(new Combinations(2,"ThreeLine",false,true,5));

//			combinations.Add(new Combinations(3,"ColorBomb",true,false,5));
//			combinations.Add(new Combinations(4,"ColorBomb",false,false,5));
//
//			combinations.Add(new Combinations(5,"SimpleBomb",true,false,4));
//			combinations.Add(new Combinations(6,"SimpleBomb",false,true,4));

			//			return;

			//			combinations.Add(new Combinations(1,"UltraColorBomb",false,true,5));
			//			combinations.Add(new Combinations(2,"UltraColorBomb",true,false,5));
			combinations.Add(new Combinations(3,"CrossLine",true,true,5));
			//			combinations.Add(new Combinations(4,"CrossBomb",false,true,5));
			combinations.Add(new Combinations(4,"OneLine",true,false,4));
			combinations.Add(new Combinations(5,"OneLine",false,true,4));


			combinations.Sort((Combinations a, Combinations b) => 
				{
					if (a.priority < b.priority)
						return -1;
					if (a.priority > b.priority)
						return 1;
					return 0;
				});
		}
	}
}
