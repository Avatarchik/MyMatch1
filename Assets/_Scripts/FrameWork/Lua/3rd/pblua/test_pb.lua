-- Generated By protoc-gen-lua Do not Edit
local protobuf = require "protobuf"
module('test_pb')


local TEST = protobuf.Descriptor();
local TEST_INT32_NUMBER_FIELD = protobuf.FieldDescriptor();
local TESTFIGHT = protobuf.Descriptor();
local TESTFIGHT_RESULT_FIELD = protobuf.FieldDescriptor();

TEST_INT32_NUMBER_FIELD.name = "int32_number"
TEST_INT32_NUMBER_FIELD.full_name = ".Protocol.Test.int32_number"
TEST_INT32_NUMBER_FIELD.number = 1
TEST_INT32_NUMBER_FIELD.index = 0
TEST_INT32_NUMBER_FIELD.label = 2
TEST_INT32_NUMBER_FIELD.has_default_value = false
TEST_INT32_NUMBER_FIELD.default_value = 0
TEST_INT32_NUMBER_FIELD.type = 5
TEST_INT32_NUMBER_FIELD.cpp_type = 1

TEST.name = "Test"
TEST.full_name = ".Protocol.Test"
TEST.nested_types = {}
TEST.enum_types = {}
TEST.fields = {TEST_INT32_NUMBER_FIELD}
TEST.is_extendable = false
TEST.extensions = {}
TESTFIGHT_RESULT_FIELD.name = "result"
TESTFIGHT_RESULT_FIELD.full_name = ".Protocol.TestFight.result"
TESTFIGHT_RESULT_FIELD.number = 1
TESTFIGHT_RESULT_FIELD.index = 0
TESTFIGHT_RESULT_FIELD.label = 2
TESTFIGHT_RESULT_FIELD.has_default_value = false
TESTFIGHT_RESULT_FIELD.default_value = 0
TESTFIGHT_RESULT_FIELD.type = 5
TESTFIGHT_RESULT_FIELD.cpp_type = 1

TESTFIGHT.name = "TestFight"
TESTFIGHT.full_name = ".Protocol.TestFight"
TESTFIGHT.nested_types = {}
TESTFIGHT.enum_types = {}
TESTFIGHT.fields = {TESTFIGHT_RESULT_FIELD}
TESTFIGHT.is_extendable = false
TESTFIGHT.extensions = {}

Test = protobuf.Message(TEST)
TestFight = protobuf.Message(TESTFIGHT)
