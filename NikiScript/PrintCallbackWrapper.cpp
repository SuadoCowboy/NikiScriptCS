#include "PrintCallbackWrapper.h"

#include <string>
#include <cstdio>

static void ns_defaultPrint(void*, ns_PrintLevel level, const char* message) {
	printf("[%s] %s", ns_levelToString(level), message);
}

static ns_PrintCallback internalPrintCallback = ns_defaultPrint;

static void proxyCallback(void* pData, ns::PrintLevel level, const char* message) {
	internalPrintCallback(pData, level, message);
}

void ns_setPrintCallback(void* pData, ns_PrintCallback callback) {
    internalPrintCallback = callback;
	ns::setPrintCallback(pData, proxyCallback);
}

void ns_print(ns_PrintLevel level, const char* message) {
    ns::print(level, message);
}

void ns_printUnknownCommand(const char* command) {
    ns::printUnknownCommand(command);
}

const char* ns_levelToString(ns_PrintLevel level) {
    return ns::levelToString(level);
}
