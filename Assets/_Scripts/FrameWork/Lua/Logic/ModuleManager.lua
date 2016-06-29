require "Common/define"
require "Module/UILoginModule"
require "Module/UIMainModule" 
require "Module/UIRankModule"
require "Module/FightModule" 
require "Module/UINamingModule" 

ModuleManager = {};
local this = ModuleManager;
local moduleList = {};	--�������б�--

function ModuleManager.Init()
	logWarn("ModuleManager.Init----->>>");
	moduleList[ModuleNames.UILoginModule] = UILoginModule.New();
	moduleList[ModuleNames.UIMainModule] = UIMainModule.New();
	moduleList[ModuleNames.FightModule] = FightModule.New();
	return this;
end

--��ӿ�����--
function ModuleManager.AddModule(moduleName, moduleObj)
	moduleList[moduleName] = ctrlObj;
	logWarn('AddModule:' + moduleName);
end

--��ȡ������--
function ModuleManager.GetModule(moduleName)
	return moduleList[moduleName];
end

--�Ƴ�������--
function ModuleManager.RemoveModule(moduleName)
	moduleList[moduleName] = nil;
end

--�رտ�����--
function ModuleManager.Close()
	logWarn('ModuleManager.Close---->>>');
end