#pragma once

#include "DLLExport.h"

#include <stdint.h>

namespace ns {
	/**
	 * @param size not counting null-terminator as it's added automatically
	 */
	char* newString(size_t size);
}

extern "C" {
	NIKIAPI void ns_freeString(char* string);
}