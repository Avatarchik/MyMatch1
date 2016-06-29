require "Logic/Network"
require "Module/UIMainModule"

TablePlayerAttrCtrl = {};

TablePlayerAttrCtrl.PlayerAttrTable = dofile('Tables/PlayerAttr');
local PlayerAttrTable = TablePlayerAttrCtrl.PlayerAttrTable ;

--获取ID相对应的索引
--返回-1表示失败

--根据玩家等级，和需要卡槽位置card1/card2/card3，hp/atk，返回一个值，不加第三个参数，默认false,
function TablePlayerAttrCtrl.GetCardSeatAddition(cardnum,hp_or_atk,lastlevel)
	local idx;
	if	lastlevel==true	then	--不加第三个参数，默认false
		idx=UIMainModule.ServerHomeData.user.lv-1;
		if	idx<=0	then
			return 0;
		end
	else
		idx=UIMainModule.ServerHomeData.user.lv;
	end	
	
	--log("GetCardSeatAddition...........idx/lv = "..idx);
	if	cardnum==1	then
		if	hp_or_atk=="hp"	then
			return PlayerAttrTable.PlayerAttr.hp_add_1[idx];
		else
			if	hp_or_atk=="atk"	then
				return PlayerAttrTable.PlayerAttr.attack_add_1[idx];
			else
				return -1;
			end
		end
	else
		if	cardnum==2	then
			if	hp_or_atk=="hp"	then
				return PlayerAttrTable.PlayerAttr.hp_add_2[idx];
			else
				if	hp_or_atk=="atk"	then
					return PlayerAttrTable.PlayerAttr.attack_add_2[idx];
				else
					return -1;
				end
			end
		else
			if	cardnum==3	then
				if	hp_or_atk=="hp"	then
					return PlayerAttrTable.PlayerAttr.hp_add_3[idx];
				else
					if	hp_or_atk=="atk"	then
						return PlayerAttrTable.PlayerAttr.attack_add_3[idx];
					else
						return -1;
					end
				end
			else
				if	cardnum==0	then	
						return PlayerAttrTable.PlayerAttr.exp[idx];
				else
					return -1;
				end
			end	
		end
	end
end

