using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FightNew
{
	// Class with information of solution
	public class Solution {
		//   T
		//   T
		// LLXRR  X - center of solution
		//   B
		//   B

		public int count; // count of chip combination (count = T + L + R + B + X)
		public int hCount;
		public int vCount;
		public int potential; // potential of solution
		public int id; // ID of chip color
		public List<Card> chips = new List<Card>();

		// center of solution
		public int x;
		public int y;

		public bool v; // is this solution is vertical?  (v = L + R + X >= 3)
		public bool h; // is this solution is horizontal? (h = T + B + X >= 3)
		public bool q;
		//public int posV; // number on right chips (posV = R)
		//public int negV; // number on left chips (negV = L)
		//public int posH; // number on top chips (posH = T)
		//public int negH; // number on bottom chips (negH = B)
	}
}