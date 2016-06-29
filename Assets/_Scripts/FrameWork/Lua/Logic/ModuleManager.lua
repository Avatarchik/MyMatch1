require "Common/define"
require "Module/UILoginModule"
require "Module/UIMainModule" 
require "Module/UIRankModule"
require "Module/FightModule" 
require "Module/UINamingModule" 

ModuleManager = {};
local this = ModuleManager;
local moduleList = {};	--¿ØÖÆÆ÷ÁĞ±í--

function ModuleManager.Init()
	logWarn("ModuleManager.Init----->>>");
	moduleList[ModuleNames.UILoginModule] = UILoginModule.New();
	moduleList[ModuleNames.UIMainModule] = UIMainModule.New();
	moduleList[ModuleNames.FightModule] = FightModule.New();
	return this;
end

--Ìí¼Ó¿ØÖÆÆ÷--
function ModuleManager.AddModule(moduleName, moduleObj)
	moduleList[moduleName] = ctrlObj;
	logWarn('AddModule:' + moduleName);
end

--»ñÈ¡¿ØÖÆÆ÷--
function ModuleManager.GetModule(moduleName)
	return moduleList[moduleName];
end

--ÒÆ³ı¿ØÖÆÆ÷--
function ModuleManager.RemoveModule(moduleName)
	moduleList[moduleName] = nil;
end

--¹Ø±Õ¿ØÖÆÆ÷--
function ModuleManager.Close()
	logWarn('ModuleManager.Close---->>>');
end