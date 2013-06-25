Delta.Wsq
=========

This is a WSQ encoder/decoder based on the [NIST implementation](http://www.nist.gov/itl/iad/ig/nbis.cfm). 
The underlying implementation was rebuilt from source code using GCC for Windows (32 and 64 bits), thus providing 
x86/x64 versions (as well as debug/release ones) of the codec.

As well as the codec assembly (Delta.Wsq.dll) a plugin for [Paint.NET](http://www.getpaint.net/) is also provided as a working example.

**NOTES:** 

* The provided solution should build with either Visual Studio 2010 or 2012, but in the latter case, Visual Studio 2010 (or at least,
the Visual Studio 2010 C++ compiler) should be installed.

* The provided **build.cmd** script builds all configuration and platforms.

*__TODO:__ Document a bit how to build from NIST source code.*