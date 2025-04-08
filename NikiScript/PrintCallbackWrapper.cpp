#include "PrintCallbackWrapper.h"

#include <string>
#include <cstdio>

#include "PrintCallback.h"

static void ns_defaultPrint(void*, ns_PrintLevel level, const char* message) {
	printf("[%s] %s", ns_printLevelToString(level), message);
}

static ns_PrintCallback internalPrintCallback = ns_defaultPrint;

static void proxyCallback(void* pData, ns::PrintLevel level, const char* message) {
	internalPrintCallback(pData, static_cast<ns_PrintLevel>(level), message);
}

void ns_setPrintCallback(void* pData, ns_PrintCallback callback) {
    internalPrintCallback = callback;
	ns::setPrintCallback(pData, proxyCallback);
}

void ns_Print(ns_PrintLevel level, const char* message) {
    ns::print(static_cast<ns::PrintLevel>(level), message);
}

void ns_printUnknownCommand(const char* command) {
    ns::printUnknownCommand(command);
}

const char* ns_printLevelToString(ns_PrintLevel level) {
    static std::string cached;
    cached = ns::printLevelToString(static_cast<ns::PrintLevel>(level));
    return cached.c_str();
}
