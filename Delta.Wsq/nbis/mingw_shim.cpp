#include "stdafx.h"
#include <stdio.h>

extern "C" {
	int debug = 1;
	int __cdecl __mingw_vfprintf(FILE* fileptr, const char* format, va_list arglist);
	int __cdecl __mingw_vsprintf(char* dest, const char* format, va_list arglist);
	int __cdecl __mingw_vsscanf(const char* source, const char* format, va_list arglist);
}

int __cdecl __mingw_vfprintf(FILE* fileptr, const char* format, va_list arglist)
{
	return vfprintf_s(fileptr, format, arglist);
}

int __cdecl __mingw_vsprintf(char* dest, const char* format, va_list arglist)
{
#pragma warning(disable: 4996)
	return vsprintf(dest, format, arglist);
#pragma warning(default: 4996)
}

// Hack: see http://stackoverflow.com/questions/2457331/replacement-for-vsscanf-on-msvc
int hacked_vsscanf(const char* source, const char* format, va_list arglist)
{
	void* a[20];
	for (int i = 0; i < sizeof(a) / sizeof (a[0]); i++) 
		a[i] = va_arg(arglist, void*);

	return sscanf_s(source, format, 
		a[0], a[1], a[2], a[3], a[4], a[5], a[6], a[7], a[8], a[9],
		a[10], a[11], a[12], a[13], a[14], a[15], a[16], a[17], a[18], a[19]);
}

int __cdecl __mingw_vsscanf(const char* source, const char* format, va_list arglist)
{
	return hacked_vsscanf(source, format, arglist);
}
