#include "BaseWrapper.h"

char* ns::newString(size_t size) {
	char* string = new char[size+1];
	string[size] = '\0';
	return string;
}

void ns_freeString(char* string) {
	if (string != nullptr) {
		delete[] string;
		string = nullptr;
	}
}