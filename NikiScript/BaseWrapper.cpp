#include "BaseWrapper.h"

char* ns::allocStringToCharArray(const std::string& src) {
	char* out = new char[src.size()+1];
	memcpy(out, src.c_str(), src.size());
	out[src.size()] = '\0';

	return out;
}

void ns_freeString(char* string) {
	if (string != nullptr) {
		delete[] string;
		string = nullptr;
	}
}