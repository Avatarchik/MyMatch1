
TableSkillCtrl = {};

TableSkillCtrl.SkillTable = dofile('Tables/Skill');
local SkillTable = TableSkillCtrl.SkillTable ;

--获取ID相对应的索引
--返回-1表示失败
function TableSkillCtrl.GetIndexByID(id) --通过ID，得到index-即哪一列
	local count = table.getn(SkillTable.Skill.id);
	
	for idx = 1,count do
		if(SkillTable.Skill.id[idx] == id) then
		return idx;
		end
	end 
	return -1;
end

function TableSkillCtrl.GetName(id)--通过ID，先得到index-即哪一列，再通过这一列得到该列的其他属性，比如名字Name，描述Desc
	local idx = TableSkillCtrl.GetIndexByID(id);
	log('TableSkillCtrl.GetName idx = '..tostring(idx));
	if idx ~= -1 then
		log('TableSkillCtrl.GetName name = '..SkillTable.Skill.name[idx]);
		return SkillTable.Skill.name[idx];
	end
	return -1;
end

--根据id返回整个skill的信息
function TableSkillCtrl.Get_1_Skill(id)
	local idx = TableSkillCtrl.GetIndexByID(id);
	local SkillInfo={};
	log(' idx = '..tostring(idx));
	if idx ~= -1 then
		SkillInfo.id=SkillTable.Skill.id[idx];
		SkillInfo.name=SkillTable.Skill.name[idx];
		SkillInfo.desc=SkillTable.Skill.desc[idx];
		return SkillInfo;
	else
		log("id not in the xls");
	end
end

--一键返回所有skill的信息
function TableSkillCtrl.Get_All_Skills()
	local AllSkillInfo={};
	for i=1, table.getn(SkillTable.Skill.id)	do
		local SkillInfo={};		
			SkillInfo.id=SkillTable.Skill.id[i];
			SkillInfo.name=SkillTable.Skill.name[i];
			SkillInfo.desc=SkillTable.Skill.desc[i];
			SkillInfo.skill_type_main=SkillTable.Skill.skill_type_main[i];
			SkillInfo.skill_type_detail=SkillTable.Skill.skill_type_detail[i];
			SkillInfo.effect_num=SkillTable.Skill.effect_num[i];
			SkillInfo.need_energy=SkillTable.Skill.need_energy[i];
			SkillInfo.harm_para=SkillTable.Skill.harm_para[i];
			SkillInfo.cooldown=SkillTable.Skill.cooldown[i];
			SkillInfo.skill_icon=SkillTable.Skill.skill_icon[i];
			SkillInfo.animation_name=SkillTable.Skill.animation_name[i];
			SkillInfo.attack_fashion=SkillTable.Skill.attack_fashion[i];
			SkillInfo.effect=SkillTable.Skill.effect[i];
			SkillInfo.attack_prefab_name=SkillTable.Skill.attack_prefab_name[i];
			SkillInfo.audio_name=SkillTable.Skill.audio_name[i];
			
			AllSkillInfo[i]=SkillInfo;
	end
	return AllSkillInfo;
end