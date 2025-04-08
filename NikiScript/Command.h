#pragma once

#include <vector>
#include <string>

namespace ns {
	struct Context;

	typedef void(*CommandCallback)(Context& ctx);

	struct Command {
		std::string name;
		unsigned char minArgs = 0, maxArgs = 0;
		CommandCallback callback = nullptr;
		std::string description;

		/**
		 * @note odd = name
		 * @note even = description
		 */
		std::vector<std::string> argsDescriptions{};

		Command();
		/**
		 * @param name
		 * @param minArgs
		 * @param maxArgs
		 * @param callback
		 * @param description Command description
		 * @param argsDescriptions Arguments description. Should have 2 strings for each argument, where the first one is argument name and the second is argument description.
		 * @see ns::registerCommands for code example
		 */
		Command(const std::string& name, unsigned char minArgs, unsigned char maxArgs, CommandCallback callback, const std::string& description, const std::vector<std::string>& argsDescriptions);
	
		std::string getArgumentsNames();

		/**
		 * @brief prints usage, description and argsDescriptions all like a data tree
		 */
		void printAsDataTree();
	};
}