#pragma once

#ifdef DLLCPP_EXPORTS
#define DLLCPP_API __declspec(dllexport)
#else
#define DLLCPP_API __declspec(dllimport)
#endif

extern "C" DLLCPP_API int plus(int a, int b);

extern "C" DLLCPP_API int minus(int a, int b);

extern "C" DLLCPP_API int multiply(int a, int b);
