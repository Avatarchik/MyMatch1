
TableTutorialCtrl = {};
TableTutorialCtrl.TutorialTable = dofile('Tables/Tutorial');
local TutorialTable = TableTutorialCtrl.TutorialTable;
local TempData = {};
TableTutorialCtrl.IDTable = {};

function TableTutorialCtrl.GetDataCount()
    return table.getn(TutorialTable.Tutorial.id);
end

function TableTutorialCtrl.GetDataByIndex(index)
    TempData.id = TutorialTable.Tutorial.id[index];
    TempData.name = TutorialTable.Tutorial.name[index];
    TempData.scene_index = TutorialTable.Tutorial.scene_index[index];
    TempData.level_id = TutorialTable.Tutorial.level_id[index];
    TempData.condition = TutorialTable.Tutorial.condition[index];
    TempData.condition_operator = TutorialTable.Tutorial.condition_operator[index];
    TempData.condition_value = TutorialTable.Tutorial.condition_value[index];
    TempData.execute_times = TutorialTable.Tutorial.execute_times[index];
    TempData.after_execute = TutorialTable.Tutorial.after_execute[index];
    TempData.next_step = TutorialTable.Tutorial.next_step[index];
    TempData.sound = TutorialTable.Tutorial.sound[index];
    TempData.script = TutorialTable.Tutorial.script[index];
    return TempData;
end

function TableTutorialCtrl.GetIndexByID(id)
    local count = table.getn(TutorialTable.Tutorial.id);
    for idx = 1,count do
	if(TutorialTable.Tutorial.id[idx] == id) then
            return idx
	end 
    end
    return -1;
end

function TableTutorialCtrl.GetName(id)
	local idx = TableTutorialCtrl.GetIndexByID(id);
	if idx ~= -1 then
		return TutorialTable.Tutorial.name[idx];
	end
	return -1;
end

function TableTutorialCtrl.GetSceneIndex(id)
    local idx = TableTutorialCtrl.GetIndexByID(id);
    if idx ~= -1 then
	return TutorialTable.Tutorial.scene_index[idx];
    end
    return -1;
end

function TableTutorialCtrl.GetCondition(id)
    local idx = TableTutorialCtrl.GetIndexByID(id);
    if idx ~= -1 then
	return TutorialTable.Tutorial.condition[idx];
    end
    return -1;
end

function TableTutorialCtrl.GetOperator(id)
    local idx = TableTutorialCtrl.GetIndexByID(id);
    if idx ~= -1 then
	return TutorialTable.Tutorial.condition_operator[idx];
    end
    return -1;
end

function TableTutorialCtrl.GetConditionVal(id)
    local idx = TableTutorialCtrl.GetIndexByID(id);
    if idx ~= -1 then
	return TutorialTable.Tutorial.condition_value[idx];
    end
    return -1;
end

function TableTutorialCtrl.GetScript(id)
    local idx = TableTutorialCtrl.GetIndexByID(id);
    if idx ~= -1 then
	return TutorialTable.Tutorial.script[idx];
    end
    return -1;
end