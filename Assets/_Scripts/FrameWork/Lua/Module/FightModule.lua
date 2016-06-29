require "Common/define"
require "3rd/pblua/auth_pb"
require "3rd/pblua/match_pb"
require "3rd/pblua/msg_pb"
require "protobuf"

Event = require 'events'

FightModule = {};
local this = FightModule;

--����������--
local ServerFightData;

local ServerFightResultData;

local ServerRankChangeData;

local ServerRankUpData;

local ServerFaceData;


--��������--
function FightModule.New()
	logWarn("FightModule.New--->>");
	return this;
end



function FightModule.GetBoss_id()
	
	return FightModule.ServerFightData.boss_id;
end

function FightModule.GetHp()
	
	return FightModule.ServerFightData.hp;
end


function FightModule.GetFightResult()
	return FightModule.ServerFightResultData.result;
end

function FightModule.GetAwardTrophy()
	--log('trophy:'..tostring(FightModule.ServerFightResultData.award.trophy));
	return FightModule.ServerFightResultData.award.trophy;
end

function FightModule.GetAwardG_money()
	return FightModule.ServerFightResultData.award.g_money;
end

function FightModule.GetAwardDrop()
	return FightModule.ServerFightResultData.award.drop;
end

function FightModule.GetAwardExp()
	return FightModule.ServerFightResultData.award.exp;
end


--排名发生变动，获得自己名字，排名，奖杯
function FightModule.RankChange_GetMyName()
	return FightModule.ServerRankChangeData.exchanges[1].me.nick;
end

function FightModule.RankChange_GetMyRank(num)
	return FightModule.ServerRankChangeData.exchanges[num+1].me.current_rank;
end

function FightModule.RankChange_GetMyTrophy()
	return FightModule.ServerRankChangeData.exchanges[1].me.current_trophy;
end


--排名发生变动，获得对家名字，排名，奖杯,照片id
function FightModule.RankChange_GetOppoName(num)
	return FightModule.ServerRankChangeData.exchanges[num+1].target.nick;
end

function FightModule.RankChange_GetOppoRank(num)
	return FightModule.ServerRankChangeData.exchanges[num+1].target.current_rank;
end

function FightModule.RankChange_GetOppoTrophy(num)
	return FightModule.ServerRankChangeData.exchanges[num+1].target.current_trophy;
end

function FightModule.RankChange_GetOppoPhoto(num)
	return FightModule.ServerRankChangeData.exchanges[num+1].target.id;
end

--要做几次交换
function FightModule.RankChange_RankChangeNums()
	--log("FightModule.ServerRankChangeData.exchanges: "..table.getn(FightModule.ServerRankChangeData.exchanges));
	return  table.getn(FightModule.ServerRankChangeData.exchanges);
end

--排名大幅提升页面，自己的最新杯数
function FightModule.RankUp_GetMyTrophy()
	log("FightModule.ServerRankUpData.current_trophy..........................."..FightModule.ServerRankUpData.current_trophy);
	return FightModule.ServerRankUpData.current_trophy;
end

--排名大幅提升页面，排名提升的数目
function FightModule.RankUp_GetMyRankChange()
	log("FightModule.ServerRankUpData.rank_up................."..FightModule.ServerRankUpData.rank_up);
	return FightModule.ServerRankUpData.rank_up;
end