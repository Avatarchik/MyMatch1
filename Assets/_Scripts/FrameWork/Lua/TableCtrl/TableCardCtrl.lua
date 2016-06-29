
TableCardCtrl = {};

local CardTable = dofile('Tables/Card');

--��ȡID���Ӧ������
--����-1��ʾʧ��
function TableCardCtrl.GetIndexByID(id)
	local count = table.getn(CardTable.Card.ID);
	for idx = 1,count do
		if(CardTable.Card.ID[idx] == id) then
		return idx
                end
	end 
	return -1;
end

function TableCardCtrl.GetName(id)
	local idx = GetIndexByID(id);
	if idx ~= -1 then
		return CardTable.Card.Name[idx];
	end
	return -1;
end

function TableCardCtrl.GetDesc(id)
	local idx = GetIndexByID(id);
	if idx ~= -1 then
		return CardTable.Card.Desc[idx];
	end
	return -1;
end

function TableCardCtrl.GetCard(id)
	local idx = GetIndexByID(id);
	if idx ~= -1 then
		return CardTable.Card.Card[idx];
	end
	return -1;
end

function TableCardCtrl.GetModel(id)
	local idx = GetIndexByID(id);
	if idx ~= -1 then
		return CardTable.Card.Model[idx];
	end
	return -1;
end

function TableCardCtrl.GetEnergy5_NonLinear(id)
	local idx = GetIndexByID(id);
	if idx ~= -1 then
		return CardTable.Card.Energy5_NonLinear[idx];
	end
	return -1;
end