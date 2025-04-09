#include "CommandHandlerWrapper.h"

#include "BaseWrapper.h"

#include "Context.h"

ns_CommandHandler* ns_CommandHandlerNew() {
	return new ns_CommandHandler();
}

NIKIAPI void ns_CommandHandlerDelete(ns_CommandHandler *pCommandHandler) {
	delete pCommandHandler;
}

// std::unordered_map<std::string, ns_Command> commands;
const char* ns_CommandHandlerGetCommandsNames(const ns_CommandHandler *pCommandHandler) {
	if (pCommandHandler->commands.empty())
		return nullptr;

	std::string names = "";
	for (auto it = pCommandHandler->commands.begin(); it != pCommandHandler->commands.end(); ++it)
		names += it->first + " ";
	names.erase(names.size()-1);

	return ns::allocStringToCharArray(names);
}

ns_Command* ns_CommandHandlerGetCommand(ns_CommandHandler *pCommandHandler, const char *name) {
	auto it = pCommandHandler->commands.find(name);
	if (it == pCommandHandler->commands.end())
		return nullptr;
	else
		return &it->second;
}

ns_Command* ns_CommandHandlerAdd(ns_CommandHandler *pCommandHandler, const ns_Command *pCommand) {
	if (pCommandHandler->add(*pCommand))
		return &pCommandHandler->commands[pCommand->name];
	else
		return nullptr;
}

void ns_CommandHandlerErase(ns_CommandHandler *pCommandHandler, const char *name) {
	pCommandHandler->commands.erase(name);
}

void ns_CommandHandlerClear(ns_CommandHandler *pCommandHandler) {
	pCommandHandler->commands.clear();
}