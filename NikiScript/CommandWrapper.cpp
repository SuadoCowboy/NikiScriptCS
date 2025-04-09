#include "CommandWrapper.h"

#include "BaseWrapper.h"

#include <vector>
#include <string>

ns_Command* ns_newCommand(const char* name, uint8_t minArgs, uint8_t maxArgs, ns_CommandCallback callback, const char* description, const char* _argsDescriptions[]) {
	std::vector<std::string> argsDescriptions{};
	for (uint16_t i = 0; i < maxArgs; i++)
		argsDescriptions.push_back(_argsDescriptions[i]);

	return new ns::Command(name, minArgs, maxArgs, callback, description, argsDescriptions);
}

void ns_deleteCommand(ns_Command* pCommand) {
	delete pCommand;
}

char* ns_CommandAllocGetArgumentsNames(ns_Command* pCommand) {
	return ns::allocStringToCharArray(pCommand->getArgumentsNames());
}

void ns_CommandPrintAsDataTree(ns_Command* pCommand) {
	pCommand->printAsDataTree();
}


void ns_CommandSetName(ns_Command* pCommand, const char* name) {
	pCommand->name = name;
}

char* ns_CommandAllocGetName(ns_Command* pCommand) {
	return ns::allocStringToCharArray(pCommand->name);
}


void ns_CommandSetMinArgs(ns_Command* pCommand, uint8_t minArgs) {
	pCommand->minArgs = minArgs;
}

uint8_t ns_CommandGetMinArgs(ns_Command* pCommand) {
	return pCommand->minArgs;
}


void ns_CommandSetMaxArgs(ns_Command* pCommand, uint8_t maxArgs) {
	pCommand->maxArgs = maxArgs;
}

uint8_t ns_CommandGetMaxArgs(ns_Command* pCommand) {
	return pCommand->maxArgs;
}


void ns_CommandSetCallback(ns_Command* pCommand, ns_CommandCallback callback) {
	pCommand->callback = callback;
}

ns_CommandCallback ns_CommandGetCallback(ns_Command* pCommand) {
	return pCommand->callback;
}


void ns_CommandSetDescription(ns_Command* pCommand, const char* description) {
	pCommand->description = description;
}

char* ns_CommandAllocGetDescription(ns_Command* pCommand) {
	return ns::allocStringToCharArray(pCommand->description);
}


void ns_CommandSetArgsDescriptions(ns_Command* pCommand, const char* argsDescriptions[]) {
	for (uint16_t i = 0; i < pCommand->maxArgs; ++i)
		pCommand->argsDescriptions[i] = argsDescriptions[i];
}

char* ns_CommandAllocGetArgDescription(ns_Command* pCommand, uint16_t index) {
	return ns::allocStringToCharArray(pCommand->argsDescriptions[index]);
}

uint16_t ns_CommandGetArgsDescriptionsSize(ns_Command* pCommand) {
	return pCommand->argsDescriptions.size();
}