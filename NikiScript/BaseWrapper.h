#pragma once

#include <stdint.h>
#include <string>

#include "DLLExport.h"

namespace ns {
	/**
	 * @param size not counting null-terminator as it's added automatically
	 */
	char* allocStringToCharArray(const std::string& src);
}

extern "C" {
	NIKIAPI void ns_freeString(char* string);
}