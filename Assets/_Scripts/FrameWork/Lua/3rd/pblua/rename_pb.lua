-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf"
local result_pb = require("result_pb")
module('rename_pb')


local CLIENTRENAME = protobuf.Descriptor();
local CLIENTRENAME_NICK_FIELD = protobuf.FieldDescriptor();
local SERVERRENAME = protobuf.Descriptor();
local SERVERRENAME_RESULT_FIELD = protobuf.FieldDescriptor();
local SERVERRENAME_NICK_FIELD = protobuf.FieldDescriptor();

CLIENTRENAME_NICK_FIELD.name = "nick"
CLIENTRENAME_NICK_FIELD.full_name = ".Protocol.ClientRename.nick"
CLIENTRENAME_NICK_FIELD.number = 1
CLIENTRENAME_NICK_FIELD.index = 0
CLIENTRENAME_NICK_FIELD.label = 2
CLIENTRENAME_NICK_FIELD.has_default_value = false
CLIENTRENAME_NICK_FIELD.default_value = ""
CLIENTRENAME_NICK_FIELD.type = 9
CLIENTRENAME_NICK_FIELD.cpp_type = 9

CLIENTRENAME.name = "ClientRename"
CLIENTRENAME.full_name = ".Protocol.ClientRename"
CLIENTRENAME.nested_types = {}
CLIENTRENAME.enum_types = {}
CLIENTRENAME.fields = {CLIENTRENAME_NICK_FIELD}
CLIENTRENAME.is_extendable = false
CLIENTRENAME.extensions = {}
SERVERRENAME_RESULT_FIELD.name = "result"
SERVERRENAME_RESULT_FIELD.full_name = ".Protocol.ServerRename.result"
SERVERRENAME_RESULT_FIELD.number = 1
SERVERRENAME_RESULT_FIELD.index = 0
SERVERRENAME_RESULT_FIELD.label = 2
SERVERRENAME_RESULT_FIELD.has_default_value = false
SERVERRENAME_RESULT_FIELD.default_value = nil
SERVERRENAME_RESULT_FIELD.enum_type = RESULT_PB_RESULT
SERVERRENAME_RESULT_FIELD.type = 14
SERVERRENAME_RESULT_FIELD.cpp_type = 8

SERVERRENAME_NICK_FIELD.name = "nick"
SERVERRENAME_NICK_FIELD.full_name = ".Protocol.ServerRename.nick"
SERVERRENAME_NICK_FIELD.number = 2
SERVERRENAME_NICK_FIELD.index = 1
SERVERRENAME_NICK_FIELD.label = 2
SERVERRENAME_NICK_FIELD.has_default_value = false
SERVERRENAME_NICK_FIELD.default_value = ""
SERVERRENAME_NICK_FIELD.type = 9
SERVERRENAME_NICK_FIELD.cpp_type = 9

SERVERRENAME.name = "ServerRename"
SERVERRENAME.full_name = ".Protocol.ServerRename"
SERVERRENAME.nested_types = {}
SERVERRENAME.enum_types = {}
SERVERRENAME.fields = {SERVERRENAME_RESULT_FIELD, SERVERRENAME_NICK_FIELD}
SERVERRENAME.is_extendable = false
SERVERRENAME.extensions = {}

ClientRename = protobuf.Message(CLIENTRENAME)
ServerRename = protobuf.Message(SERVERRENAME)

