require "Logic/Network"

TableBattleCtrl = {};

TableBattleCtrl.BattleTable = dofile('Tables/Battle');
local BattleTable = TableBattleCtrl.BattleTable ;

--��ȡID���Ӧ������
--����-1��ʾʧ��
function TableBattleCtrl.GetIndexByID(id)
	local count = table.getn(BattleTable.Battle.ID);
	
	for idx = 1,count do
		if(BattleTable.Battle.ID[idx] == id) then
		return idx;
		end
	end 
	return -1;
end

function TableBattleCtrl.GetName(id)
	local idx = TableBattleCtrl.GetIndexByID(id);
	log('TableBattleCtrl.GetName idx = '..tostring(idx));
	if idx ~= -1 then
		log('TableBattleCtrl.GetName name = '..BattleTable.Battle.Name[idx]);
		return BattleTable.Battle.Name[idx];
	end
	return -1;
end

--������ұ���������ID
function TableBattleCtrl.GetIndexByTrophy(trophy)
	local count = table.getn(BattleTable.Battle.Unlocked);
	
	if	trophy==0	then
		local str= utilMgr: PlayerPrefs_GetString_Lua(UIMainModule.ServerHomeData.user.id.."newbie","yesnewbie");
		if	str=="noMore"	then
			return 2;
		else
			return 1;
		end
	end	

	if	trophy >= BattleTable.Battle.Unlocked[count]	then
		return count;
	end
	
	for idx = 3,count do
		if	BattleTable.Battle.Unlocked[idx] > trophy	then
			if	BattleTable.Battle.Unlocked[idx-1] <= trophy then
				return idx-1;
			end
		end
	end 
	return -1;
end

--������ұ����������볡�����
function TableBattleCtrl.GetGoldCost(trophy)
	local idx = TableBattleCtrl.GetIndexByTrophy(trophy);
	if idx ~= -1 then
		return BattleTable.Battle.Gold_consume[idx];
	end
	return -1;
end

--������ұ���������ս������
function TableBattleCtrl.GetBattleField(trophy)
	local idx = TableBattleCtrl.GetIndexByTrophy(trophy);
	if idx ~= -1 then
		return BattleTable.Battle.Name[idx];
	end
	return -1;
end